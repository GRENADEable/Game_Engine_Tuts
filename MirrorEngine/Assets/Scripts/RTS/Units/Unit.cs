using System;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class Unit : NetworkBehaviour
{
    #region Serialized Variables
    [SerializeField] private UnityEvent OnSelected = default;
    [SerializeField] private UnityEvent OnDeselected = default;

    [SerializeField] private UnitMovement unitMovement = default;
    public UnitMovement GetUnitMovement() => unitMovement;

    //[SerializeField] private Targeter targeter = default;
    //public Targeter GetTargeter() => targeter;

    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;

    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDespawned;
    #endregion

    #region My Functions

    #region Server
    public override void OnStartServer() => ServerOnUnitSpawned?.Invoke(this);

    public override void OnStopServer() => ServerOnUnitDespawned?.Invoke(this);
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

        OnSelected?.Invoke();
    }

    [Client]
    public void DeSelect()
    {
        if (!hasAuthority)
            return;

        OnDeselected?.Invoke();
    }
    #endregion

    #endregion
}