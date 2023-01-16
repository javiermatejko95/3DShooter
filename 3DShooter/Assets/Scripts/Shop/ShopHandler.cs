using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private WeaponHandler weaponHandler = null;
    #endregion

    #region PRIVATE_FIELDS
    private List<Weapon> weapons = new();
    #endregion
}
