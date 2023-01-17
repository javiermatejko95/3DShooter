using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private Transform player = null;

    [Space, Header("Config")]
    [SerializeField] private float mouseSensitivity;    
    #endregion

    #region PRIVATE_FIELDS
    private float verticalAngle;

    private float xRotation = 0f;

    private float rotationSpeedX;
    private float rotationSpeedY;
    private float rotationRange = 60.0f;
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

    #region PRIVATE_METHODS
    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -rotationRange, rotationRange);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }    
    #endregion
}
