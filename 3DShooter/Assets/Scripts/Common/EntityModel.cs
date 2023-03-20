using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityModel
{
    #region PROTECTED_FIELDS
    protected string id = string.Empty;
    protected int healthAmount = 100;
    #endregion

    #region PROPERTIES
    public string Id { get => id; set => id = value; }
    public int HealthAmount { get => healthAmount; set => healthAmount = value; }
    #endregion

    #region CONSTRUCTOR
    protected EntityModel(EntityData data)
    {
        id = data.Id;
        healthAmount = data.MaxHealthAmount;
    }
    #endregion
}
