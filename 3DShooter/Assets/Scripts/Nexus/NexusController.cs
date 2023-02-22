using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusControllerActions
{
    public Action<int> onTakeDamage = null;
}

public class NexusController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Nexus nexus = null;
    [SerializeField] private NexusData nexusData = null;
    #endregion

    #region PRIVATE_FIELDS
    private NexusControllerActions nexusControllerActions = new();
    #endregion

    #region INIT
    public void Init()
    {
        nexus.Init(nexusData);

        nexusControllerActions.onTakeDamage += nexus.TakeDamage;
    }
    #endregion

    #region PUBLIC_METHODS
    public NexusControllerActions GetActions()
    {
        return nexusControllerActions;
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion
}
