using System;
using UnityEngine;

public class Sway : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private float intensity = 10f;
    [SerializeField] private float smooth = 10f;
    #endregion

    #region PRIVATE_FIELDS
    private Quaternion originRotation = new();
    #endregion

    #region UNITY_CALLS

    #endregion

    #region INIT
    public void Init()
    {
        originRotation = transform.localRotation;
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS
    public void UpdateSway()
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
