using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JoinedRoomCanvas : MonoBehaviour
{
    #region Private Variables
    [SerializeField] private JoinedPlayerListingPanel _joinedPlayerListingPanel = default;
    [SerializeField] private GameObject _startGameButton = default;
    [SerializeField] private GameObject _readyPlayerButton = default;
    [SerializeField] private LeaveRoomButton _leaveRoomButton = default;
    public LeaveRoomButton LeaveRoomButton { get { return _leaveRoomButton; } }
    private RoomsCanvases _roomsCanvases;
    #endregion

    #region My Functions
    public void Intialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
        _joinedPlayerListingPanel.Intialize(canvases);
        _leaveRoomButton.Intialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
            _startGameButton.SetActive(true);
            _readyPlayerButton.SetActive(false);
        }
        else
        {
            _startGameButton.SetActive(false);
            _readyPlayerButton.SetActive(true);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion
}