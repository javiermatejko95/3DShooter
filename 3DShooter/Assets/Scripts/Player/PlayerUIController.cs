using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;

public class PlayerUIActions
{
    public Action<int, int> onUpdateAmmoText = null;
    public Action<string> onUpdateMessage = null;
    public Action<int> onUpdateMoneyText = null;
    public Action<int, int> onUpdateWallHealthBar = null;
}

public class PlayerUIController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private TextMeshProUGUI ammoText = null;
    [SerializeField] private TextMeshProUGUI messageText = null;
    [SerializeField] private TextMeshProUGUI moneyText = null;

    [SerializeField] private HealthBar wallHealthBar = null;
    #endregion

    #region PRIVATE_FIELDS
    public PlayerUIActions playerUIActions = new();
    #endregion

    #region INIT
    public void Init()
    {
        playerUIActions.onUpdateAmmoText = UpdateAmmoText;
        playerUIActions.onUpdateMessage = UpdateMessageText;
        playerUIActions.onUpdateMoneyText = UpdateMoneyText;
        playerUIActions.onUpdateWallHealthBar = UpdateWallHealthBar;

        wallHealthBar.Init(false);
    }
    #endregion

    #region PUBLIC_METHODS
    public PlayerUIActions GetActions()
    {
        return playerUIActions;
    }
    #endregion

    #region PRIVATE_METHODS
    private void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        ammoText.text = string.Format("{0}/{1}", currentAmmo, maxAmmo);
    }

    private void UpdateMessageText(string message)
    {
        messageText.text = message;
    }

    private void UpdateMoneyText(int amount)
    {
        moneyText.text = "$" + amount;
    }

    private void UpdateWallHealthBar(int currentHealth, int maxHealth)
    {
        wallHealthBar.UpdateTarget(currentHealth, maxHealth);
    }
    #endregion
}
