using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : Interactable
{
    #region EXPOSED_FIELDS
    [SerializeField] private ShopController shopController = null;
    #endregion

    #region PRIVATE_FIELDS
    private bool onToggle = false;
    #endregion

    #region OVERRIDE_METHODS
    protected override void Interaction()
    {
        onToggle = !onToggle;

        shopController.ToggleUI(onToggle);
    }
    #endregion
}
