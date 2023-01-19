using System;
using UnityEngine;

public class SwayActions
{
    public Action onUpdate = null;
    public Action<bool> onToggle = null;
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

    private bool canSway = false;
    #endregion

    #region INIT
    public void Init()
    {
        swayActions.onUpdate = UpdateSway;
        swayActions.onToggle = Toggle;

        originRotation = transform.localRotation;

        Toggle(true);
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
        if(!canSway)
        {
            return;
        }

        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        Quaternion xAdjustment = Quaternion.AngleAxis(-intensity * xMouse, Vector3.up);
        Quaternion yAdjustment = Quaternion.AngleAxis(intensity * yMouse, Vector3.right);
        Quaternion zAdjustment = Quaternion.AngleAxis(-intensity * xMouse, Vector3.forward);
        Quaternion targetRotation = originRotation * xAdjustment * yAdjustment * zAdjustment;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }

    private void Toggle(bool status)
    {
        canSway = status;
    }
    #endregion
}
