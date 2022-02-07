using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class UnitMovement : NetworkBehaviour
{
    #region Seralized Variables
    [SerializeField] private NavMeshAgent playerAgent = default;
    //public Targeter targeter;
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
        //targeter.ClearTarget();

        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            return;

        playerAgent.SetDestination(hit.position);
    }
    #endregion

    #endregion
}