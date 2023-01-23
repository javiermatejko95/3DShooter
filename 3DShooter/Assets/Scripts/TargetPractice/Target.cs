using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private int health = 100;
    [SerializeField] private TargetLimb[] limbs = null;
    #endregion

    #region INIT
    public void Init()
    {
        for(int i = 0; i < limbs.Length; i++)
        {
            limbs[i].Init(TakeDamage);
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void TakeDamage()
    {
        health -= 10;

        if(health <= 0)
        {
            health = 100;
        }
    }
    #endregion
}
