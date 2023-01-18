using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;

public class PlayerUIActions
{
    public Action<int, int> onUpdateAmmoText = null;
    public Action<string> onUpdateMessage = null;
}

public class PlayerUIController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private TextMeshProUGUI ammoText = null;
    [SerializeField] private TextMeshProUGUI messageText = null;
    #endregion

    #region PRIVATE_FIELDS
    public PlayerUIActions playerUIActions = new();
    #endregion

    #region INIT
    public void Init()
    {
        playerUIActions.onUpdateAmmoText = UpdateAmmoText;
        playerUIActions.onUpdateMessage = UpdateMessageText;
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
    #endregion
}
