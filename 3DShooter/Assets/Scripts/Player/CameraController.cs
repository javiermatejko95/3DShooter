using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerActions
{
    public Action<bool> onToggle = null;
}

public class CameraController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private Transform player = null;

    [Space, Header("Config")]
    [SerializeField] private float mouseSensitivity;    
    #endregion

    #region PRIVATE_FIELDS
    private float xRotation = 0f;

    private float rotationRange = 60.0f;

    private bool canRotate = false;
    #endregion

    #region ACTIONS
    private CameraControllerActions cameraControllerActions = new();
    #endregion

    #region UNITY_CALLS
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotatePlayer();        
    }
    #endregion

    #region INIT
    public void Init()
    {
        cameraControllerActions.onToggle = Toggle;

        Toggle(true);
    }
    #endregion

    #region PUBLIC_METHODS
    public CameraControllerActions GetActions()
    {
        return cameraControllerActions;
    }
    #endregion

    #region PRIVATE_METHODS
    private void RotatePlayer()
    {
        if(!canRotate)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -rotationRange, rotationRange);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }    

    private void Toggle(bool status)
    {
        canRotate = status;

        if(status)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    #endregion
}
