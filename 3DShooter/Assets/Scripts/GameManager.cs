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
    [SerializeField] private NexusController nexusController = null;
    [SerializeField] private WavesController wavesController = null;
    #endregion

    #region PRIVATE_FIELDS

    #endregion

    #region ACTIONS
    private PlayerUIActions playerUIActions = null;
    private PlayerMovementActions playerMovementActions = null;
    private EconomyActions economyActions = null;
    private NexusControllerActions nexusControllerActions = null;
    private WeaponControllerActions weaponControllerActions = null;
    private CameraControllerActions cameraControllerActions = null;
    private WavesControllerActions wavesControllerActions = null;
    private SwayActions swayActions = null;
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
        nexusController.Init();

        nexusControllerActions = nexusController.GetActions();

        playerController.Init();

        playerUIActions = playerController.GetActions();

        economyController.Init(playerUIActions);

        economyActions = economyController.GetActions();
        playerMovementActions = playerController.GetMovementActions();
        weaponControllerActions = playerController.GetWeaponControllerActions();
        cameraControllerActions = playerController.GetCameraControllerActions();
        swayActions = playerController.GetSwayActions();

        shopController.Init(economyActions, playerMovementActions, weaponControllerActions, cameraControllerActions, swayActions);
        statisticsUIController.Init();

        targetController.Init(economyActions);

        wavesControllerActions = wavesController.GetActions();

        wavesController.Init(nexusControllerActions, economyActions);
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
