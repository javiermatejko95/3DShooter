using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyModel
{
    #region PRIVATE_FIELDS
    private int totalAmount = 0;
    #endregion

    #region PROPERTIES
    public int TotalAmount { get => totalAmount; set => totalAmount = value; }
    #endregion

    #region CONSTRUCTOR
    public PlayerMoneyModel(PlayerMoneyData playerMoneyData)
    {
        totalAmount = playerMoneyData.TotalAmount;
    }
    #endregion
}
