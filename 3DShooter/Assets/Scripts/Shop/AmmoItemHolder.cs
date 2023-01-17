using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItemHolder : ItemHolder<AmmoShopData>
{
    #region OVERRIDE_METHODS
    public override void Init(AmmoShopData itemData, Action onBuy)
    {
        priceText.text = "$" + itemData.Price;
        image.sprite = itemData.Sprite;

        buyButton.onClick.AddListener(() => onBuy?.Invoke());
    }
    #endregion
}
