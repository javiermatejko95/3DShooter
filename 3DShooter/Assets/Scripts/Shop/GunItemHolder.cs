using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunItemHolder : ItemHolder<GunShopData>
{
    #region OVERRIDE_METHODS
    public override void Init(GunShopData itemData, Action onBuy)
    {
        priceText.text = "$" + itemData.Price;
        image.sprite = itemData.Sprite;

        buyButton.onClick.AddListener(() => onBuy?.Invoke());
    }
    #endregion
}
