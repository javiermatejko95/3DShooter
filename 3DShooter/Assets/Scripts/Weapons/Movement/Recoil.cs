using System;
using UnityEngine;

public class RecoilActions
{
    public Action onRecoil = null;
    public Action<RecoilConfig> onSetRecoilConfig = null;
    public Action<bool> onToggleIsAiming = null;
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
    [SerializeField] private RecoilConfig recoilConfig = default;
    //[SerializeField] private Vector3 recoilRotation = new Vector3(10f, 5f, 7f);
    //[SerializeField] private Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);
    //[SerializeField] private Vector3 recoilRotationAim = new Vector3(10f, 4f, 6f);
    //[SerializeField] private Vector3 recoilKickBackAim = new Vector3(0.015f, 0f, -0.2f);

    [Space, Header("State")]
    [SerializeField] private bool isAiming = false;
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
        recoilActions.onSetRecoilConfig = SetConfig;
        recoilActions.onToggleIsAiming = ToggleIsAiming;
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
        if(isAiming)
        {
            rotationalRecoil += new Vector3(-recoilConfig.RecoilRotationAim.x, UnityEngine.Random.Range(-recoilConfig.RecoilRotationAim.y, recoilConfig.RecoilRotationAim.y), UnityEngine.Random.Range(-recoilConfig.RecoilRotationAim.z, recoilConfig.RecoilRotationAim.z));
            positionalRecoil += new Vector3(UnityEngine.Random.Range(-recoilConfig.RecoilKickBackAim.x, recoilConfig.RecoilKickBackAim.x), UnityEngine.Random.Range(-recoilConfig.RecoilKickBackAim.y, recoilConfig.RecoilKickBackAim.y), recoilConfig.RecoilKickBackAim.z);
        }
        else
        {
            rotationalRecoil += new Vector3(-recoilConfig.RecoilRotation.x, UnityEngine.Random.Range(-recoilConfig.RecoilRotation.y, recoilConfig.RecoilRotation.y), UnityEngine.Random.Range(-recoilConfig.RecoilRotation.z, recoilConfig.RecoilRotation.z));
            positionalRecoil += new Vector3(UnityEngine.Random.Range(-recoilConfig.RecoilKickBack.x, recoilConfig.RecoilKickBack.x), UnityEngine.Random.Range(-recoilConfig.RecoilKickBack.y, recoilConfig.RecoilKickBack.y), recoilConfig.RecoilKickBack.z);
        }
    }

    private void SetConfig(RecoilConfig recoilConfig)
    {
        this.recoilConfig = recoilConfig;
    }

    private void ToggleIsAiming(bool status)
    {
        isAiming = status;
    }
    #endregion
}
