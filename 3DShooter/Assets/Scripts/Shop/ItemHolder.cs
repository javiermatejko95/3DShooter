using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public abstract class ItemHolder<T> : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] protected TextMeshProUGUI priceText = null;
    [SerializeField] protected Image image = null;
    [SerializeField] protected Button buyButton = null;
    #endregion

    #region PRIVATE_FIELDS

    #endregion

    #region OVERRIDE_METHODS
    public abstract void Init(T itemData, Action onBuy);
    #endregion

    #region PUBLIC_METHODS
    public void Toggle(bool status)
    {
        gameObject.SetActive(status);
    }
    #endregion
}
