using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class StatisticsUIController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [Header("FPS")]
    [SerializeField] private GameObject fpsHolder = null;
    [SerializeField] private TextMeshProUGUI fpsText = null;
    [SerializeField] private Timer fpsTimer = null;
    #endregion

    #region PRIVATE_FIELDS
    private float fpsCount = 0f;
    private bool showFPS = false;
    #endregion

    #region UNITY_CALLS
    private void Start()
    {
        fpsTimer.Init(0.1f, UpdateFPS);

        ToggleFPS(showFPS);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F8))
        {
            showFPS = !showFPS;
            ToggleFPS(showFPS);
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void ToggleFPS(bool status)
    {
        fpsHolder.SetActive(status);
        fpsTimer.ToggleTimer(status);
    }

    private void UpdateFPS()
    {
        fpsCount = 1f / Time.unscaledDeltaTime;
        fpsText.text = "FPS: " + Mathf.Round(fpsCount);
        fpsTimer.ToggleTimer(true);
    }
    #endregion
}
