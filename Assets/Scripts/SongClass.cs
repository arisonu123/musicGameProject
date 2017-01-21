using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongClass : MonoBehaviour {

   /* Queue<int> NoteQueue = new Queue<int>();
    NoteQueue.Enqueue(3); // Queue is '3'
    NoteQueue.Enqueue(7); // QUeue is '3 -> 7'
    NoteQueue.Enqueue(4); //Q is '3 -> 7 -> 4'
    int temp = NoteQueue.Dequeue(); // Q is 7 -> 4*/

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


