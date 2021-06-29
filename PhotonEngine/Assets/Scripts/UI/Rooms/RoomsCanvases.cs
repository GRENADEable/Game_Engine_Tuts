using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    #region Private Variables
    [SerializeField] LobbyCanvas _lobbyCanvas = default;
    public LobbyCanvas LobbyCanvas { get { return _lobbyCanvas; } }

    [SerializeField] JoinedRoomCanvas _joinedRoomCanvas = default;
    public JoinedRoomCanvas JoinedRoomCanvas { get { return _joinedRoomCanvas; } }

    [SerializeField] MyNetworkManager _myNetworkManager = default;
    public MyNetworkManager MyNetworkManager { get { return _myNetworkManager; } }
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        Intialize();
    }
    #endregion

    #region My Functions
    void Intialize()
    {
        LobbyCanvas.Intialize(this);
        JoinedRoomCanvas.Intialize(this);
        MyNetworkManager.Intialize(this);
    }
    #endregion
}