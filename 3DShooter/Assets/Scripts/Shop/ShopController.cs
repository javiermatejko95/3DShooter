using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [Header("Data")]
    [SerializeField] private ShopCategoryItemData[] categoriesData = null;

    [Space, Header("Prefabs")]
    [SerializeField] private ShopCategoryHolder categoryPrefabUI = null;
    [SerializeField] private AmmoItemHolder ammoItemHolderPrefab = null;
    [SerializeField] private GunItemHolder gunItemHolderPrefab = null;

    [Space, Header("Parents")]
    [SerializeField] private Transform parentCategoryObject = null;
    [SerializeField] private Transform parentItemObject = null;

    [Space, Header("Views")]
    [SerializeField] private GameObject shopView = null;

    [Space, Header("Handlers")]
    [SerializeField] private WeaponHandler weaponHandler = null;
    #endregion

    #region PRIVATE_FIELDS
    private bool initialized = false;

    private List<ShopCategoryHolder> categoriesDataList = new();
    private List<AmmoItemHolder> ammoItemsList = new();
    private List<GunItemHolder> gunItemsList = new();

    private bool onToggle = false;

    private string selectedCategory = string.Empty;
    #endregion

    #region UNITY_CALLS

    #endregion

    #region INIT
    public void Init()
    {
        for (int i = 0; i < categoriesData.Length; i++)
        {
            ShopCategoryHolder categoryHolder = Instantiate(categoryPrefabUI, parentCategoryObject);

            string id = categoriesData[i].Id;

            categoryHolder.Init(categoriesData[i], () => SelectCategory(id));

            categoriesDataList.Add(categoryHolder);

            switch (categoriesData[i].Type)
            {
                case ITEM_TYPE.AMMO:
                    for (int j = 0; j < categoriesData[i].ItemsData.Length; j++)
                    {
                        AmmoItemHolder ammoItemHolder = Instantiate(ammoItemHolderPrefab, parentItemObject);

                        AmmoShopData ammoShopData = categoriesData[i].ItemsData[j] as AmmoShopData;

                        ammoItemHolder.Init(ammoShopData, () => BuyAmmo(ammoShopData.Id, ammoShopData.Amount));
                        ammoItemHolder.Toggle(false);

                        ammoItemsList.Add(ammoItemHolder);
                    }
                    break;
                case ITEM_TYPE.GUN:
                    for (int j = 0; j < categoriesData[i].ItemsData.Length; j++)
                    {
                        GunItemHolder gunItemHolder = Instantiate(gunItemHolderPrefab, parentItemObject);

                        GunShopData gunShopData = categoriesData[i].ItemsData[j] as GunShopData;

                        gunItemHolder.Init(gunShopData, () => BuyWeapon(gunShopData.Id));
                        gunItemHolder.Toggle(false);

                        gunItemsList.Add(gunItemHolder);
                    }
                    break;
            }
        }

        shopView.SetActive(onToggle);

        initialized = true;
    }
    #endregion

    #region PUBLIC_METHODS
    public void ToggleUI(bool status)
    {
        shopView.SetActive(status);
    }
    #endregion

    #region PRIVATE_METHODS
    private void SelectCategory(string id)
    {
        if(selectedCategory == id)
        {
            return;
        }

        for(int i = 0; i < categoriesData.Length; i++)
        {
            if(categoriesData[i].Id == id)
            {
                switch (categoriesData[i].Type)
                {
                    case ITEM_TYPE.AMMO:
                        ToggleAmmoList(true);
                        ToggleGunsList(false);
                        break;
                    case ITEM_TYPE.GUN:
                        ToggleAmmoList(false);
                        ToggleGunsList(true);
                        break;
                }

                break;
            }            
        }

        selectedCategory = id;
    }

    private void ToggleAmmoList(bool status)
    {
        for(int i = 0; i < ammoItemsList.Count; i++)
        {
            ammoItemsList[i].Toggle(status);
        }
    }

    private void ToggleGunsList(bool status)
    {
        for (int i = 0; i < gunItemsList.Count; i++)
        {
            gunItemsList[i].Toggle(status);
        }
    }

    private void BuyWeapon(string id)
    {
        weaponHandler.BuyWeapon(id);
    }

    private void BuyAmmo(string id, int amount)
    {
        weaponHandler.AddAmmo(id, amount);
    }
    #endregion
}
