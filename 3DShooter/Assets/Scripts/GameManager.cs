using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private ShopController shopController = null;
    [SerializeField] private StatisticsUIController statisticsUIController = null;
    #endregion

    #region PRIVATE_FIELDS

    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        Init();
    }
    #endregion

    #region INIT
    private void Init()
    {
        playerController.Init();
        shopController.Init();
        statisticsUIController.Init();
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
