using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMoneyData
{
    #region EXPOSED_FIELDS
    [SerializeField] private int totalAmount = 0;
    #endregion

    #region PROPERTIES
    public int TotalAmount { get => totalAmount; }
    #endregion
}
