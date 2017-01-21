using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongClass : MonoBehaviour {

#pragma warning disable 649
    [SerializeField]
    [Header("Note Sounds")]
    public AudioClip[] noteSounds;

    [SerializeField]
    [Header("Song Tempo (Lower is Faster)")]
    private float songTempo;

    /// <summary>
    /// Play the song clip associated with this class
    /// </summary>
    /// <value>The song clip.</value>
    public void songClip()
    {
        // Possible to change to having a "timestamp" of sorts if we have time
        if (!GameMaster.Instance.soundCurrentlyPlaying)
        {
            GameMaster.Instance.soundCurrentlyPlaying = true;
            StartCoroutine(playSongClip());
        }
    }

    IEnumerator playSongClip() // Helper function (coroutine)
    {
        AudioSource soundSource = GameMaster.Instance.gameObject.GetComponent<AudioSource>();
        // COntinue playing through all notes, stop if the player selects a card
        for (int i = 0; i < notes.Length && GameMaster.Instance.soundCurrentlyPlaying; i++)
        {
            soundSource.Stop();
            soundSource.PlayOneShot(noteSounds[getNote(i)], 1.0f);
            yield return new WaitForSeconds(songTempo);
        }
        GameMaster.Instance.soundCurrentlyPlaying = false;
        yield return null;
    }

	[SerializeField]
	[Header("Song Notes")]
	private int[] notes;
#pragma warning restore 649
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


