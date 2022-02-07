using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    #region Public Variables
    [SerializeField] private GameObject unitPrefab = default;
    [SerializeField] private Transform unitSpawn = default;
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