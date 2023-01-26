using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel
{
    #region PRIVATE_FIELDS
    private string id = string.Empty;
    private float rateOfFire = 1f;
    private float reloadTime = 1f;
    private int maxAmmo = 90;
    private int currentMaxAmmo = 90;
    private int currentAmmo = 30;
    private int maxMagazineSize = 30;
    private int damage = 10;
    private bool unlocked = false;
    private RecoilConfig recoilConfig = default;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public float RateOfFire { get => rateOfFire; }
    public float ReloadTime { get => reloadTime; }
    public int MaxAmmo { get => maxAmmo; }
    public int CurrentMaxAmmo { get => currentMaxAmmo; set => currentMaxAmmo = value; }
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public int MaxMagazineSize { get => maxMagazineSize; }
    public int Damage { get => damage; }
    public bool Unlocked { get => unlocked; set => unlocked = value; }
    public RecoilConfig RecoilConfig { get => recoilConfig; }
    #endregion

    #region CONSTRUCTOR
    public WeaponModel(WeaponData weaponData)
    {
        id = weaponData.Id;
        rateOfFire = weaponData.RateOfFire;
        reloadTime = weaponData.ReloadTime;
        maxAmmo = weaponData.MaxAmmo;
        currentMaxAmmo = weaponData.MaxAmmo;
        currentAmmo = weaponData.MaxMagazineSize;
        maxMagazineSize = weaponData.MaxMagazineSize;
        damage = weaponData.Damage;
        recoilConfig = weaponData.RecoilConfig;
        unlocked = weaponData.Unlocked;
    }
    #endregion
}
