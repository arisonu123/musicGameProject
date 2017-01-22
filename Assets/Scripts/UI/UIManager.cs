using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
#pragma warning disable 649

    [Header("Needed UI objects")]
    [SerializeField]
    private GameObject upButtonObj;
    [SerializeField]
    private GameObject downButtonObj;

    private cardClass currentCardSelected;
#pragma warning restore 649

    private bool playingSound;

    private int[] currentDisplayedCardNums = { 0, 1, 2, 3 };

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                if (GameObject.FindObjectOfType(typeof(UIManager)) != null)
                {
                    instance = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;

                }
                else
                {
                    GameObject temp = new GameObject();
                    temp.name = "~UIManager";
                    temp.AddComponent<UIManager>();
                    temp.tag = "UIManager";
                    temp.isStatic = true;
                    DontDestroyOnLoad(temp);
                    instance = temp.GetComponent<UIManager>();

                }
            }
            return instance;
        }
    }

    /// <summary>
    /// Gets the up button gameobject
    /// </summary>
    public GameObject upButObj
    {
        get { return upButtonObj; }
    }

    /// <summary>
    /// Gets the down button gameobject
    /// </summary>
    public GameObject downButObj
    {
        get { return downButtonObj; }
    }

    /// <summary>
    /// Gets the current card selected
    /// </summary>
    public cardClass currentCardChosen
    {
        get { return currentCardSelected; }

        set { currentCardSelected= value; }
    }
    /// <summary>
    /// Selects the card, moves it up and plays its corresponding note sound clip
    /// </summary>
    public void selectCard(GameObject card)
    {
        AudioSource audioSource = GameMaster.Instance.gameObject.GetComponent<AudioSource>();
        card.transform.Translate(Vector3.forward);
        currentCardSelected = EventSystem.current.currentSelectedGameObject.GetComponent<cardClass>();
        audioSource.Stop();
        audioSource.PlayOneShot(currentCardSelected.noteAudioClips[currentCardSelected.cardNum]);
    }

    /// <summary>
    /// Place the card in a location
    /// </summary>
    public void placeCardOnScale(GameObject socket)
    {
        //fill data in this spot to correspond to data of dropped card

        socket.GetComponent<cardClass>().setCardData(currentCardSelected.cardNum,true);
       // EventSystem.current.currentSelectedGameObject.GetComponent<Image>().enabled = true;
        //EventSystem.current.currentSelectedGameObject.transform.position = new Vector3(EventSystem.current.currentSelectedGameObject.transform.position.x, EventSystem.current.currentSelectedGameObject.transform.position.y + (0.3f * currentCardSelected.cardNum), 0);


    }

    

    /// <summary>
    /// Play the song
    /// </summary>
    public void playSong()
    {
      GameMaster.Instance.getCurrentSong().gameObject.GetComponent<SongClass>().songClip();         
    }

    public void playSound(GameObject card)
    {
        if (!playingSound)
        {
          
            playingSound = true;
            soundPlayCheck(card);
            StartCoroutine(soundPlayCheck(card));
        }
        
    }

    private IEnumerator soundPlayCheck(GameObject card)
    {
        AudioClip clipToPlay = card.GetComponent<cardClass>().noteAudioClips[card.GetComponent<cardClass>().cardNum];
        GameMaster.Instance.gameObject.GetComponent<AudioSource>().PlayOneShot(card.GetComponent<cardClass>().noteAudioClips[card.GetComponent<cardClass>().cardNum]);

        yield return new WaitForSeconds(clipToPlay.length);
        
        playingSound = false;
        yield return null;
    }

    /// <summary>
    /// Rotate the cards up
    /// </summary>
    public void rotateCardsUp()
    {
        if (currentDisplayedCardNums[3] != GameMaster.Instance.songCardArray.Length-1)
        {
            for (int i = 0; i < currentDisplayedCardNums.Length; i++)
            {
                GameMaster.Instance.songCardArray[currentDisplayedCardNums[i]].SetActive(false);
                currentDisplayedCardNums[i] += 4;
                GameMaster.Instance.songCardArray[currentDisplayedCardNums[i]].SetActive(true);
                if (currentDisplayedCardNums[i] == GameMaster.Instance.songCardArray.Length)
                {
                    upButtonObj.SetActive(false);
                    if (downButtonObj.activeInHierarchy == false)
                    {
                        downButtonObj.SetActive(true);
                    }
                }
            }

        }
       
    }


    /// <summary>
    /// Rotate the cards down
    /// </summary>
    public void rotateCardsDown()
    {
        if (currentDisplayedCardNums[0] != 0)
        {
            for (int i = 0; i < currentDisplayedCardNums.Length; i++)
            {
                GameMaster.Instance.songCardArray[currentDisplayedCardNums[i]].SetActive(false);
                currentDisplayedCardNums[i] -= 4;
                GameMaster.Instance.songCardArray[currentDisplayedCardNums[i]].SetActive(true);
                if (currentDisplayedCardNums[i] == 0)
                {
                    downButtonObj.SetActive(false);
                    if (upButtonObj.activeInHierarchy == false)
                    {
                        upButtonObj.SetActive(true);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    public void playGame()
    {
        GameMaster.Instance.startGame();
    }
}
