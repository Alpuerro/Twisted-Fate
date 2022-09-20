using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardsPlayedZone : MonoBehaviour, IDropHandler
{
    private RectTransform _droppingTransform;
    private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] Button _playButton;

    public List<CardData> cardsPlayed = new List<CardData>();
    
    private void Awake()
    {
        _droppingTransform = transform as RectTransform;
        _layoutGroup = GetComponent<HorizontalLayoutGroup>();
        _playButton.interactable = false;
        GameEvents.CardPlayed.AddListener(AddCard);
        GameEvents.CardRemoved.AddListener(RemoveCard);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (cardsPlayed.Count < 4)
        {
            if (eventData.pointerDrag != null && RectTransformUtility.RectangleContainsScreenPoint(_droppingTransform, Input.mousePosition))
            {
                eventData.pointerDrag.GetComponent<DraggableItem>().OnDropZone(_droppingTransform, Vector2.zero);
                GameEvents.CardPlayed.Invoke(eventData.pointerDrag.GetComponent<Card>().GetCardData());
            }
        }
    }

    private void AddCard(CardData cardData)
    {
        if (!cardsPlayed.Contains(cardData))
        {
            cardsPlayed.Add(cardData);
        }

        CheckPlayButton();
        ReorderCardZone();
    }

    private void RemoveCard(CardData cardData)
    {
        if (cardsPlayed.Contains(cardData))
        {
            cardsPlayed.Remove(cardData);
        }

        CheckPlayButton();
        ReorderCardZone();
    }

    private void CheckPlayButton()
    {
        if (cardsPlayed.Count >= 2)
        {
            _playButton.interactable = true;
        }
        else
        {
            _playButton.interactable = false;
        }
    }

    public void PlayCards()
    {
        ComboCardManager.instance.ProcessComboCard(cardsPlayed);

        //TODO animacion de combo y descartarlas
    }

    private void ReorderCardZone()
    {
        if (_droppingTransform.childCount > 1)
        {
            RectTransform cardTransform = _droppingTransform.GetChild(0).transform as RectTransform;
            if (cardTransform.sizeDelta.x * _droppingTransform.childCount > _droppingTransform.sizeDelta.x)
            {
                _layoutGroup.spacing = -(cardTransform.sizeDelta.x * _droppingTransform.childCount - _droppingTransform.sizeDelta.x) / (_droppingTransform.childCount - 1);
            }
        }
    }

}
