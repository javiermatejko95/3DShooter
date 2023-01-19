using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyHandler : MonoBehaviour
{
    #region EXPOSED_FIELDS

    #endregion

    #region PRIVATE_FIELDS

    #endregion

    #region ACTIONS
    private EconomyActions economyActions = null;
    #endregion

    #region INIT
    public void Init(EconomyActions economyActions)
    {
        this.economyActions = economyActions;
    }
    #endregion

    #region PUBLIC_METHODS
    public EconomyActions GetActions()
    {
        return economyActions;
    }
    #endregion
}
