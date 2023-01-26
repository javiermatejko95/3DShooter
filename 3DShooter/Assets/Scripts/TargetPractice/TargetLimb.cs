using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLimb : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private LIMB limb = default;
    #endregion

    #region PRIVATE_FIELDS
    private BoxCollider collider = null;
    #endregion

    #region ACTIONS
    private Action<int> onTakeDamage = null;
    #endregion

    #region INIT
    public void Init(Action<int> onTakeDamage)
    {
        this.onTakeDamage = onTakeDamage;

        collider = this.GetComponent<BoxCollider>();
    }
    #endregion

    #region PUBLIC_METHODS
    public void TakeDamage(int amount)
    {
        onTakeDamage?.Invoke(amount);
    }

    public void ToggleCollider(bool status)
    {
        collider.enabled = status;
    }
    #endregion
}
