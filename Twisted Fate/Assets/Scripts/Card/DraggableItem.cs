using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float dampingSpeed = 0.05f;
    public bool onZone = false;

    private RectTransform _draggingTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _velocity;
    private Canvas _canvas;

    private void Awake()
    {
        _draggingTransform = transform as RectTransform;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = _canvas.transform;
        _canvasGroup.blocksRaycasts = false;
        onZone = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
        {
            _draggingTransform.position = Vector3.SmoothDamp(_draggingTransform.position, globalMousePosition, ref _velocity, dampingSpeed);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!onZone)
        {
            GameEvents.CardRemoved.Invoke(GetComponent<Card>().GetCardData());
            transform.parent = FindObjectOfType<CardHand>().transform;
        }
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnDropZone(Transform newParent, Vector2 position)
    {
        onZone = true;
        transform.parent = newParent;
        transform.position = position;
    }
}
