using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class InputPlayerName : MonoBehaviour
{
    const string playerName = "PlayerName";

    private void Start()
    {
        string defaultName = string.Empty;
        TMP_InputField _inputField = GetComponent<TMP_InputField>();

        if(_inputField != null )
        {
            if(PlayerPrefs.HasKey(playerName))
            {
                defaultName = PlayerPrefs.GetString(playerName);
                _inputField.name = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerName, value);
    }
}
