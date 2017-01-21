using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

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

    /// <summary>
    /// Array of cards
    /// </summary>
    public GameObject[] songCardArray
    {
        get { return songCards; }

        set { songCards = value; }
    }

    // Use this for initialization
    private void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

    private void startGame()
    {

    }


#pragma warning disable 649
    //The following would be in scene manager, probably with these functions as well
    int currentLevel; // For referencing songList active song
    GameObject[] songList; // List of all songs we have 
    GameObject UI;
    SongClass activeSongNotes;
    UIManager placedNotes;
#pragma warning restore 649
    private void setNoteReferences()
    {
        activeSongNotes = songList[currentLevel].GetComponent<SongClass>();
        placedNotes = UI.GetComponent<UIManager>();
    }

    /// <summary>
    /// Compares the notes of the song to the notes placed by the players
    /// </summary>
    public void compareNotes()
    {
        int songLength = activeSongNotes.getSongLength();

        for (int i = 0; i < songLength; i++)
        {
            if (activeSongNotes.getNote(i) != songCards[i].GetComponent<cardClass>().cardNum)
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
        // Load data for next level (song, length)
        // Adjust UI as needed

    }

    /// <summary>
    /// Resets level if players fail to match notes
    /// </summary>
    private void onFail()
    {
        // Reset level, return cards to pool and redistribute to players

    }
}
