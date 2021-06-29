using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Variables

    [Space, Header("Player Variables")]
    public float playerWalkSpeed = 5f;
    public float playerRunSpeed = 10f;
    public float gravity = -9.81f;
    [Range(0f, 10.0f)] public float rotationSpeed = 6f;

    [Space, Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;

    [Space, Header("Camera")]
    public Vector3 offset = new Vector3(0f, 3f, -4f);
    public Transform target;
    #endregion

    #region Private Variables
    [Header("Photon")]
    private PhotonView _pview;

    [Header("Player Variables")]
    private CharacterController _charControl = default;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _vel = Vector3.zero;
    private float _moveVertical = 0f;
    private float _moveHorizontal = 0f;

    [Header("Ground Check")]
    private bool _isGrounded = default;

    [Space, Header("Camera")]
    private Camera cam = default;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        _pview = GetComponent<PhotonView>();

        if (_pview.IsMine)
            cam = Camera.main;
    }

    void Update()
    {
        if (_pview.IsMine)
        {
            CheckInputs();
            GroundCheck();
            MovePlayer();
        }
    }

    void LateUpdate()
    {
        if (_pview.IsMine)
            CamFollow();
    }
    #endregion

    #region My Functions
    void CheckInputs()
    {
        _moveVertical = Input.GetAxis("Vertical");
        _moveHorizontal = Input.GetAxis("Horizontal");

        _moveDirection = new Vector3(_moveHorizontal, 0.0f, _moveVertical);
        _moveDirection = _moveDirection.normalized;

        if (Input.GetKey(KeyCode.LeftShift))
            _moveDirection *= playerRunSpeed;
        else
            _moveDirection *= playerWalkSpeed;
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (_isGrounded & _vel.y < 0)
            _vel.y = -2f;

        _vel.y += gravity * Time.deltaTime;
        _charControl.Move(_vel * Time.deltaTime);
    }

    void MovePlayer()
    {
        _charControl.Move(_moveDirection * Time.deltaTime);

        if (_moveDirection != Vector3.zero)
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_moveDirection), rotationSpeed * Time.deltaTime);
    }

    void CamFollow()
    {
        cam.transform.position = target.position + offset;
        cam.transform.LookAt(target);
    }
    #endregion

    #region Events

    #endregion
}