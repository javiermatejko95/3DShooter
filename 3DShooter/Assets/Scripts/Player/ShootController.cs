using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Weapon
{
    public string id;
    public float rateOfFire;
    public float reloadTime;
    public int maxAmmo;
    public int currentAmmo;
}

public class ShootController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private WeaponSO weapon = null;
    #endregion

    #region PRIVATE_FIELDS
    private Camera camera = null;

    private WeaponSO selectedWeapon = null;

    private Weapon currentWeaponState = new();
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        Shoot();
    }
    #endregion

    #region INIT
    public void Init(Camera camera)
    {
        this.camera = camera;

        SetWeapon();
    }
    #endregion

    #region PRIVATE_METHODS
    private void SetWeapon(/*WeaponSO weapon*/)
    {
        //selectedWeapon = weapon;

        currentWeaponState.id = weapon.Id;
        currentWeaponState.currentAmmo = weapon.MaxAmmo;
        currentWeaponState.maxAmmo = weapon.MaxAmmo;
        currentWeaponState.rateOfFire = weapon.RateOfFire;
        currentWeaponState.reloadTime = weapon.ReloadTime;
    }
    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100f))
        {
            Debug.Log(hit.transform.name);
        }
    }

    private void Reload()
    {

    }

    private void ToggleScope(bool status)
    {

    }
    #endregion
}
