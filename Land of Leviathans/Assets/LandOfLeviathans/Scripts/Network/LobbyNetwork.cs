using UnityEngine;

public class LobbyNetwork : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Connection to Server.. ");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
	}

    private void OnConnectedToMaster()
    {
        print("Connected to Master.");
        PhotonNetwork.playerName = PlayerNetwork.instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    private void OnJoinedLobby()
    {
        print("Joined Lobby");
    }
	

}
