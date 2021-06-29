using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _text = default;
    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }

    public void OnClickJoin()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}