using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class CardsPlayedZone : MonoBehaviour, IDropHandler
{
    private RectTransform _droppingTransform;
    private HorizontalLayoutGroup _layoutGroup;
    [SerializeField] Button _playButton;
    private CardGraveyard _cardGraveyard;

    public List<CardData> cardsPlayed = new List<CardData>();
    public List<Card> cards = new List<Card>();
    public bool playCards = false;

    private void Awake()
    {
        _droppingTransform = transform as RectTransform;
        _layoutGroup = GetComponent<HorizontalLayoutGroup>();
        _playButton.interactable = false;
        _cardGraveyard = FindObjectOfType<CardGraveyard>();
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

    public void SetPlayCards(bool newValue)
    {
        playCards = newValue;
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

    public async Task PlayCards()
    {
        await TaskUtils.WaitUntil(() => playCards == true);
        for (int i = 0; i < _droppingTransform.childCount; i++)
        {
            Sequence playCardSequence = DOTween.Sequence();
            cards.Add(_droppingTransform.GetChild(i).GetComponent<Card>());

            //playCardSequence.Append(_droppingTransform.GetChild(i).DOShakePosition(0.75f, 4f).SetEase(Ease.InOutSine));
            playCardSequence.Append(_droppingTransform.GetChild(i).DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.InOutSine));
            playCardSequence.Append(_droppingTransform.GetChild(i).DOMove(_cardGraveyard.transform.position, 0.7f).SetEase(Ease.InOutSine));

            if (i == 0) //only do once
            {
                playCardSequence.AppendCallback(() =>
                {
                    ComboCardManager.instance.ProcessComboCard(cardsPlayed);

                    _cardGraveyard.AddCards(cardsPlayed);
                    ClearZone();
                });
            }

            playCardSequence.Play();
        }
        playCards = false;

        await Task.Yield();
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

    private void ClearZone()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            CardPool.instance.ReturnCard(cards[i]);
        }
        cardsPlayed.Clear();
        CheckPlayButton();
    }

}
