using UnityEngine;

public class Reload
{
    #region EXPOSED_FIELDS
    [SerializeField] private Transform originalPosition = null;
    [SerializeField] private Transform reloadingPosition = null;
    [SerializeField] private float speed = 2f;
    #endregion

    #region PRIVATE_FIELDS
    private Transform weaponPosition = null;

    private bool isReloading = false;

    private Vector3 rotation = new();
    private Vector3 currentRotation = new();
    #endregion

    public void SetIsReloading(bool state)
    {
        isReloading = state;
    }

    public void Init(Transform weaponPosition)
    {
        this.weaponPosition = weaponPosition;
    }

    public void UpdateReload()
    {
        if (isReloading)
        {
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, reloadingPosition.localPosition, speed * Time.deltaTime);

            //rotation = Vector3.Slerp(rotation, reloadingPosition.localPosition, speed * Time.fixedDeltaTime);
            //weaponPosition.localRotation = Quaternion.Euler(rotation);
        }
        else
        {
            weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, originalPosition.localPosition, speed * Time.deltaTime);

            //rotation = Vector3.Slerp(rotation, originalPosition.localPosition, speed * Time.fixedDeltaTime);
            //weaponPosition.localRotation = Quaternion.Euler(rotation);
        }
    }
}
