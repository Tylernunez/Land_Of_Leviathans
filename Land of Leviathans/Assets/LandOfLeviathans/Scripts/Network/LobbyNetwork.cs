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
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    private void OnJoinedLobby()
    {
        print("Joined Lobby");
        if (!PhotonNetwork.inRoom)
        {
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
        }
        
    }
	

}
