using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCommandGiver : MonoBehaviour
{
    #region Public Variables
    [SerializeField] private LayerMask mouseLayer = default;
    [SerializeField] private UnitSelectionHandler unitSelection = default;
    #endregion

    #region Private Variables
    private Camera _cam;
    #endregion

    #region Unity Callbacks
    void Start() => _cam = Camera.main;

    void Update()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame)
            return;

        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mouseLayer))
            return;

        //if (hit.collider.TryGetComponent<Targatable>(out Targatable target))
        //{
        //    if (target.hasAuthority)
        //    {
        //        TryMove(hit.point);
        //        return;
        //    }

        //    TryTarget(target);
        //    return;
        //}

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

    //void TryTarget(Targatable target)
    //{
    //    foreach (Unit selectedUnit in unitSelection.SelectedUnits)
    //        selectedUnit.GetTargeter().CmdSetTarget(target.gameObject);
    //}
    #endregion

    #endregion
}
