using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("Movement")]
    [SerializeField] private PlayerMovement playerMovement = null;

    [Space, Header("Camera")]
    [SerializeField] private CameraController cameraController = null;
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] private PlayerInteraction playerInteraction = null;

    [Space, Header("Shooting")]
    [SerializeField] private WeaponController weaponController = null;

    [Space, Header("UI")]
    [SerializeField] private PlayerUIController playerUIController = null;
    #endregion

    #region PRIVATE_FIELDS
    private bool initialized = false;
    #endregion

    #region ACTIONS
    private PlayerUIActions playerUIActions = null;
    private PlayerMovementActions playerMovementActions = null;
    private CameraControllerActions cameraControllerActions = null;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        if(!initialized)
        {
            return;
        }

        playerMovementActions.onMove?.Invoke();
    }
    #endregion

    #region INIT
    public void Init()
    {
        Setup();
    }
    #endregion

    #region PUBLIC_METHODS
    public PlayerUIActions GetActions()
    {
        return playerUIActions;
    }

    public PlayerMovementActions GetMovementActions()
    {
        return playerMovementActions;
    }

    public WeaponControllerActions GetWeaponControllerActions()
    {
        return weaponController.GetActions();
    }

    public SwayActions GetSwayActions()
    {
        return weaponController.GetSwayActions();
    }

    public CameraControllerActions GetCameraControllerActions()
    {
        return cameraController.GetActions();
    }
    #endregion

    #region PRIVATE_METHODS
    private void Setup()
    {
        playerMovement.Init(playerCamera);
        playerUIController.Init();

        playerUIActions = playerUIController.GetActions();
        playerMovementActions = playerMovement.GetActions();

        playerInteraction.Init(playerCamera, playerUIActions);
        weaponController.Init(playerUIActions);

        cameraController.Init();

        initialized = true;
    }
    #endregion
}
