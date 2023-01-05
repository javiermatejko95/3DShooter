using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("Movement")]
    [SerializeField] private float gravityForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private bool canJump = true;    

    [Space, Header("Camera")]
    [SerializeField] private Transform playerCamera;

    [Space, Header("Shooting")]
    [SerializeField] private WeaponController weaponController = null;

    [Space, Header("UI")]
    [SerializeField] private PlayerUIController playerUIController = null;
    #endregion

    #region PRIVATE_FIELDS
    private PlayerUIActions playerUIActions = null;

    private float gravity;    

    private bool isCrouching = false;
    private bool isSprinting = false;

    private CharacterController cc;
    #endregion

    #region UNITY_CALLS
    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (cc.isGrounded)
        {
            gravity = 0;
            canJump = true;
            PlayerJump();
        }
        gravity += gravityForce * Time.deltaTime;
        canJump = false;

        PlayerMovement();
    }
    #endregion

    #region PRIVATE_METHODS
    private void Setup()
    {
        playerUIController.Init();

        playerUIActions = playerUIController.GetActions();
        cc = GetComponent<CharacterController>();

        sprintSpeed = 1f;
        canJump = true;
        isCrouching = false;
        isSprinting = false;

        weaponController.Init(playerUIActions);
    }

    private void PlayerMovement()
    {
        Vector3 mov = Vector3.zero;
        mov += Vector3.down * gravity;
        Sprint();
        Crouch();
        mov += transform.forward * Input.GetAxis("Vertical") * playerSpeed * sprintSpeed;
        mov += transform.right * Input.GetAxis("Horizontal") * playerSpeed * sprintSpeed;
        cc.Move(mov * Time.deltaTime);
        //MoveAnimations(verticalAxis, horizontalAxis);
    }

    public void CharacterRotation()
    {
        
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && canJump && !isCrouching)
        {
            gravity -= jumpForce;
            canJump = false;
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            sprintSpeed = 1.5f;
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintSpeed = 1f;
            isSprinting = false;
        }
    }
    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !isSprinting)
        {
            sprintSpeed = 0.5f;
            playerCamera.transform.localPosition = new Vector3(0, 0, 0);
            isCrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            sprintSpeed = 1f;
            playerCamera.transform.localPosition = new Vector3(0, 0.7f, 0);
            isCrouching = false;
        }
    }
    #endregion
}
