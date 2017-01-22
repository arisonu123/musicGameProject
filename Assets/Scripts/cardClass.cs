using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cardClass : MonoBehaviour {
#pragma warning disable 649
    [Header("Possible Card Images")]
    [SerializeField]
    [Tooltip("Images to display on the cards")]
    private Sprite cardImage;
    [SerializeField]
    [Tooltip("Image to display when placed on scale")]
    private Sprite scaleNoteImage;

    [Header("Sounds for different cards/notes")]
    [SerializeField]
    private AudioClip[] noteSounds = new AudioClip[8];

    [Header("Card specifics")]
    [SerializeField]
    [Range(0,10)]
    [Tooltip("This number determines the image loaded onto the card, The image will be the image at this location from the card image array")]
    private int cardNumber;
  
    [SerializeField]
    [Tooltip("Determines whether or not this card is a player card or a card to match to")]
    private bool isPlayerCard = true;

    private int placeInHand;
#pragma warning restore 649

    public int getPlaceInHand()
    {
        return placeInHand;
    }

    public void setPlaceInHand(int p)
    {
        placeInHand = p;
    }

    /// <summary>
    /// The number that corresponds to the note this card is
    /// </summary>
    /// <value>The number that corresponds to a specific note that this card represents</value>
    public int cardNum
    {
        get { return cardNumber; }
    }

    public void setCardNumber(int num)
    {
        cardNumber = num;
    }

    public bool isNote()
    {
        return !isPlayerCard;
    }

    /// <summary>
    /// The array of note audio clips
    /// </summary>
    /// <value>The audio clips that correspond to different musical notes</value>
    public AudioClip[] noteAudioClips
    {
        get { return noteSounds; }
    }

    private void Awake()
    {
        if (isPlayerCard)
        {
            this.gameObject.GetComponent<Image>().sprite = cardImage;
        }
        
    }
	


    /// <summary>
    /// Set card data
    /// </summary>
    /// <param name="noteNumber">Number note that this card should correspond to</param>
    public void setCardData(int noteNumber,bool isOnScale)
    {
        cardNumber = noteNumber;
        if (isOnScale)
        {
            this.gameObject.GetComponent<Image>().sprite = scaleNoteImage;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = cardImage;
        }
        isPlayerCard = !isPlayerCard;
    }
}
