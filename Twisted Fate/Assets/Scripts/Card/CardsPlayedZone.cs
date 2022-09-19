using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardsPlayedZone : MonoBehaviour, IDropHandler
{
    private RectTransform _droppingTransform;
    
    private void Awake()
    {
        _droppingTransform = transform as RectTransform;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DraggableItem>().OnDropZone(_droppingTransform, Vector2.zero);
        }
    }

}
