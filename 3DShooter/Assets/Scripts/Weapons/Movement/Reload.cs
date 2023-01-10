using System;

using UnityEngine;

public class ReloadActions
{
    public Action onUpdate = null;
    public Action<bool> onSetIsReloading = null;
    public Func<bool> onGetIsReloading = null;
}

public class Reload : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Transform originalPosition = null;
    [SerializeField] private Transform reloadingPosition = null;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationAngle = 10f;
    #endregion

    #region PRIVATE_FIELDS
    private Transform weaponPosition = null;

    private bool isReloading = false;

    private Quaternion originRotation = new();

    private bool initialized = false;

    private ReloadActions reloadActions = new();
    #endregion

    #region INIT
    public void Init(Transform weaponPosition)
    {
        reloadActions.onUpdate = UpdateReload;
        reloadActions.onSetIsReloading = SetIsReloading;
        reloadActions.onGetIsReloading = GetIsReloading;

        this.weaponPosition = weaponPosition;
        originRotation = Quaternion.identity;

        initialized = true;
    }
    #endregion

    #region PUBLIC_METHODS
    public ReloadActions GetActions()
    {
        return reloadActions;
    }
    #endregion

    #region PRIVATE_METHODS
    private void UpdateReload()
    {
        if (!initialized)
        {
            return;
        }

        if (isReloading)
        {
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, reloadingPosition.localPosition, speed * Time.deltaTime);

            Quaternion xAdjustment = Quaternion.AngleAxis(rotationAngle, Vector3.right);
            Quaternion targetRotation = originRotation * xAdjustment;

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
        }
        else
        {
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, originalPosition.localPosition, speed * Time.deltaTime);

            Quaternion xAdjustment = Quaternion.AngleAxis(0f, Vector3.right);
            Quaternion targetRotation = originRotation * xAdjustment;

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
        }
    }

    private void SetIsReloading(bool state)
    {
        isReloading = state;
    }

    private bool GetIsReloading()
    {
        return isReloading;
    }    
    #endregion    
}
