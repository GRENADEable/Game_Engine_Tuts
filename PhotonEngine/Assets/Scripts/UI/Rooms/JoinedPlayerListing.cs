using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class JoinedPlayerListing : MonoBehaviourPunCallbacks
{
    #region Private Variabels
    [SerializeField] private TextMeshProUGUI _text = default;
    public Player Player { get; private set; }
    public bool isPlayerReady = false;
    #endregion

    #region PUN Callbacks
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        if (targetPlayer != null && targetPlayer == Player)
        {
            if (changedProps.ContainsKey("RandomNumber"))
                SetPlayerText(targetPlayer);
        }
    }
    #endregion

    #region My Functions
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        SetPlayerText(player);
    }

    void SetPlayerText(Player player)
    {
        int result = -1;
        if (player.CustomProperties.ContainsKey("RandomNumber"))
            result = (int)player.CustomProperties["RandomNumber"];

        _text.text = result.ToString() + ", " + player.NickName;
    }
    #endregion
}