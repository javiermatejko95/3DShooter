using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementActions
{
    public Action onMove = null;
    public Action<bool> onToggle = null;
}

public class PlayerMovement : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [Header("Config")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float crouchSpeed = 4f;
    [SerializeField] private float jumpHeight = 3f;

    [Space, Header("Ground Check")]
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask = default;

    [Space, Header("Crouching")]
    [SerializeField] private Transform playerBody = null;

    [Space, Header("Ladder Check")]
    [SerializeField] private float avoidFloorDistance = 0.1f;
    [SerializeField] private float ladderGrabDistance = 0.4f;
    [SerializeField] private float ladderFloorDropDistance = 0.1f;
    #endregion

    #region PRIVATE_FIELDS
    private CharacterController cc;

    private Transform playerCamera = null;

    private Vector3 velocity = new();

    private bool isGrounded = false;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private bool isClimbingLadder = false;

    private Vector3 lastGrabLadderDirection = new();

    private float currentSpeed = 0f;

    private bool canMove = false;
    #endregion

    #region ACTIONS
    private PlayerMovementActions playerMovementActions = new();
    #endregion

    #region INIT
    public void Init(Transform camera)
    {
        this.playerCamera = camera;

        cc = GetComponent<CharacterController>();

        playerMovementActions.onMove = Move;
        playerMovementActions.onToggle = Toggle;

        currentSpeed = speed;

        Toggle(true);
    }
    #endregion

    #region PUBLIC_METHODS
    public PlayerMovementActions GetActions()
    {
        return playerMovementActions;
    }
    #endregion

    #region PRIVATE_METHODS
    private void Move()
    {
        if(!canMove)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;        

        Sprint();
        Crouch();

        Vector3 direction = transform.forward * z;

        ClimbLadder(direction);

        if (isClimbingLadder)
        {
            move = transform.up * z;
            velocity = Vector3.zero;
        }

        cc.Move(move * currentSpeed * Time.deltaTime);        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(!isClimbingLadder)
        {
            velocity.y += gravity * Time.deltaTime;

            cc.Move(velocity * Time.deltaTime);
        }        
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            currentSpeed = sprintSpeed;
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = speed;
            isSprinting = false;
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !isSprinting)
        {
            currentSpeed = crouchSpeed;
            playerCamera.transform.localPosition = new Vector3(0f, 0f, 0f);
            playerBody.localScale = new Vector3(1f, 0.5f, 1f);
            playerBody.localPosition = new Vector3(0f, -0.7f, 0f);
            isCrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            currentSpeed = speed;
            playerCamera.transform.localPosition = new Vector3(0f, 0.7f, 0f);
            playerBody.localScale = new Vector3(1f, 1f, 1f);
            playerBody.localPosition = new Vector3(0f, 0f, 0f);
            isCrouching = false;
        }
    }

    private void ClimbLadder(Vector3 direction)
    {
        if (!isClimbingLadder)
        {
            if (Physics.Raycast(groundCheck.position + Vector3.up * avoidFloorDistance, direction, out RaycastHit hit, ladderGrabDistance))
            {
                if (hit.transform.TryGetComponent(out Ladder ladder))
                {
                    GrabLadder(direction);
                }
            }
        }
        else
        {
            if (Physics.Raycast(groundCheck.position + Vector3.up * avoidFloorDistance, lastGrabLadderDirection, out RaycastHit hit, ladderGrabDistance))
            {
                if (!hit.transform.TryGetComponent(out Ladder ladder))
                {
                    DropLadder();
                    velocity = Vector3.up * 4f;
                }
            }
            else
            {
                DropLadder();
            }

            if (Vector3.Dot(direction, lastGrabLadderDirection) < 0)
            {
                if (Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit floorRayCastHit, ladderFloorDropDistance))
                {
                    DropLadder();
                }
            }
        }
    }

    private void GrabLadder(Vector3 lastGrabLadderDirection)
    {
        isClimbingLadder = true;

        this.lastGrabLadderDirection = lastGrabLadderDirection;
    }

    private void DropLadder()
    {
        isClimbingLadder = false;
    }

    private void Toggle(bool status)
    {
        canMove = status;
    }
    #endregion
}
