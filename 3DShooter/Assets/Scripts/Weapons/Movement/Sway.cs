using System;
using UnityEngine;

public class SwayActions
{
    public Action onUpdate = null;
}

public class Sway : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private float intensity = 10f;
    [SerializeField] private float smooth = 10f;
    #endregion

    #region PRIVATE_FIELDS
    private Quaternion originRotation = new();

    private SwayActions swayActions = new();
    #endregion

    #region INIT
    public void Init()
    {
        swayActions.onUpdate = UpdateSway;

        originRotation = transform.localRotation;
    }
    #endregion

    #region PUBLIC_METHODS
    public SwayActions GetActions()
    {
        return swayActions;
    }
    #endregion

    #region PRIVATE_METHODS
    private void UpdateSway()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        Quaternion xAdjustment = Quaternion.AngleAxis(-intensity * xMouse, Vector3.up);
        Quaternion yAdjustment = Quaternion.AngleAxis(intensity * yMouse, Vector3.right);
        Quaternion zAdjustment = Quaternion.AngleAxis(-intensity * xMouse, Vector3.forward);
        Quaternion targetRotation = originRotation * xAdjustment * yAdjustment * zAdjustment;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
    #endregion
}
