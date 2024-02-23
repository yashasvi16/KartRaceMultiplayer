using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameObject localInstance;
    public GameObject playerUIPrefab;

#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
#endif

    void Awake()
    {
        if(photonView.IsMine)
        {
            PlayerManager.localInstance = this.gameObject;
        }        
    }

    private void Start()
    {
        CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
        if (_cameraWork != null)
        {
            _cameraWork.OnStartFollowing();
        }
#if UNITY_5_4_OR_NEWER
        // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
        if (playerUIPrefab != null)
        {
            GameObject _ui = Instantiate(playerUIPrefab);
            _ui.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);

        }
    }
#if UNITY_5_4_OR_NEWER
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
#endif

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(0);
        }
    }

#if !UNITY_5_4_OR_NEWER
/// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
void OnLevelWasLoaded(int level)
{
    this.CalledOnLevelWasLoaded(level);
}
#endif
    void CalledOnLevelWasLoaded(int level)
    {
        GameObject _uiGo = Instantiate(playerUIPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }

}
