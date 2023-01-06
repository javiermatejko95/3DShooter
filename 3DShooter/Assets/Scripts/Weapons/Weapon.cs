using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    #region PRIVATE_FIELDS
    private string id = string.Empty;
    private float rateOfFire = 1f;
    private float reloadTime = 1f;
    private int maxAmmo = 90;
    private int currentAmmo = 30;
    private int maxMagazineSize = 30;
    private GameObject modelPrefab = null;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public float RateOfFire { get => rateOfFire; }
    public float ReloadTime { get => reloadTime; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public int MaxMagazineSize { get => maxMagazineSize; }
    public GameObject ModelPrefab { get => modelPrefab; }
    #endregion

    #region CONSTRUCTOR
    public Weapon(WeaponSO weaponData)
    {
        id = weaponData.Id;
        rateOfFire = weaponData.RateOfFire;
        reloadTime = weaponData.ReloadTime;
        maxAmmo = weaponData.MaxAmmo;
        currentAmmo = weaponData.MaxMagazineSize;
        maxMagazineSize = weaponData.MaxMagazineSize;
        modelPrefab = weaponData.ModelPrefab;
    }
    #endregion
}
