using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_TYPE
{
    GUN,
    AMMO
}

[CreateAssetMenu(fileName = "ShopItemData_", menuName = "ScriptableObjects/Shop/Item", order = 1)]
public class ShopItemData : ScriptableObject
{
    #region EXPOSED_FIELDS
    [SerializeField] private string id = string.Empty;
    [SerializeField] private string idParent = string.Empty;
    [SerializeField] private int price = 100;
    [SerializeField] private int amount = 10;
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private ITEM_TYPE type = default;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public string IdParent { get => idParent; }
    public int Price { get => price; }
    public int Amount { get => amount; }
    public Sprite Sprite { get => sprite; }
    public ITEM_TYPE Type { get => type; }
    #endregion
}
