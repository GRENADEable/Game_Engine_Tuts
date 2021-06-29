using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class JoinedPlayerListingPanel : MonoBehaviourPunCallbacks
{
    #region Private Variables
    [SerializeField] private JoinedPlayerListing _joinedPlayerListingPrefab = default;
    [SerializeField] private Transform _content = default;
    [SerializeField] private TextMeshProUGUI _readUpText = default;

    private List<JoinedPlayerListing> _listings = new List<JoinedPlayerListing>();
    private RoomsCanvases _roomsCanvases = default;
    private bool _isReady = false;
    #endregion

    #region Pun Callbacks
    public override void OnEnable()
    {
        base.OnEnable();

        GetCurrentRoomPlayers();
        SetReadyUp(false);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        for (int i = 0; i < _listings.Count; i++)
            Destroy(_listings[i].gameObject);

        _listings.Clear();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _roomsCanvases.JoinedRoomCanvas.LeaveRoomButton.OnClickLeaveRoom();
    }
    #endregion

    #region My Functions
    public void Intialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    public void OnClickStartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < _listings.Count; i++)
            {
                if (_listings[i].Player != PhotonNetwork.LocalPlayer)
                {
                    if (!_listings[i].isPlayerReady)
                        return;
                }
            }

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel("MainScene");
        }
    }

    public void OnClickReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_isReady);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _isReady);
        }
    }

    void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;

        foreach (KeyValuePair<int, Player> playerinfo in PhotonNetwork.CurrentRoom.Players)
            AddPlayerListing(playerinfo.Value);
    }

    void AddPlayerListing(Player player)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
            _listings[index].SetPlayerInfo(player);
        else
        {
            JoinedPlayerListing listing = Instantiate(_joinedPlayerListingPrefab, _content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }
        }
    }

    void SetReadyUp(bool state)
    {
        _isReady = state;

        if (_isReady)
            _readUpText.text = "Ready";
        else
            _readUpText.text = "Not Ready";
    }

    [PunRPC]
    void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
            _listings[index].isPlayerReady = ready;
    }
    #endregion
}
