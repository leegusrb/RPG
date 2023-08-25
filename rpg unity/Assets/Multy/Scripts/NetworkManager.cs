using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public GameObject DisconnectPanel;
    public GameObject RespwanPanel;
    public GameObject ConnectPanel;
    public GameObject InGameUI;
    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null);
    }

    public override void OnJoinedRoom()
    {
        DisconnectPanel.SetActive(false);
        ConnectPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
    }

    public void Spawn(string character)
    {
        GameObject player = PhotonNetwork.Instantiate("Character/" + character, Vector3.zero, Quaternion.identity);
        ConnectPanel.SetActive(false);
        RespwanPanel.SetActive(false);
        InGameUI.SetActive(true);
        InGameUI.GetComponent<InGameUI>().myCharacter = player;
        InGameUI.GetComponent<InGameUI>().setUp();
        
    }
        

    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPanel.SetActive(true);
        RespwanPanel.SetActive(false);
    }
}