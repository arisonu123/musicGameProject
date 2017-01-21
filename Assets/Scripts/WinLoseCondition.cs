using UnityEngine;
using System.Collections;

public class WinLoseCondition : MonoBehaviour {
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

		for(int i = 0; i < songLength; i++)
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
