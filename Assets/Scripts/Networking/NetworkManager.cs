using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject progessPanel;
    [SerializeField] GameObject lobbyPanel;
    [SerializeField] TMP_InputField createRoom;
    [SerializeField] TMP_InputField joinRoom;


    string gameVersion = "1";
    bool isConnecting;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void Start()
    {
        progessPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
    public void JoinRoom()
    {
        progessPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRoom(joinRoom.text);
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    
    public void CreateRoom()
    {
        progessPanel.SetActive(true);
        lobbyPanel.SetActive(false);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(createRoom.text, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Scene 1");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progessPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        isConnecting = false;
        Debug.LogWarningFormat("OnDisconnected()", cause);
    }
}
