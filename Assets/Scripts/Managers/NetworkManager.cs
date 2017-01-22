using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	string _room = "backseatDriver";

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void OnJoinedLobby()
	{

		RoomOptions roomOptions = new RoomOptions() { };
		PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom(){
		print ("Joined" + _room);
	}

}
