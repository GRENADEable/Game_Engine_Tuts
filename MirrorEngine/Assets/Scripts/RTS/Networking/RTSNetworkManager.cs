﻿using UnityEngine;
using Mirror;

public class RTSNetworkManager : NetworkManager
{
    [SerializeField] private GameObject unitSpawnerPrefab = default;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        GameObject unitSpawnerObj = Instantiate(unitSpawnerPrefab, conn.identity.transform.position, conn.identity.transform.rotation);

        NetworkServer.Spawn(unitSpawnerObj, conn);
    }
}