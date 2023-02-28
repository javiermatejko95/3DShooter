using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private int health = 100;
    [SerializeField] private TargetLimb[] limbs = null;
    [SerializeField] private int moneyGiven = 100;
    #endregion

    #region PRIVATE_FIELDS
    private bool isDead = false;

    private Animator anim = null;
    #endregion

    #region ACTIONS
    private Action<int> onAddMoney = null;
    #endregion

    #region INIT
    public void Init(Action<int> onAddMoney)
    {
        //anim = transform.GetComponent<Animator>();

        this.onAddMoney = onAddMoney;

        for(int i = 0; i < limbs.Length; i++)
        {
            limbs[i].Init(TakeDamage);
        }
    }
    #endregion

    #region UNITY_CALLS
    private void Update()
    {

    }
    #endregion

    #region PRIVATE_METHODS
    private void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            onAddMoney?.Invoke(moneyGiven);

            isDead = true;

            ResetTarget();
        }
    }

    private void ResetTarget()
    {
        StartCoroutine(IReset());

        IEnumerator IReset()
        {
            ToggleColliders(false);

            anim.SetBool("isDead", isDead);

            yield return new WaitForSeconds(2f);

            health = 100;

            isDead = false;

            anim.SetBool("isDead", isDead);

            ToggleColliders(true);
        }
    }

    private void ToggleColliders(bool status)
    {
        for(int i = 0; i < limbs.Length; i++)
        {
            limbs[i].ToggleCollider(status);
        }
    }
    #endregion
}
