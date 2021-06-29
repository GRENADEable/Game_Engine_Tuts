using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class UnitCommandGiver : MonoBehaviour
{
    #region Public Variables
    public UnitSelectionHandler unitSelection;
    public LayerMask mouseLayer;
    #endregion

    #region Private Variables
    private Camera _cam;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame)
            return;

        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mouseLayer))
            return;

        TryMove(hit.point);
    }
    #endregion

    #region My Functions

    #region Server

    #endregion

    #region Client
    void TryMove(Vector3 point)
    {
        foreach (Unit selectedUnit in unitSelection.SelectedUnits)
            selectedUnit.GetUnitMovement().CmdMove(point);
    }
    #endregion

    #endregion
}
