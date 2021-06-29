using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;
using System;

public class PlayerController : NetworkBehaviour
{
    #region Public Variables
    public NavMeshAgent playerAgent;
    #endregion

    #region Private Variables
    private Camera _cam;
    #endregion

    #region My Functions

    #region Server
    [Command]
    void CmdMove(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            return;

        playerAgent.SetDestination(hit.position);
    }
    #endregion

    #region Client
    public override void OnStartAuthority()
    {
        _cam = Camera.main;
    }

    [ClientCallback]
    void Update()
    {
        if (!hasAuthority)
            return;

        if (!Input.GetMouseButtonDown(1))
            return;

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            return;

        CmdMove(hit.point);
    }
    #endregion

    #endregion
}