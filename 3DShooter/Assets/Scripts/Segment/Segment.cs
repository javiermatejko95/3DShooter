using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour, IAttackable
{
    #region EXPOSED_FIELDS
    [SerializeField] private GameObject breakableObject = null;

    [Space, Header("UI")]
    [SerializeField] private HealthBar healthBar = null;
    #endregion

    #region PRIVATE_FIELDS
    private SegmentModel model = null;
    #endregion

    #region ACTIONS
    private Action onDestroyed = null;
    #endregion

    #region INIT
    public void Init(SegmentData data, Action onDestroyed)
    {
        this.onDestroyed = onDestroyed;

        model = new(data);

        healthBar.Init();
        healthBar.UpdateTarget(model.HealthAmount, model.MaxHealthAmount);
    }
    #endregion

    #region PUBLIC_METHODS
    public void Heal(int amount)
    {
        model.HealthAmount += amount;
    }
    #endregion

    public void TakeDamage(int amount)
    {
        model.HealthAmount -= amount;

        healthBar.UpdateTarget(model.HealthAmount, model.MaxHealthAmount);

        if (model.HealthAmount <= 0)
        {
            Debug.Log($"<color=red>Segment Destroyed!</color>");

            breakableObject.SetActive(false);

            onDestroyed?.Invoke();
        }
    }
}
