using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nexus : MonoBehaviour, IAttackable
{
    #region EXPOSED_FIELDS
    [SerializeField] private HealthBar healthBar = null;
    #endregion

    #region PRIVATE_FIELDS
    private NexusData nexusData = null;

    private string id = string.Empty;
    private int healthAmount = 100;

    private bool isDestroyed = false;
    #endregion

    #region UNITY_CALLS

    #endregion

    #region INIT
    public void Init(NexusData nexusData)
    {
        this.nexusData = nexusData;

        id = nexusData.Id;
        healthAmount = nexusData.MaxHealthAmount;

        healthBar.Init(true);
        healthBar.UpdateTarget(healthAmount, nexusData.MaxHealthAmount);

        isDestroyed = false;
    }
    #endregion

    #region PUBLIC_METHODS
    public void TakeDamage(int amount)
    {
        healthAmount -= amount;

        healthBar.UpdateTarget(healthAmount, nexusData.MaxHealthAmount);

        if(healthAmount <= 0)
        {
            isDestroyed = true;
        }
    }

    public bool GetIsDestroyed()
    {
        return isDestroyed;
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion
}
