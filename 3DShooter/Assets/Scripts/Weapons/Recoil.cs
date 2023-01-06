using System;
using UnityEngine;

public class RecoilActions
{
    public Action onRecoil = null;
}

public class Recoil : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [Header("Reference point")]
    [SerializeField] private Transform recoilPosition = null;
    [SerializeField] private Transform rotationPoint = null;

    [Space, Header("Speed Settings")]
    [SerializeField] private float positionalRecoilSpeed = 8f;
    [SerializeField] private float rotationalRecoilSpeed = 8f;
    [SerializeField] private float positionalReturnSpeed = 18f;
    [SerializeField] private float rotationalReturnSpeed = 38f;

    [Space, Header("Amount Settings")]
    [SerializeField] private Vector3 recoilRotation = new Vector3(10f, 5f, 7f);
    [SerializeField] private Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);
    [SerializeField] private Vector3 recoilRotationAim = new Vector3(10f, 4f, 6f);
    [SerializeField] private Vector3 recoilKickBackAim = new Vector3(0.015f, 0f, -0.2f);

    [Space, Header("State")]
    [SerializeField] private bool aiming = false;
    #endregion

    #region PRIVATE_FIELDS
    private Vector3 rotationalRecoil = new();
    private Vector3 positionalRecoil = new();
    private Vector3 rotation = new();

    private RecoilActions recoilActions = new();
    #endregion

    #region UNITY_CALLS
    private void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
        rotation = Vector3.Slerp(rotation, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(rotation);
    }
    #endregion

    #region INIT
    public void Init()
    {
        recoilActions.onRecoil = Fire;
    }
    #endregion

    #region PUBLIC_METHODS
    public RecoilActions GetActions()
    {
        return recoilActions;
    }
    #endregion

    #region PRIVATE_METHODS
    private void Fire()
    {
        if(aiming)
        {
            rotationalRecoil += new Vector3(-recoilRotationAim.x, UnityEngine.Random.Range(-recoilRotationAim.y, recoilRotationAim.y), UnityEngine.Random.Range(-recoilRotationAim.z, recoilRotationAim.z));
            positionalRecoil += new Vector3(UnityEngine.Random.Range(-recoilKickBackAim.x, recoilKickBackAim.x), UnityEngine.Random.Range(-recoilKickBackAim.y, recoilKickBackAim.y), recoilKickBackAim.z);
        }
        else
        {
            rotationalRecoil += new Vector3(-recoilRotation.x, UnityEngine.Random.Range(-recoilRotation.y, recoilRotation.y), UnityEngine.Random.Range(-recoilRotation.z, recoilRotation.z));
            positionalRecoil += new Vector3(UnityEngine.Random.Range(-recoilKickBack.x, recoilKickBack.x), UnityEngine.Random.Range(-recoilKickBack.y, recoilKickBack.y), recoilKickBack.z);
        }
    }
    #endregion
}
