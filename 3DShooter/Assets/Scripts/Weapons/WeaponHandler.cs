using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private GameObject[] weaponsPrefab = null;
    #endregion

    #region PRIVATE_FIELDS
    private List<Weapon> weapons = new();

    private int currentIndex = -1;
    #endregion

    #region INIT
    public void Init(Transform parent)
    {
        currentIndex = -1;

        for(int i = 0; i < weaponsPrefab.Length; i++)
        {
            GameObject weaponGO = Instantiate(weaponsPrefab[i], parent);

            Weapon weapon = weaponGO.GetComponent<Weapon>();

            weapon.Init();
            weapon.Toggle(false);

            weapons.Add(weapon);
        }

        weapons[0].WeaponModel.Unlocked = true;
    }
    #endregion

    #region PUBLIC_METHODS
    public Weapon GetWeaponByIndex(int index)
    {
        Weapon weapon = null;

        if(currentIndex >= 0 && currentIndex < weapons.Count)
        {
            weapons[currentIndex].Toggle(false);
        }

        if(index >= 0 || index < weapons.Count)
        {
            weapon = weapons[index];

            weapons[index].Toggle(true);

            currentIndex = index;
        }

        return weapon;
    }

    public Weapon GetWeaponById(string id)
    {
        Weapon weapon = null;

        for(int i = 0; i < weapons.Count; i++)
        {
            if(weapons[i].WeaponModel.Id == id)
            {
                weapons[currentIndex].Toggle(false);

                weapon = weapons[i];

                weapons[i].Toggle(true);

                currentIndex = i;
                break;
            }
        }

        return weapon;
    }

    public int GetCurrentWeaponIndex()
    {
        return currentIndex;
    }

    public List<Weapon> GetWeapons()
    {
        return weapons;
    }
    #endregion
}
