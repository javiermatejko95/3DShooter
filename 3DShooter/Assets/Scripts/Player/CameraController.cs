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

    private float rotationSpeedX;
    private float rotationSpeedY;
    private float rotationRange = 60.0f;    
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        RotatePlayer();        
    }    
    #endregion

    #region PRIVATE_METHODS
    private void RotatePlayer()
    {
        float horRot = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float verRot = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        verticalAngle += verRot;
        verticalAngle = Mathf.Clamp(verticalAngle, -rotationRange, rotationRange);
        transform.localEulerAngles = new Vector3(verticalAngle, 0, 0);
        player.Rotate(0, horRot, 0);
    }    
    #endregion
}
