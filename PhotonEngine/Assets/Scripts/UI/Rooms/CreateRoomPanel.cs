using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateRoomPanel : MonoBehaviourPunCallbacks
{
    #region Public Variables
    public TextMeshProUGUI debugText;
    #endregion

    #region Private Variables
    [SerializeField] private TMP_InputField _roomName = default;
    private RoomsCanvases _roomsCanvases;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _roomName.characterLimit = 10;
    }
    #endregion

    #region My Functions

    #region Photon Callbacks
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room Successfully");
        debugText.text = "Created Room Successfully";
        _roomsCanvases.JoinedRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"Room Creation Failed: {message}");
        debugText.text = $"Room Creation Failed: {message}";
    }
    #endregion

    #region UI
    public void Intialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    public void OnClickCreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        if (!string.IsNullOrWhiteSpace(_roomName.text) && _roomName.characterLimit <= 10)
        {
            RoomOptions options = new RoomOptions();
            options.BroadcastPropsChangeToAll = true;
            options.MaxPlayers = 4;
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
        }
    }
    #endregion

    #endregion
}