using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLimb : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private LIMB limb = default;
    #endregion

    #region ACTIONS
    private Action onTakeDamage = null;
    #endregion

    #region INIT
    public void Init(Action onTakeDamage)
    {
        this.onTakeDamage = onTakeDamage;
    }
    #endregion

    #region PUBLIC_METHODS
    public void TakeDamage()
    {
        onTakeDamage?.Invoke();
    }
    #endregion
}
