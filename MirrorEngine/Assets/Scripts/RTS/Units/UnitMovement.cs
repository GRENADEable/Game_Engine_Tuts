using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class UnitMovement : NetworkBehaviour
{
    #region Public Variables
    public NavMeshAgent playerAgent;
    #endregion

    #region My Functions

    #region Server
    [ServerCallback]
    void Update()
    {
        if (!playerAgent.hasPath)
            return;

        if (playerAgent.remainingDistance > playerAgent.stoppingDistance)
            return;

        playerAgent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            return;

        playerAgent.SetDestination(hit.position);
    }
    #endregion

    #endregion
}