using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class Unit : NetworkBehaviour
{
    #region Public Variables
    public UnityEvent onSelected;
    public UnityEvent onDeselected;

    public UnitMovement unitMovement;
    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }

    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;

    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDespawned;
    #endregion

    #region My Functions

    #region Server
    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        if (!isClientOnly || !hasAuthority)
            return;

        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        if (!isClientOnly || !hasAuthority)
            return;

        AuthorityOnUnitDespawned?.Invoke(this);
    }

    [Client]
    public void Select()
    {
        if (!hasAuthority)
            return;

        onSelected?.Invoke();
    }

    [Client]
    public void DeSelect()
    {
        if (!hasAuthority)
            return;

        onDeselected?.Invoke();
    }
    #endregion

    #endregion
}