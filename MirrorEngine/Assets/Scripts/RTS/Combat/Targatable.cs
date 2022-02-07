using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Targatable : NetworkBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables
    [SerializeField] private Transform _aimAtPoint = default;
    public Transform GetAimAtPoint() => _aimAtPoint;
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
    #endregion

    #region Coroutines

    #endregion

    #region Events

    #endregion
}