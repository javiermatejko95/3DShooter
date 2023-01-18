using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActions
{
    public Action onShoot = null;
    public Action<int> onAddAmmo = null;
    public Action onBuy = null;
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
        weaponActions.onAddAmmo = AddAmmo;
        weaponActions.onBuy = Buy;
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

    private void AddAmmo(int amount)
    {
        int newAmount = weaponModel.CurrentMaxAmmo + amount;

        if(newAmount > weaponModel.MaxAmmo)
        {
            weaponModel.CurrentMaxAmmo = weaponModel.MaxAmmo;
        }
        else
        {
            weaponModel.CurrentMaxAmmo = newAmount;
        }
    }

    private void Buy()
    {
        weaponModel.Unlocked = true;
    }
    #endregion
}
