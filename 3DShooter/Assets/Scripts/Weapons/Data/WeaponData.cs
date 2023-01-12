using System;

using UnityEngine;

[Serializable]
public struct RecoilConfig
{
    public Vector3 RecoilRotation;
    public Vector3 RecoilKickBack;
    public Vector3 RecoilRotationAim;
    public Vector3 RecoilKickBackAim;
}

[CreateAssetMenu(fileName = "Weapon_", menuName = "ScriptableObjects/Weapons", order = 1)]
public class WeaponData : ScriptableObject
{
    #region SERIALIZED_FIELDS
    [SerializeField] private string id = string.Empty;

    [Space, Header("Weapon Config")]
    [SerializeField] private float rateOfFire = 1f;
    [SerializeField] private float reloadTime = 0.5f;
    [SerializeField] private int maxAmmo = 120;
    [SerializeField] private int currentMaxAmmo = 120;
    [SerializeField] private int maxMagazineSize = 30;
    [SerializeField] private bool unlocked = false;

    [Space, Header("Recoil Config")]
    [SerializeField] private RecoilConfig recoilConfig = default;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public float RateOfFire { get => rateOfFire; }
    public float ReloadTime { get => reloadTime; }
    public int CurrentMaxAmmo { get => currentMaxAmmo; }
    public int MaxAmmo { get => maxAmmo; }
    public int MaxMagazineSize { get => maxMagazineSize; }
    public bool Unlocked { get => unlocked; }
    public RecoilConfig RecoilConfig { get => recoilConfig; }
    #endregion
}
