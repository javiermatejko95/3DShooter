using UnityEngine;

public enum ITEM_TYPE
{
    GUN,
    AMMO
}

public abstract class ItemShopData : ScriptableObject
{
    #region EXPOSED_FIELDS
    [SerializeField] protected string id = string.Empty;
    [SerializeField] protected int price = 100;
    [SerializeField] protected Sprite sprite = null;
    [SerializeField] protected ITEM_TYPE type = default;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public int Price { get => price; }
    public Sprite Sprite { get => sprite; }
    public ITEM_TYPE Type { get => type; }
    #endregion
}