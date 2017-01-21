using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    private int currentLevel, songLength;
    private SongClass activeSongNotes;
    private UIManager placedNotes;

    [SerializeField]
    [Header("Song List")]
    private GameObject[] songList; // List of all songs we have 

    [SerializeField]
    [Header("Placed Notes Array Object")]
    private GameObject UI;

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public GameObject getSong()
    {
        return songList[currentLevel];
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
            if (activeSongNotes.getNote(i) != placedNotes.songCardArray[i].GetComponent<cardClass>().cardNum)
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
        if(currentLevel < songList.Length) loadLevel();
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
    }

    private void initializeVariables()
    {
        currentLevel = 0;
        placedNotes = UI.GetComponent<UIManager>();
        loadLevel(); 
    }

    // Use this for initialization
    private void Start () {
        initializeVariables();
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

    private void startGame()
    {

    }
}
