using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    #region Private Variables
    [SerializeField] private CreateRoomPanel _createRoomPanel = default;
    [SerializeField] private RoomListingPanel _roomListingPanel = default;
    private RoomsCanvases _roomsCanvases;
    #endregion

    #region My Functions
    public void Intialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
        _createRoomPanel.Intialize(canvases);
        _roomListingPanel.Intialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion
}