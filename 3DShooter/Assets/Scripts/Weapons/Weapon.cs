using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActions
{
    public Action onShoot = null;
}

public class Weapon : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private WeaponData weaponData = null;
    #endregion

    #region PRIVATE_FIELDS
    private WeaponModel weaponModel = null;

    private WeaponActions weaponActions = new();
    #endregion

    #region PROPERTIES
    public WeaponModel WeaponModel { get => weaponModel; }
    #endregion

    #region INIT
    public void Init()
    {
        weaponModel = new WeaponModel(weaponData);

        weaponActions.onShoot = Shoot;
    }
    #endregion

    #region PUBLIC_METHODS
    public WeaponActions GetActions()
    {
        return weaponActions;
    }
    
    public void Toggle(bool status)
    {
        gameObject.SetActive(status);
    }
    #endregion

    #region PRIVATE_METHODS
    private void Shoot()
    {
        weaponModel.CurrentAmmo--;
    }
    #endregion
}
