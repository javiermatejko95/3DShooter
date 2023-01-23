using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [Header("Controllers")]
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private ShopController shopController = null;
    [SerializeField] private EconomyController economyController = null;
    [SerializeField] private StatisticsUIController statisticsUIController = null;
    [SerializeField] private TargetController targetController = null;

    //[Space, Header("Handlers")]
    //[SerializeField] private EconomyHandler economyHandler = null;
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
        economyController.Init(playerController.GetActions());
        shopController.Init(economyController.GetActions(), playerController.GetMovementActions(), playerController.GetWeaponControllerActions(), playerController.GetCameraControllerActions(), playerController.GetSwayActions());
        statisticsUIController.Init();

        targetController.Init();
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
