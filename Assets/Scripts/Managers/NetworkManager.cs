using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string roomName = "RoomName";
	private RoomInfo[] roomsList;

	bool joined;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
	}

	void OnJoinedLobby(){
		//create room
		if (PhotonNetwork.room == null) {
			PhotonNetwork.CreateRoom (roomName);
		}

		// Join Room
		if (roomsList != null) {
			for (int i = 0; i < roomsList.Length; i++) {
				PhotonNetwork.JoinRoom (roomsList [i].Name);
			}

		}
	}

	void OnJoinedRoom()
	{
		Debug.Log("Connected to Room");
	}
}
