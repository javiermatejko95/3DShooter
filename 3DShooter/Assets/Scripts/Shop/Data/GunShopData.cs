using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunShopData_", menuName = "ScriptableObjects/Shop/Shop_Gun", order = 1)]
public class GunShopData : ItemShopData
{
    #region EXPOSED_FIELDS
    [SerializeField] private bool unlocked = false;
    #endregion

    #region PROPERTIES
    public bool Unlocked { get => unlocked; }
    #endregion
}
