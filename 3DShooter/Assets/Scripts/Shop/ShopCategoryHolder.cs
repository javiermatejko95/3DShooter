using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ShopCategoryHolder : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private TextMeshProUGUI descriptionText = null;
    [SerializeField] private Button selectButton = null;
    #endregion

    #region PRIVATE_FIELDS
    private string id = string.Empty;
    private ShopCategoryItemData categoryData = null;
    #endregion

    #region INIT
    public void Init(ShopCategoryItemData categoryData, Action onSelect)
    {
        this.categoryData = categoryData;

        id = categoryData.Id;
        descriptionText.text = categoryData.Description;

        selectButton.onClick.AddListener(() => onSelect?.Invoke());
    }
    #endregion
}
