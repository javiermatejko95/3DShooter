using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private PlayerMoneyData playerMoneyData = null;
    #endregion

    #region PROPERTIES
    public PlayerMoneyData PlayerMoneyData { get => playerMoneyData; }
    #endregion
}
