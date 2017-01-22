using UnityEngine;
using System.Collections;

public class PlayerHand : MonoBehaviour {

    private GameObject[] CardsInHand;
    public GameObject DefaultCard;
    public int[] RotationValuesLtR;
    public Vector2[] MinPlacementValuesLtR;
    public Vector2[] MaxPlacementValuesLtR;
    public Vector3 CardScale;
    public Transform parentOfCardsInHand; // Apparently gets mad when actually made a child of hand

    private void Start()
    {
        CardsInHand = new GameObject[4];

    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (CardsInHand[i] == null) CreateCard(i);
        }
    }

    /// <summary>
    /// Creates the card object to be placed in the player's hand
    /// </summary>
    void CreateCard(int handPlacement)
    {
        if (GameMaster.Instance.queueSize() != 0)
        {
            GameObject Card = (GameObject)Instantiate(DefaultCard);
            Card.transform.parent = parentOfCardsInHand;
            Card.transform.localScale = CardScale;
            CardsInHand[handPlacement] = Card;

            Card.GetComponent<cardClass>().setCardNumber(GameMaster.Instance.getNextCard());
            Card.GetComponent<cardClass>().setPlaceInHand(handPlacement);
            RotateCard(handPlacement, Card);
        }
    }

    /// <summary>
    /// Rotates the card based on it's location in the player's hand
    /// </summary>
    void RotateCard(int handPlacement, GameObject Card)
    {
        Card.transform.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, RotationValuesLtR[handPlacement]));
        Card.transform.GetComponent<RectTransform>().offsetMin = MinPlacementValuesLtR[handPlacement];
        Card.transform.GetComponent<RectTransform>().offsetMax = MaxPlacementValuesLtR[handPlacement];
    }

    public void removeCardFromHand(int placeInHand)
    {
        if (CardsInHand[placeInHand] != null) CardsInHand[placeInHand] = null;
    }

    public void returnCardToHand(GameObject card)
    {
        for (int i = 0; i < 4; i++)
        {
            if (CardsInHand[i] == null)
            {
                cardClass temp = card.GetComponent<cardClass>();
                CardsInHand[i] = card;
                temp.setPlaceInHand(i);
                temp.setCardData(temp.cardNum, false);
                card.transform.parent = parentOfCardsInHand;
                RotateCard(i, card);

                break;
            }
        }
    }

    public bool handFull()
    {
        for (int i = 0; i < 4; i++)
        {
            if (CardsInHand[i] == null) return false;
        }
        return true;
    }
}
