using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class Player : ScriptableObject
{
    public int health;
    public int defense;
    [Space(10)]
    public CardHand cardHand;
    public CardDeck cardDeck;
    public CardGraveyard cardGraveyard;
    public int cardsToDraw;
}