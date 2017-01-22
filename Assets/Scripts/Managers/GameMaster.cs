using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

#pragma warning disable 649
    private int currentLevel, songLength;
    [SerializeField]
    private SongClass activeSongNotes;
    [SerializeField]
    private GameObject activeSongObj;
    public bool soundCurrentlyPlaying = false;

    [SerializeField]
    [Header("Song List")]
    private GameObject[] songList; // List of all songs we have 

    [SerializeField]
    [Header("Placed Notes Array Object")]
    private GameObject UI;

    [SerializeField]
    [Header("Player Hand Object for Reference")]
    private GameObject playerHand;

    [SerializeField]
    [Header("Base song card slots, will be duplicated as needed")]
    private GameObject[] baseCardSlots;


    [SerializeField]
    private GameObject[] songCards;

#pragma warning disable 649
    [Header("Main menu and main game gameobjects ")]
    [SerializeField]
    [Tooltip("The main menu game object, activated and deactivated as neccessary")]
    private GameObject mainMenu;
    [SerializeField]
    [Tooltip("The main game GameObject, activated and deactivated as neccessary")]
    private GameObject mainGame;



#pragma warning restore 649

#pragma warning restore 649

    // Shaun got lazy and wanted easy access to this for returning cards to player hand
    // #spaghetti code for Global Game Jam
    public PlayerHand getPlayerHand()
    {
        return playerHand.GetComponent<PlayerHand>();
    }

    public Queue<int> NoteQueue;

    /// <summary>
    /// Creates a queue of notes to be shared between the players
    /// </summary>
    public void createNotePool()
    {
        int length = getSongNotes().getSongLength();

        int[] NewArray = new int[length];
        for (int i = 0; i < length; i++)
        {
            NewArray[i] = getSongNotes().getNote(i);
        }

        for (int i = 0; i < length; i++)
        {
            int index = Random.Range(0, length);

            int tmp = NewArray[i];
            NewArray[i] = NewArray[index];
            NewArray[index] = tmp;
        }

        NoteQueue = new Queue<int>();
        for (int i = 0; i < length; i++)
        {
            insertCard(NewArray[i]);
        }
    }
    /// <summary>
    /// Gives the next card in the "deck", removing it from the "pool"
    /// </summary>
    public int getNextCard()
    {
        return NoteQueue.Dequeue();
    }

    /// <summary>
    /// Places a card with value 'i' at the bottom of the "deck", returning it to the pool
    /// </summary>
    public void insertCard(int i)
    {
        NoteQueue.Enqueue(i);
    }

    public int queueSize()
    {
       return  NoteQueue.Count;
    }

    /// <summary>
    /// 
    /// </summary>
    public GameObject getCurrentSong()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("songObj"));
        
        if (activeSongObj==null)
        {
            activeSongObj = GameObject.Instantiate(songList[currentLevel]);
        }
     /*   activeSongObj = GameObject.Instantiate(songList[currentLevel]);
        GameObject[] songObjs = GameObject.FindGameObjectsWithTag("songObj");
        activeSongObj = songObjs[songObjs.Length - 1];
        for(int i = 0; i < songObjs.Length - 1; i++)
        {
            Destroy(songObjs[i]);
        }
        /* else if(!activeSongObj.name.Contains(currentLevel.ToString()))
         {
             activeSongObj = GameObject.Instantiate(songList[currentLevel]);
         }*/
        //activeSongObj = GameObject.Instantiate(songList[currentLevel]);
        //activeSongObj = GameObject.Instantiate(songList[currentLevel]);
        return activeSongObj;
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public AudioClip getSound()
    {
       return songList[currentLevel].GetComponent<SongClass>().noteSounds[0];
    
    }


    public SongClass getSongNotes()
    {
        return activeSongNotes;
    }

    /// <summary>
    /// Compares the notes of the song to the notes placed by the players
    /// </summary>
    public void compareNotes()
    {
        Debug.Log("Comparing Notes....");
        if (songCards.Length == songLength&&activeSongNotes!=null)
        {

            for (int i = 0; i < songLength; i++)
            {
               
                if (songCards[i].transform.childCount != 0)
                {
                    if (activeSongNotes.getNote(i) != songCards[i].transform.GetChild(0).GetComponent<cardClass>().cardNum)
                    {
                        onFail();
                    }


                    else if (i == songLength - 1)
                    {

                        onSuccess(); // Last one, all match, success!
                    }
                }
            }
        }
    }

    /// <summary>
    /// Loads next level if players successfully match notes
    /// </summary>
    private void onSuccess()
    {
        Debug.Log("fix siht");
        // Maybe some animation here??
        foreach (GameObject item in songCards)
        {
            if (item.transform.childCount != 0)
            {
                Destroy(item.transform.GetChild(0).gameObject);
            }
        }

        Destroy(activeSongObj);
        activeSongObj = null;
        
        currentLevel+=1;
        Debug.LogWarning(currentLevel + "is the current level");
        Debug.LogWarning("song list length is" + songList.Length);
        if (currentLevel < songList.Length) loadLevel();
        // Case for no more levels here
        else
        {
            toggleMenu();
        }

    }

    /// <summary>
    /// Resets level if players fail to match notes
    /// </summary>
    private void onFail()
    {
        Debug.Log("Level Failure... Resetting Level!");
        //Maybe some animation here??
        foreach(GameObject item in songCards)
        {
            if (item.transform.childCount != 0)
            {
                Destroy(item.transform.GetChild(0).gameObject);
            }
        }
        Destroy(activeSongObj);
        activeSongObj = null;
        loadLevel();
    }

    /// <summary>
    /// Loads level data (song, length)
    /// </summary>
    private void loadLevel()
    {

        GameMaster.instance.soundCurrentlyPlaying = false;
        activeSongNotes = getCurrentSong().GetComponent<SongClass>();

        //activeSongNotes = songList[currentLevel].GetComponent<SongClass>();
        songLength = activeSongNotes.getSongLength();

        UIManager.Instance.playSong();
        createNotePool(); // Sets up the pool of notes for players to use
        if (songCards.Length > 4)
        {
            for (int i = 4; i < songCards.Length; i++)
            {

                Destroy(songCards[i]);

            }
        }
              
        songCards = new GameObject[songLength];
        songCards[0] = baseCardSlots[0];
        songCards[1] = baseCardSlots[1];
        songCards[2] = baseCardSlots[2];
        songCards[3] = baseCardSlots[3];
        songCards[0].SetActive(true);
        songCards[1].SetActive(true);
        songCards[2].SetActive(true);
        songCards[3].SetActive(true);
        if (songCards.Length > 4)
        {
            Debug.Log("getting cards slots made");

            for (int i = 4; i < songCards.Length; i+=4)
            {
                Debug.Log("make stuff happen pls");
                songCards[i] = Instantiate(baseCardSlots[0], baseCardSlots[0].transform.position, Quaternion.identity) as GameObject;
                songCards[i].transform.SetParent(UI.transform);
                Destroy(songCards[i].transform.GetChild(0).gameObject);
                songCards[i].SetActive(false);
                songCards[i+1] = (GameObject)Instantiate(baseCardSlots[1], baseCardSlots[1].transform.position, Quaternion.identity);
                songCards[i+1].transform.SetParent(UI.transform);
                Destroy(songCards[i+1].transform.GetChild(0).gameObject);
                songCards[i + 1].SetActive(false);
                songCards[i+2] = (GameObject)Instantiate(baseCardSlots[2], baseCardSlots[2].transform.position, Quaternion.identity);
                songCards[i+2].transform.SetParent(UI.transform);
                Destroy(songCards[i+2].transform.GetChild(0).gameObject);
                songCards[i + 2].SetActive(false);
                songCards[i+3] = (GameObject)Instantiate(baseCardSlots[3], baseCardSlots[3].transform.position, Quaternion.identity);
                songCards[i+3].transform.SetParent(UI.transform);
                Destroy(songCards[i+3].transform.GetChild(0).gameObject);
                songCards[i + 3].SetActive(false);
            }
            UIManager.Instance.upButObj.SetActive(true);
            UIManager.Instance.downButObj.SetActive(false);
        }
   
        
    }

    private void initializeVariables()
    {
        currentLevel = 0;
        loadLevel();
    }

    private static GameMaster instance;

    public static GameMaster Instance
    {
        get
        {
            if (instance == null)
            {
                if (GameObject.FindObjectOfType(typeof(GameMaster)) != null)
                {
                    instance = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;

                }
                else
                {
                    GameObject temp = new GameObject();
                    temp.name = "~GameMaster";
                    temp.AddComponent<GameMaster>();
                    temp.tag = "GameMaster";
                    temp.isStatic = true;
                    DontDestroyOnLoad(temp);
                    instance = temp.GetComponent<GameMaster>();

                }
            }
            return instance;
        }
    }


    /// <summary>
    /// Array of cards currently placed by players
    /// </summary>
    public GameObject[] songCardArray
    {
        get { return songCards; }

        set { songCards = value; }
    }

    private bool allCardsPlaced()
    {
        if (songCardArray != null&&songLength!=0)
        {
            for (int i = 0; i < songLength; i++)
            {

                if (songCardArray[i].transform.childCount == 0)
                {
                    //Debug.Log("Returnin false to all cards placed");
                    /*if (songCardArray[i] == null)*/ return false;
                }
            }
            Debug.Log("All notes placed!");
            return true;
        }
        return false;
    }

    // Use this for initialization
    private void Start () {
        //initializeVariables();
	
	}
	
	// Update is called once per frame
	private void Update () {
        if (allCardsPlaced()) compareNotes();
	}

    private void toggleMenu()
    {
        Debug.LogWarning("toggles");
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        mainGame.SetActive(!mainGame.activeInHierarchy);

        //fill song card array
        if (mainGame.activeInHierarchy)
        {
            initializeVariables();
            songCards = GameObject.FindGameObjectsWithTag("scaleSocket");
            if(activeSongObj)
            if (songCards.Length == 4)
            {
                UIManager.Instance.upButObj.SetActive(false);
                
            }
            UIManager.Instance.downButObj.SetActive(false);
        }
    }

    /// <summary>
    /// Start the game, disabled and enable game objects as needed, load song up
    /// </summary>
    public void startGame()
    {

        toggleMenu();

        //TODO:load song up 
    }

  
}
