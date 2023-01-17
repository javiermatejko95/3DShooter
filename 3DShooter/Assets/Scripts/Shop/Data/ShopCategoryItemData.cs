using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopCategoryItemData_", menuName = "ScriptableObjects/Shop/Category", order = 1)]
public class ShopCategoryItemData : ScriptableObject
{
    #region EXPOSED_FIELDS
    [SerializeField] private string id = string.Empty;
    [SerializeField] private string description = string.Empty;
    [SerializeField] private ItemShopData[] itemsData = null;
    [SerializeField] private ITEM_TYPE type = default;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public string Description { get => description; }
    public ItemShopData[] ItemsData { get => itemsData; }
    public ITEM_TYPE Type { get => type; }
    #endregion
}
