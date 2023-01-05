using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private WeaponSO[] weapons = null;
    #endregion

    #region PRIVATE_FIELDS

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
    public Weapon GetWeaponByIndex(int index)
    {
        Weapon weapon = null;

        if(index >= 0 || index < weapons.Length)
        {
            weapon = new Weapon(weapons[index]);
        }

        return weapon;
    }

    public Weapon GetWeaponById(string id)
    {
        Weapon weapon = null;

        for(int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].Id == id)
            {
                weapon = new Weapon(weapons[i]);
                break;
            }
        }

        return weapon;
    }
    #endregion

    #region PRIVATE_METHODS

    #endregion
}
