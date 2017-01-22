using UnityEngine;
using System.Collections;

public class PlayerHand : MonoBehaviour {

    private GameObject[] CardsInHand;
    public GameObject DefaultCard;
    public int[] RotationValuesLtR;
    public Vector2[] MinPlacementValuesLtR;
    public Vector2[] MaxPlacementValuesLtR;
    public Vector3 CardScale;

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
        if (/*Cards in Pool*/ true)
        {
            GameObject Card = (GameObject)Instantiate(DefaultCard);
            Card.transform.parent = this.transform;
            Card.transform.localScale = CardScale;
            CardsInHand[handPlacement] = Card;

            Card.GetComponent<cardClass>().setCardNumber(/*GameMaster.Instance.getNextCard()*/1);
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


}
