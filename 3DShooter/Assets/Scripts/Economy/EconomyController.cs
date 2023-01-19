using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyActions
{
    public Action<int, Action, Action> onUseCoins = null;

    public Action<int> onCheckMoney = null;
}

public class EconomyController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private EconomyHandler economyHandler = null;
    #endregion

    #region PRIVATE_FIELDS
    [SerializeField] private int moneyAmount = 0;
    #endregion

    #region ACTIONS
    private EconomyActions economyActions = new();
    private PlayerUIActions playerUIActions = null;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            moneyAmount += 10;
            playerUIActions.onUpdateMoneyText?.Invoke(moneyAmount);
        }
    }
    #endregion

    #region INIT
    public void Init(PlayerUIActions playerUIActions)
    {
        economyActions.onUseCoins = UseCoins;

        this.playerUIActions = playerUIActions;

        economyHandler.Init(economyActions);

        playerUIActions.onUpdateMoneyText?.Invoke(moneyAmount);
    }
    #endregion

    #region PUBLIC_METHODS
    public EconomyActions GetActions()
    {
        return economyActions;
    }

    public void UseCoins(int amount, Action onSuccess, Action onFailure)
    {
        int newAmount = moneyAmount - amount;

        if(newAmount >= 0)
        {
            moneyAmount = newAmount;

            playerUIActions.onUpdateMoneyText?.Invoke(moneyAmount);

            onSuccess?.Invoke();
        }
        else
        {
            onFailure?.Invoke();
        }
    }
    #endregion
}
