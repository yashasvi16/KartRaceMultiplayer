using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;

        if (Instance == null)
            Instance = this;

        if(PlayerManager.localInstance == null)
        {
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(Random.Range(-3.5f, 3.5f), 1, 0), Quaternion.identity, 0);
        }
    }

    private void LoadArena()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        PhotonNetwork.LoadLevel("Scene 1");
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(1);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }
    }


}
