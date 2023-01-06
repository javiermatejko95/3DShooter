using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSight : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Transform originalPosition = null;
    [SerializeField] private Transform adsPosition = null;
    [SerializeField] private float speed = 2f;
    #endregion

    #region PRIVATE_FIELDS
    private Transform weaponPosition = null;
    private Camera camera = null;

    private bool isAiming = false;
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if(isAiming)
        {
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, adsPosition.localPosition, speed * Time.deltaTime);
            camera.fieldOfView = 30f;
        }
        else
        {
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, originalPosition.localPosition, speed * Time.deltaTime);
            camera.fieldOfView = 90f;
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void SetFieldOfView(float value)
    {
        camera.fieldOfView = value;
    }
    #endregion

    public void SetIsAiming(bool state)
    {
        isAiming = state;
    }

    public void Init(Transform weaponPosition)
    {
        this.weaponPosition = weaponPosition;
    }
}
