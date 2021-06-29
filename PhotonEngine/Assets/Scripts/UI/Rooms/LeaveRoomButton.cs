using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LeaveRoomButton : MonoBehaviour
{
    #region Private Variables
    private RoomsCanvases _roomsCanvases;
    #endregion

    #region My Functions
    public void Intialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        _roomsCanvases.JoinedRoomCanvas.Hide();
    }
    #endregion
}