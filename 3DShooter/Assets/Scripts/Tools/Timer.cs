using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region PRIVATE_FIELDS
    private float startingTime = 1f;
    private float currentTime = 1f;
    private bool isOn = false;
    #endregion

    #region ACTIONS
    private Action onReach = null;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        
    }
    #endregion

    #region INIT
    public void Init()
    {

    }
    #endregion

    #region PUBLIC_METHODS
    public void ToggleTimer(bool status)
    {
        isOn = status;
    }

    public void RestartTimer()
    {

    }
    #endregion

    #region PRIVATE_METHODS
    private void Countdown()
    {
        startingTime -= Time.deltaTime;

        if(startingTime <= 0f)
        {

        }
    }
    #endregion
}
