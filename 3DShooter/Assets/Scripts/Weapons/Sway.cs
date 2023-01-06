using System.Collections;
using System.Collections.Generic;
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
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        UpdateSway();
    }
    #endregion

    #region INIT
    public void Init()
    {
        originRotation = transform.localRotation;
    }
    #endregion

    #region PRIVATE_METHODS
    private void UpdateSway()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        Quaternion xAdjustment = Quaternion.AngleAxis(-intensity * xMouse, Vector3.up);
        Quaternion yAdjustment = Quaternion.AngleAxis(intensity * yMouse, Vector3.right);
        Quaternion targetRotation = originRotation * xAdjustment * yAdjustment;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
    #endregion
}
