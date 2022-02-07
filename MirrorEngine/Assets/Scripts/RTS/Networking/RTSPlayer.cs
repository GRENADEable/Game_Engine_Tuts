using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RTSPlayer : NetworkBehaviour
{
    #region Public Variables

    #endregion

    #region Private Variables
    [SerializeField] private List<Unit> myUnits = new List<Unit>();

    public List<Unit> GetMyUnits() => myUnits;
    #endregion

    #region My Functions

    #region Server
    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += ServerOnUnitSpawnedEventReceived;
        Unit.ServerOnUnitDespawned += ServerOnUnitDeSpawnedEventReceived;
    }

    public override void OnStopServer()
    {
        Unit.ServerOnUnitSpawned -= ServerOnUnitSpawnedEventReceived;
        Unit.ServerOnUnitDespawned -= ServerOnUnitDeSpawnedEventReceived;
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        if (!isClientOnly)
            return;

        Unit.AuthorityOnUnitSpawned += AuthorityServerOnUnitSpawnedEventReceived;
        Unit.AuthorityOnUnitDespawned += AuthorityServerOnUnitDeSpawnedEventReceived;
    }

    public override void OnStopClient()
    {
        if (!isClientOnly)
            return;

        Unit.AuthorityOnUnitSpawned -= AuthorityServerOnUnitSpawnedEventReceived;
        Unit.AuthorityOnUnitDespawned -= AuthorityServerOnUnitDeSpawnedEventReceived;
    }
    #endregion

    #endregion

    #region Events

    #region Server
    void ServerOnUnitSpawnedEventReceived(Unit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId)
            return;

        myUnits.Add(unit);
    }

    void ServerOnUnitDeSpawnedEventReceived(Unit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId)
            return;

        myUnits.Remove(unit);
    }
    #endregion

    #region Client
    void AuthorityServerOnUnitSpawnedEventReceived(Unit unit)
    {
        if (!hasAuthority)
            return;

        myUnits.Add(unit);
    }

    void AuthorityServerOnUnitDeSpawnedEventReceived(Unit unit)
    {
        if (!hasAuthority)
            return;

        myUnits.Remove(unit);
    }
    #endregion

    #endregion
}
