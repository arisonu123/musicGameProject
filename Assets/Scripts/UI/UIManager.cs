using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIManager : MonoBehaviour {
#pragma warning disable 649
    [SerializeField]
    [Header("Gameobjects needed for UI functions")]
    private GameObject cameraObj;

    private cardClass currentCardSelected;
#pragma warning restore 649

   

    private int[] currentDisplayedCardNums = { 0, 1, 2, 3 };
	// Use this for initialization
	private void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

    /// <summary>
    /// Selects the card, moves it up and plays its corresponding note sound clip
    /// </summary>
    public void selectCard()
    {
        EventSystem.current.currentSelectedGameObject.transform.Translate(Vector3.forward);
        currentCardSelected = EventSystem.current.currentSelectedGameObject.GetComponent<cardClass>();
        cameraObj.GetComponent<AudioSource>().PlayOneShot(currentCardSelected.noteAudioClips[currentCardSelected.cardNum]);
    }

    /// <summary>
    /// Place the card in a location
    /// </summary>
    public void placeCard()
    {
        //fill data in this spot to correspond to data of dropped card
        EventSystem.current.currentSelectedGameObject.GetComponent<cardClass>().setCardData(currentCardSelected.cardNum);


    }

    /// <summary>
    /// Play the song
    /// </summary>
    public void playSong()
    {
        cameraObj.GetComponent<AudioSource>().PlayOneShot(GameObject.FindWithTag("songObj").GetComponent<SongClass>().songClip, 1);
     
    }


    /// <summary>
    /// Rotate the cards up
    /// </summary>
    public void rotateCardsUp()
    {
        for(int i = 0; i < currentDisplayedCardNums.Length; i++)
        {
            GameMaster.Instance.songCardArray[currentDisplayedCardNums[i]].SetActive(false);
            currentDisplayedCardNums[i] += 1;
            GameMaster.Instance.songCardArray[currentDisplayedCardNums[i]].SetActive(true);
        }     
       
    }


    /// <summary>
    /// Rotate the cards down
    /// </summary>
    public void rotateCardsDown()
    {
        for (int i = 0; i < currentDisplayedCardNums.Length; i++)
        {
            GameMaster.Instance.songCardArray[currentDisplayedCardNums[i]].SetActive(false);
            currentDisplayedCardNums[i] -= 1;
            GameMaster.Instance.songCardArray[currentDisplayedCardNums[i]].SetActive(true);
        }
    }
}
