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
    [SerializeField] private EconomyHandler economyHandler = null;
    #endregion

    #region PRIVATE_FIELDS
    private List<ShopCategoryHolder> categoriesDataList = new();
    private List<AmmoItemHolder> ammoItemsList = new();
    private List<GunItemHolder> gunItemsList = new();

    private bool onToggle = false;

    private string selectedCategory = string.Empty;
    #endregion

    #region ACTIONS
    private EconomyActions economyActions = null;
    private PlayerMovementActions playerMovementActions = null;
    private WeaponControllerActions weaponControllerActions = null;
    private CameraControllerActions cameraControllerActions = null;
    private SwayActions swayActions = null;
    #endregion

    #region UNITY_CALLS

    #endregion

    #region INIT
    public void Init(EconomyActions economyActions, PlayerMovementActions playerMovementActions, WeaponControllerActions weaponControllerActions, CameraControllerActions cameraControllerActions, SwayActions swayActions)
    {
        this.economyActions = economyActions;
        this.playerMovementActions = playerMovementActions;
        this.weaponControllerActions = weaponControllerActions;
        this.cameraControllerActions = cameraControllerActions;
        this.swayActions = swayActions;

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

                        ammoItemHolder.Init(ammoShopData, () => BuyAmmo(ammoShopData));
                        ammoItemHolder.Toggle(false);

                        ammoItemsList.Add(ammoItemHolder);
                    }
                    break;
                case ITEM_TYPE.GUN:
                    for (int j = 0; j < categoriesData[i].ItemsData.Length; j++)
                    {
                        GunItemHolder gunItemHolder = Instantiate(gunItemHolderPrefab, parentItemObject);

                        GunShopData gunShopData = categoriesData[i].ItemsData[j] as GunShopData;

                        gunItemHolder.Init(gunShopData, () => BuyWeapon(gunShopData));
                        gunItemHolder.Toggle(false);

                        gunItemsList.Add(gunItemHolder);
                    }
                    break;
            }
        }

        shopView.SetActive(onToggle);
    }
    #endregion

    #region PUBLIC_METHODS
    public void ToggleUI(bool status)
    {
        shopView.SetActive(status);
        playerMovementActions.onToggle?.Invoke(!status);
        weaponControllerActions.onToggle?.Invoke(!status);
        cameraControllerActions.onToggle?.Invoke(!status);
        swayActions.onToggle?.Invoke(!status);
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

    private void BuyWeapon(ItemShopData itemShopData)
    {
        //check if weapon is already unlocked

        economyActions.onUseCoins?.Invoke(itemShopData.Price,
            () => weaponHandler.BuyWeapon(itemShopData.Id),
            () => Debug.Log("NO SE PUDE COMPRAR " + itemShopData.Id));
    }

    private void BuyAmmo(ItemShopData itemShopData)
    {
        //check if can add more ammo

        economyActions.onUseCoins?.Invoke(itemShopData.Price,
            () => weaponHandler.AddAmmo(itemShopData.Id, ((AmmoShopData)itemShopData).Amount),
            () => Debug.Log("NO SE PUDE COMPRAR " + itemShopData.Id + " BALAS"));
    }
    #endregion
}
