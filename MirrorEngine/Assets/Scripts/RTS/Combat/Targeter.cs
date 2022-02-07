using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Targeter : NetworkBehaviour
{
    #region Public Variables

    #endregion

    #region Private Variables
    [SerializeField] private Targatable _target;
    #endregion

    #region Unity Callbacks
    void Start()
    {

    }

    void Update()
    {

    }
    #endregion

    #region My Functions

    #region Server
    [Command]
    public void CmdSetTarget(GameObject targetObj)
    {
        if (!targetObj.TryGetComponent<Targatable>(out Targatable newTarget))
            return;

        this._target = newTarget;
    }

    [Server]
    public void ClearTarget() => _target = null;
    #endregion

    #region Client

    #endregion

    #endregion

    #region Coroutines

    #endregion

    #region Events

    #endregion
}