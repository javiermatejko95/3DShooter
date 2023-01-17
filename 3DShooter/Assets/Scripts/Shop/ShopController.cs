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
    private List<ShopCategoryHolder> categoriesDataList = new();
    private List<AmmoItemHolder> ammoItemsList = new();
    private List<GunItemHolder> gunItemsList = new();

    private bool onToggle = false;

    private string selectedCategory = string.Empty;
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        for(int i = 0; i < categoriesData.Length; i++)
        {
            ShopCategoryHolder categoryHolder = Instantiate(categoryPrefabUI, parentCategoryObject);

            string id = categoriesData[i].Id;

            categoryHolder.Init(categoriesData[i], () => SelectCategory(id));

            categoriesDataList.Add(categoryHolder);

            switch(categoriesData[i].Type)
            {
                case ITEM_TYPE.AMMO:
                    for(int j = 0; j < categoriesData[i].ItemsData.Length; j++)
                    {
                        AmmoItemHolder ammoItemHolder = Instantiate(ammoItemHolderPrefab, parentItemObject);

                        ammoItemHolder.Init(categoriesData[i].ItemsData[j] as AmmoShopData, null);
                        ammoItemHolder.Toggle(false);

                        ammoItemsList.Add(ammoItemHolder);
                    }
                    break;
                case ITEM_TYPE.GUN:
                    for (int j = 0; j < categoriesData[i].ItemsData.Length; j++)
                    {
                        GunItemHolder gunItemHolder = Instantiate(gunItemHolderPrefab, parentItemObject);

                        gunItemHolder.Init(categoriesData[i].ItemsData[j] as GunShopData, null);
                        gunItemHolder.Toggle(false);

                        gunItemsList.Add(gunItemHolder);
                    }
                    break;
            }            
        }

        shopView.SetActive(onToggle);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            onToggle = !onToggle;
            shopView.SetActive(onToggle);
        }
    }
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
        }
    }
    #endregion

    #region PUBLIC_METHODS

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
            switch(categoriesData[i].Type)
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
    #endregion
}
