using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text LogText;
    string[] mapScen=new string[]{ "battle01", "Map2","Map3","Map4","Map5","Map6"};
    void Start()
    {
        //ShowElementPhoton();
        PhotonNetwork.NickName = PlayerPrefs.GetString("Name");
        Log("Player's name is set to " + PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        /* AutomaticallySyncScene нужен для того если на одном клиенте переключалась сцена то и у других она тоже переключалась.*/
        PhotonNetwork.GameVersion = "1";//присваиваем версию кода (Project settings=>Player=>Other settings=>version)
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnConnectedToMaster()
    {
        Log("Connected to master");
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
        Debug.Log("Create_Room");
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        Log("joined the room");
        PhotonNetwork.LoadLevel(mapScen[0]);//выбераем карту а не сцену
    }
    private void Log(string message)
    {
        Debug.Log("LobbyManager:_"+message);
        LogText.text += "\n";
        LogText.text += message;
    }
}
