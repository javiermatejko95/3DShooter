using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "ScriptableObjects/Weapons", order = 1)]
public class WeaponSO : ScriptableObject
{
    #region SERIALIZED_FIELDS
    [SerializeField] private string id = string.Empty;
    [SerializeField] private float rateOfFire = 1f;
    [SerializeField] private float reloadTime = 0.5f;
    [SerializeField] private int maxAmmo = 120;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public float RateOfFire { get => rateOfFire; }
    public float ReloadTime { get => reloadTime; }
    public int MaxAmmo { get => maxAmmo; }
    #endregion
}
