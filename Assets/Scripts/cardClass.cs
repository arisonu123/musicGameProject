using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cardClass : MonoBehaviour {
#pragma warning disable 649
    [Header("Possible Card Images")]
    [SerializeField]
    [Tooltip("Images to display on the cards")]
    private Image[] cardImages=new Image[8];

    [Header("Card specifics")]
    [SerializeField]
    [Range(0,8)]
    [Tooltip("This number determines the image loaded onto the card, The image will be the image at this location from the card image array")]
    private int cardNumber;
    [SerializeField]
    [Tooltip("Determines whether or not this card is a player card or a card to match to")]
    private bool isPlayerCard;
#pragma warning restore 649

    /// <summary>
    /// The number that corresponds to the note this card is
    /// </summary>
    public int cardNum
    {
        get { return cardNumber; }
    }
    private void Awake()
    {
        this.gameObject.GetComponent<Image>().sprite = cardImages[cardNumber].sprite;
    }
	
}
