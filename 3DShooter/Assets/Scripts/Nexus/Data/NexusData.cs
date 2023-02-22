using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nexus_", menuName = "ScriptableObjects/Nexus", order = 1)]
public class NexusData : ScriptableObject
{
    #region EXPOSED_FIELDS
    [SerializeField] private string id = string.Empty;

    [SerializeField] private int healthAmount = 100;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public int HealthAmount { get => healthAmount; }
    #endregion
}
