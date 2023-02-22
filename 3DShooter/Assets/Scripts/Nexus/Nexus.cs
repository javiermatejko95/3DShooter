using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nexus : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private HealthBar healthBar = null;
    #endregion

    #region PRIVATE_FIELDS
    private NexusData nexusData = null;

    private string id = string.Empty;
    private int healthAmount = 100;
    #endregion

    #region UNITY_CALLS

    #endregion

    #region INIT
    public void Init(NexusData nexusData)
    {
        this.nexusData = nexusData;

        id = nexusData.Id;
        healthAmount = nexusData.HealthAmount;

        healthBar.Init();
        healthBar.UpdateTarget(healthAmount, nexusData.HealthAmount);
    }
    #endregion

    #region PUBLIC_METHODS
    public void TakeDamage(int amount)
    {
        healthAmount -= amount;

        healthBar.UpdateTarget(healthAmount, nexusData.HealthAmount);
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion
}
