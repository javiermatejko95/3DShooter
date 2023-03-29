using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private ParticleSystem muzzleParticles = null;
    [SerializeField] private Light light = null;
    #endregion

    #region PRIVATE_FIELDS
    private float duration = 0f;
    #endregion

    #region UNITY_CALLS

    #endregion

    #region INIT
    public void Init()
    {
        duration = muzzleParticles.main.startLifetimeMultiplier;
        light.enabled = false;        
    }
    #endregion

    #region PUBLIC_METHODS
    public void Flash()
    {
        muzzleParticles.Play();
        
        StartCoroutine(IRestart());
    }
    #endregion

    #region PRIVATE_METHODS
    private IEnumerator IRestart()
    {
        light.enabled = false;
        light.enabled = true;
        yield return new WaitForSeconds(duration);
        light.enabled = false;
    }
    #endregion
}
