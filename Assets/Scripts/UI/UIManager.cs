using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
#pragma warning disable 649



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
    public void placeCardOnScale()
    {
        //fill data in this spot to correspond to data of dropped card

        EventSystem.current.currentSelectedGameObject.GetComponent<cardClass>().setCardData(currentCardSelected.cardNum,true);
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().enabled = true;
        EventSystem.current.currentSelectedGameObject.transform.position = new Vector3(EventSystem.current.currentSelectedGameObject.transform.position.x, EventSystem.current.currentSelectedGameObject.transform.position.y + (0.3f * currentCardSelected.cardNum), 0);


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
           // GameMaster.Instance.gameObject.GetComponent<AudioSource>().PlayOneShot(card.GetComponent<cardClass>().noteAudioClips[card.GetComponent<cardClass>().cardNum]);
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

    /// <summary>
    /// Starts the game
    /// </summary>
    public void playGame()
    {
        GameMaster.Instance.startGame();
    }
}
