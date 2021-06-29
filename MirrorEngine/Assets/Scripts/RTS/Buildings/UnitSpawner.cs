using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    #region Public Variables
    public GameObject unitPrefab;
    public Transform unitSpawn;
    #endregion

    #region My Functions

    #region Server
    [Command]
    void CmdSpawnUnit()
    {
        GameObject unitObj = Instantiate(unitPrefab, unitSpawn.position, unitSpawn.rotation);

        NetworkServer.Spawn(unitObj, connectionToClient);
    }
    #endregion

    #region Client
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (!hasAuthority)
            return;

        CmdSpawnUnit();
    }
    #endregion

    #endregion
}