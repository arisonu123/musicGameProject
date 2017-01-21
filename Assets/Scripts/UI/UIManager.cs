using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    private GameObject[] songCards;

    /// <summary>
    /// Array of cards
    /// </summary>
    public GameObject[] songCardArray
    {
        get { return songCards; }

        set { songCards = value; }
    }

    private int[] currentDisplayedCardNums = { 0, 1, 2, 3 };
	// Use this for initialization
	private void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {
	
	}

    public void dragCard()
    {

    }

    public void dropCard()
    {
        //fill data in this spot to correspond to data of dropped card


    }

    /// <summary>
    /// Play the song
    /// </summary>
    public void playSong()
    {
      //  GameObject.FindWithTag("songObj").GetComponent<SongClass>().song.PlayClip();
    }

    /// <summary>
    /// Replay the level
    /// </summary>
    public void replayLevel()
    {
        //replay level
    }

    /// <summary>
    /// Rotate the cards up
    /// </summary>
    public void rotateCardsUp()
    {
        for(int i = 0; i < currentDisplayedCardNums.Length; i++)
        {
            songCards[currentDisplayedCardNums[i]].SetActive(false);
            currentDisplayedCardNums[i] += 1;
            songCards[currentDisplayedCardNums[i]].SetActive(true);
        }     
       
    }


    /// <summary>
    /// Rotate the cards down
    /// </summary>
    public void rotateCardsDown()
    {
        for (int i = 0; i < currentDisplayedCardNums.Length; i++)
        {
            songCards[currentDisplayedCardNums[i]].SetActive(false);
            currentDisplayedCardNums[i] -= 1;
            songCards[currentDisplayedCardNums[i]].SetActive(true);
        }
    }
}
