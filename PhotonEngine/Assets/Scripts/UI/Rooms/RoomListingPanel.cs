using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingPanel : MonoBehaviourPunCallbacks
{
    #region Private Variables
    [SerializeField] private RoomListing _roomListingPrefab = default;
    [SerializeField] private Transform _content = default;

    private List<RoomListing> _listings = new List<RoomListing>();
    private RoomsCanvases _roomsCanvases;
    #endregion

    #region Pun Callbacks
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList) // Removed from rooms list
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else // Added to rooms list
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);

                if (index == -1)
                {
                    RoomListing listing = Instantiate(_roomListingPrefab, _content);
                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        _listings.Add(listing);
                    }
                }
                else // Modify listings
                {
                    // _listings[index].DoWhatever
                }
            }
        }
    }

    public override void OnJoinedRoom()
    {
        _roomsCanvases.JoinedRoomCanvas.Show();
        _content.DestroyChildren();
        _listings.Clear();
    }
    #endregion

    #region My Functions
    public void Intialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }
    #endregion
}