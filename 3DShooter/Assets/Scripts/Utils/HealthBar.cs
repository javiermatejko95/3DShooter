using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Image healthBarImage = null;
    [SerializeField] private float reduceSpeed = 2f;
    #endregion

    #region PRIVATE_FIELDS
    private Camera camera = null;

    private float target = 0f;

    private bool isWorldSpace = false;

    private bool initialized = false;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        if(!initialized)
        {
            return;
        }

        Rotate();

        UpdateHealthBar();

        if (Input.GetKeyDown(KeyCode.V))
        {
            UpdateTarget(target - 0.1f, 1f);
        }
    }
    #endregion

    #region INIT
    public void Init(bool isWorldSpace)
    {
        this.isWorldSpace = isWorldSpace;

        if(isWorldSpace)
        {
            camera = Camera.main;
        }        

        initialized = true;
    }
    #endregion

    #region PUBLIC_METHODS
    public void UpdateTarget(float currentHealth, float maxHealth)
    {
        target =  currentHealth / maxHealth;
    }
    #endregion

    #region PRIVATE_METHODS
    private void Rotate()
    {
        if(!isWorldSpace)
        {
            return;
        }
        //optimize by calculating if the bar is near or far

        transform.LookAt(camera.transform);
        transform.Rotate(0, 180, 0);
    }

    private void UpdateHealthBar()
    {
        if(healthBarImage.fillAmount == target)
        {
            return;
        }

        healthBarImage.fillAmount = Mathf.MoveTowards(healthBarImage.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
    #endregion
}
