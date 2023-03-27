using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour, IAttackable
{
    #region EXPOSED_FIELDS
    [SerializeField] private GameObject breakableObject = null;
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

        collider = GetComponent<BoxCollider>();
        collider.enabled = true;
    }
    #endregion

    #region PUBLIC_METHODS
    public void TakeDamage(int amount)
    {
        onTakeDamage?.Invoke(amount);
    }
    #endregion
}
