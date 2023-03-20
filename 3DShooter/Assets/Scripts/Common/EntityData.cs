using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityData : ScriptableObject
{
    #region EXPOSED_FIELDS
    [SerializeField] protected string id = string.Empty;

    [SerializeField] protected int maxHealthAmount = 100;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public int MaxHealthAmount { get => maxHealthAmount; }
    #endregion
}
