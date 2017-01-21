using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string roomName = "RoomName";
	private RoomInfo[] roomsList;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	// Update is called once per frame
	void Update () {
		if (!PhotonNetwork.connected)
		{
			//GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if (PhotonNetwork.room == null)
		{
			// Create Room
				PhotonNetwork.CreateRoom(roomName);

			// Join Room
			if (roomsList != null)
			{
				for (int i = 0; i < roomsList.Length; i++)
				{
						PhotonNetwork.JoinRoom(roomsList[i].Name);
				}
			}
		}
	}

	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
	}

	void OnJoinedRoom()
	{
		Debug.Log("Connected to Room");
	}
}
