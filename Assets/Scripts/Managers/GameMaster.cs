using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    private static GameMaster instance;


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
}
