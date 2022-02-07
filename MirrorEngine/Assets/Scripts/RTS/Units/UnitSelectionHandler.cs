using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class UnitSelectionHandler : MonoBehaviour
{
    #region Public Variables
    [SerializeField] private LayerMask mouseLayer = default;
    [SerializeField] private RectTransform unitSelectionArea = default;
    #endregion

    #region Private Variables
    private Vector2 _startPos;

    private RTSPlayer _player;
    private Camera _cam;

    public List<Unit> SelectedUnits { get; } = new List<Unit>();
    #endregion

    #region Unity Callbacks
    void Start() => _cam = Camera.main;

    void Update()
    {
        if (_player == null)
            _player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();

        if (Mouse.current.leftButton.wasPressedThisFrame)
            StartSelectionArea();
        else if (Mouse.current.leftButton.isPressed)
            UpdateSelectionArea();
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
            ClearSelectionArea();
    }
    #endregion

    #region My Functions

    #region Server

    #endregion

    #region Client
    void StartSelectionArea()
    {
        if (!Keyboard.current.leftShiftKey.isPressed)
        {
            foreach (Unit selectedUnit in SelectedUnits)
                selectedUnit.DeSelect();

            SelectedUnits.Clear();
        }

        unitSelectionArea.gameObject.SetActive(true);
        _startPos = Mouse.current.position.ReadValue();

        UpdateSelectionArea();
    }

    void UpdateSelectionArea()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        float areaWidth = mousePos.x - _startPos.x;
        float areaHeight = mousePos.y - _startPos.y;

        unitSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));
        unitSelectionArea.anchoredPosition = _startPos + new Vector2(areaWidth / 2, areaHeight / 2);
    }

    void ClearSelectionArea()
    {
        unitSelectionArea.gameObject.SetActive(false);

        if (unitSelectionArea.sizeDelta.magnitude == 0)
        {
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mouseLayer))
                return;

            if (!hit.collider.TryGetComponent<Unit>(out Unit unit))
                return;

            if (!unit.hasAuthority)
                return;

            SelectedUnits.Add(unit);

            foreach (Unit selectedUnit in SelectedUnits)
                selectedUnit.Select();

            return;
        }

        Vector2 min = unitSelectionArea.anchoredPosition - (unitSelectionArea.sizeDelta / 2);
        Vector3 max = unitSelectionArea.anchoredPosition + (unitSelectionArea.sizeDelta / 2);

        foreach (Unit selectedUnit in _player.GetMyUnits())
        {
            if (SelectedUnits.Contains(selectedUnit))
                continue;

            Vector3 screenPos = _cam.WorldToScreenPoint(selectedUnit.transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x
                && screenPos.y > min.y && screenPos.y < max.y)
            {
                SelectedUnits.Add(selectedUnit);
                selectedUnit.Select();
            }
        }
    }
    #endregion

    #endregion
}
