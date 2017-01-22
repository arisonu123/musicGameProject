using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

#pragma warning disable 649
    private int currentLevel, songLength;
    private SongClass activeSongNotes;
    public bool soundCurrentlyPlaying = false;

    [SerializeField]
    [Header("Song List")]
    private GameObject[] songList; // List of all songs we have 

    [SerializeField]
    [Header("Placed Notes Array Object")]
    private GameObject UI;
#pragma warning restore 649

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
     //   activeSongNotes = songList[currentLevel].GetComponent<SongClass>();
        //songLength = activeSongNotes.getSongLength();
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
