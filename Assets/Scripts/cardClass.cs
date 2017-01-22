using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cardClass : MonoBehaviour {
#pragma warning disable 649
    [Header("Possible Card Images")]
    [SerializeField]
    [Tooltip("Images to display on the cards,should be size 8")]
    private Sprite[] cardImages=new Sprite[8];
    [SerializeField]
    [Tooltip("Image to display when placed on scale")]
    private Sprite scaleNoteImage;

    [Header("Sounds for different cards/notes")]
    [SerializeField]
    private AudioClip[] noteSounds = new AudioClip[8];

    [Header("Card specifics")]
    [SerializeField]
    [Range(0,8)]
    [Tooltip("This number determines the image loaded onto the card, The image will be the image at this location from the card image array")]
    private int cardNumber;
  
    [SerializeField]
    [Tooltip("Determines whether or not this card is a player card or a card to match to")]
    private bool isPlayerCard = true;
#pragma warning restore 649

    /// <summary>
    /// The number that corresponds to the note this card is
    /// </summary>
    /// <value>The number that corresponds to a specific note that this card represents</value>
    public int cardNum
    {
        get { return cardNumber; }
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
        int cardNumber = Random.Range(0, 10);
        this.gameObject.GetComponent<Image>().sprite = cardImages[0];
        
    }
	
	
	// Update is called once per frame
	private void Update () {
	
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
            this.gameObject.GetComponent<Image>().sprite = cardImages[cardNumber];
        }
        isPlayerCard = !isPlayerCard;
    }
}
