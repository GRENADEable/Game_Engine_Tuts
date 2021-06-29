using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MyNetworkManager : MonoBehaviourPunCallbacks
{
    #region Public Variables
    [Space, Header("Panels")]
    public GameObject usernamePanel;
    public GameObject loadingPanel;

    [Space, Header("UI Text References")]
    public TextMeshProUGUI debugText;
    public TMP_InputField playerNickName;
    #endregion

    #region Private Variables
    private RoomsCanvases _roomsCanvases;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        playerNickName.characterLimit = 16;
    }
    #endregion

    #region Photon Callbacks

    #region Server
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        debugText.text = "Connected to master";

        loadingPanel.SetActive(false);
        _roomsCanvases.LobbyCanvas.Show();

        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected because: {cause}");
        debugText.text = $"Disconnected because: {cause}";
    }
    #endregion

    #endregion

    #region My Functions
    public void Intialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    public void OnClickSetUsername()
    {
        if (!string.IsNullOrWhiteSpace(playerNickName.text))
        {
            StartServer();
            PhotonNetwork.NickName = playerNickName.text.ToString();
            //Debug.Log(PhotonNetwork.NickName);
            usernamePanel.SetActive(false);
            loadingPanel.SetActive(true);
        }
    }

    void StartServer()
    {
        Debug.Log("Connecting to server");
        debugText.text = "Connecting to server";

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.MyGameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion
}