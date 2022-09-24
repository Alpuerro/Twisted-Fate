using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler, IPointerEnterHandler
{
    public float dampingSpeed = 0.05f;
    public bool onZone = false;

    private RectTransform _draggingTransform;
    private Card _card;
    private CanvasGroup _canvasGroup;
    private Vector3 _velocity;
    private Canvas _canvas;

    private void Awake()
    {
        _draggingTransform = transform as RectTransform;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
        _card = GetComponent<Card>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 position = transform.position;
        transform.SetParent(_canvas.transform, false);
        transform.position = position;
        _canvasGroup.blocksRaycasts = false;
        _card.scaleToReset = Vector3.one;
        _card.SetCardRotation(0);
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
            transform.SetParent(FindObjectOfType<CardHand>().transform, false);
            GameEvents.CardRemoved.Invoke(_card.GetCardData());
        }
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnDropZone(Transform newParent, Vector2 position)
    {
        onZone = true;
        _card.CardPlayedAnimation();
        transform.SetParent(newParent, false);
        transform.position = position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!onZone)
            _card.ResetAnimation();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!onZone)
            _card.SelectedAnimation();
    }
}
