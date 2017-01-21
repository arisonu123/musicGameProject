using UnityEngine;
using System.Collections;

public class SongClass : MonoBehaviour {

	[SerializeField]
	[Header("Song File")]
	private AudioClip song;

	/// <summary>
	/// Gets the song clip associated with this class
	/// </summary>
	/// <value>The song clip.</value>
	public AudioClip songClip{
		get{ return song;}
	}

	[SerializeField]
	[Header("Song Notes")]
	private int[] notes;

	/// <summary>
	/// Gets the note value in the select portion of the song (0 = first)
	/// </summary>
	/// <value>The song clip.</value>
	public int getNote(int position)
	{
		return notes[position];
	}

	public int getSongLength()
	{
		return notes.Length;
	}
}


