using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

#pragma warning disable 649
    private int currentLevel, songLength;
    private SongClass activeSongNotes;
    private GameObject activeSongObj;
    public bool soundCurrentlyPlaying = false;

    [SerializeField]
    [Header("Song List")]
    private GameObject[] songList; // List of all songs we have 

    [SerializeField]
    [Header("Placed Notes Array Object")]
    private GameObject UI;
#pragma warning restore 649

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
        if (activeSongObj == null)
        {
            activeSongObj = GameObject.Instantiate(songList[currentLevel]);
        }
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
        for (int i = 0; i < songLength; i++)
        {
            if (activeSongNotes.getNote(i) != songCardArray[i].GetComponent<cardClass>().cardNum)
            {
                onFail();
            }

            else if (i == songLength - 1)
            {
                onSuccess(); // Last one, all match, success!
            }
        }
    }

    /// <summary>
    /// Loads next level if players successfully match notes
    /// </summary>
    private void onSuccess()
    {
        // Maybe some animation here??
        currentLevel++;
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
        //Maybe some animation here??
        loadLevel();
    }

    /// <summary>
    /// Loads level data (song, length)
    /// </summary>
    private void loadLevel()
    {
        activeSongNotes = songList[currentLevel].GetComponent<SongClass>();
        songLength = activeSongNotes.getSongLength();
        createNotePool(); // Sets up the pool of notes for players to use
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

    /// <summary>
    /// Array of cards currently placed by players
    /// </summary>
    public GameObject[] songCardArray
    {
        get { return songCards; }

        set { songCards = value; }
    }

    // Use this for initialization
    private void Start () {
        initializeVariables();
	
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

    private void toggleMenu()
    {
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        mainGame.SetActive(!mainGame.activeInHierarchy);
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
