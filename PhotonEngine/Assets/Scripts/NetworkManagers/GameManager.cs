using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    #region Private Variables
    [SerializeField] private GameObject _playerPrefab = default;
    [SerializeField] private Transform[] _playerPosition = default;
    #endregion

    void Awake()
    {
        if (PhotonNetwork.IsConnected)
            MasterManager.NetworkInstantiate(_playerPrefab, _playerPosition[Random.Range(0, _playerPosition.Length)].position, Quaternion.identity);
    }
}