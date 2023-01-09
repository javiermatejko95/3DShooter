using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSight : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Transform originalPosition = null;
    [SerializeField] private Transform adsPosition = null;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float speedFOV = 2f;

    [SerializeField] private float defaultFOV = 90f;
    [SerializeField] private float adsFOV = 30f;
    #endregion

    #region PRIVATE_FIELDS
    private Transform weaponPosition = null;
    private Camera camera = null;

    private bool isAiming = false;

    private float currentFOV = 90f;
    private float targetFOV = 90f;

    private bool initialized = false;
    #endregion

    #region PRIVATE_METHODS
    private void SetFieldOfView(float value)
    {
        if(isAiming)
        {
            if(currentFOV >= adsFOV)
            {
                currentFOV -= Time.deltaTime * speedFOV;
                camera.fieldOfView = currentFOV;
            }
        }
        else
        {
            if (currentFOV <= defaultFOV)
            {
                currentFOV += Time.deltaTime * speedFOV;
                camera.fieldOfView = currentFOV;
            }
        }        
    }
    #endregion

    public void SetIsAiming(bool state)
    {
        isAiming = state;
    }

    public void Init(Transform weaponPosition, Camera camera)
    {
        this.weaponPosition = weaponPosition;
        this.camera = camera;
        currentFOV = defaultFOV;

        initialized = true;
    }

    public void UpdateAimDownSight()
    {
        if(!initialized)
        {
            return;
        }

        if (isAiming)
        {
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, adsPosition.localPosition, speed * Time.deltaTime);
            SetFieldOfView(adsFOV);
        }
        else
        {
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, originalPosition.localPosition, speed * Time.deltaTime);
            SetFieldOfView(defaultFOV);
        }
    }

    public void UpdateFOV()
    {
        SetFieldOfView(defaultFOV);
    }
}
