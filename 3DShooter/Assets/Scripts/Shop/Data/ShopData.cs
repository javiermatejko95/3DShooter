using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop_", menuName = "ScriptableObjects/Shop", order = 1)]
public class ShopData : ScriptableObject
{
    #region EXPOSED_FIELDS
    [SerializeField] private string id = string.Empty;
    #endregion
}
