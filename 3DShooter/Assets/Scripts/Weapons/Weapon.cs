using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private WeaponData weaponData = null;
    #endregion

    #region PRIVATE_FIELDS
    private WeaponModel weaponModel = null;
    #endregion

    #region PROPERTIES
    public WeaponModel WeaponModel { get => weaponModel; }
    #endregion

    #region INIT
    public void Init()
    {
        weaponModel = new WeaponModel(weaponData);
    }
    #endregion

    #region PUBLIC_METHODS
    public void Toggle(bool status)
    {
        gameObject.SetActive(status);
    }
    #endregion
}
