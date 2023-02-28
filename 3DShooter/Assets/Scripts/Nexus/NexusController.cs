using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusControllerActions
{
    public Action<int> onTakeDamage = null;
    public Func<Nexus> onGetNexus = null;
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
        nexusControllerActions.onGetNexus += GetNexus;
    }
    #endregion

    #region PUBLIC_METHODS
    public NexusControllerActions GetActions()
    {
        return nexusControllerActions;
    }

    //public Nexus GetNexus()
    //{
    //    return nexus;
    //}
    #endregion

    #region PRIVATE_METHODS
    private Nexus GetNexus()
    {
        return nexus;
    }
    #endregion
}
