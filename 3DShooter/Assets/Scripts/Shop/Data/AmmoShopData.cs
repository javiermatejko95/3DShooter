using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoShopData_", menuName = "ScriptableObjects/Shop/Shop_Ammo", order = 1)]
public class AmmoShopData : ItemShopData
{
    #region EXPOSED_FIELDS
    [SerializeField] private int amount = 10;
    #endregion

    #region PROPERTIES
    public int Amount { get => amount; }
    #endregion
}
