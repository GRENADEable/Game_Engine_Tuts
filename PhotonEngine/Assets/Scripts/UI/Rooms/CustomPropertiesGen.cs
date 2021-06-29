using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CustomPropertiesGen : MonoBehaviour
{
    #region Private Variables
    [SerializeField] private TextMeshProUGUI _customPropertiesGenText = default;
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    #endregion

    #region My Functions
    public void OnClickRandomGenerator()
    {
        SetCustomNumber();
    }

    void SetCustomNumber()
    {
        System.Random rnd = new System.Random();
        int result = rnd.Next(0, 99);

        _customPropertiesGenText.text = result.ToString();

        _myCustomProperties["RandomNumber"] = result;
        //PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
    }
    #endregion
}