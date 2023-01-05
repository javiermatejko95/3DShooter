using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private WeaponHandler weaponHandler = null;
    [SerializeField] private Timer weaponTimer = null;
    [SerializeField] private Timer reloadTimer = null;
    #endregion

    #region PRIVATE_FIELDS
    private Camera camera = null;

    private PlayerUIActions playerUIActions = null;

    private Weapon selectedWeapon = null;
    
    private bool canShoot = false;
    private bool isReloading = false;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            CancelShooting();
            StartReloading();
        }

        if(Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
    #endregion

    #region INIT
    public void Init(PlayerUIActions playerUIActions)
    {
        this.camera = Camera.main;
        this.playerUIActions = playerUIActions;

        SetWeaponById(WeaponConstants.idTestingWeapon);
    }
    #endregion

    #region PUBLIC_METHODS
    public void SetWeaponById(string id)
    {
        selectedWeapon = weaponHandler.GetWeaponById(id);

        playerUIActions.onUpdateAmmoText?.Invoke(selectedWeapon.CurrentAmmo, selectedWeapon.MaxAmmo);

        weaponTimer.Init(selectedWeapon.RateOfFire, () =>
        {
            ToggleShooting(true);
        });

        reloadTimer.Init(selectedWeapon.ReloadTime, () =>
        {
            Reload();
            ToggleShooting(true);
            ToggleReloading(false);
        });

        ToggleShooting(true);
    }
    #endregion

    #region PRIVATE_METHODS
    private void Shoot()
    {
        if(!canShoot || isReloading)
        {
            return;
        }

        selectedWeapon.CurrentAmmo--;

        ToggleShooting(false);
        weaponTimer.ToggleTimer(true);

        playerUIActions.onUpdateAmmoText?.Invoke(selectedWeapon.CurrentAmmo, selectedWeapon.MaxAmmo);

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100f))
        {
            Debug.Log(hit.transform.name);
        }

        if(selectedWeapon.CurrentAmmo <= 0)
        {
            CancelShooting();

            if (selectedWeapon.MaxAmmo > 0)
            {
                //start animation                
                StartReloading();
            }            
        }
    }

    private void Reload()
    {
        int currAmmoAux = selectedWeapon.MaxMagazineSize - selectedWeapon.CurrentAmmo;
        int maxAmmoAux = selectedWeapon.MaxAmmo - currAmmoAux;

        if(maxAmmoAux >= 0)
        {
            selectedWeapon.CurrentAmmo += currAmmoAux;
            selectedWeapon.MaxAmmo -= currAmmoAux;
        }
        else
        {
            selectedWeapon.CurrentAmmo += selectedWeapon.MaxAmmo;
            selectedWeapon.MaxAmmo = 0;
        }     

        playerUIActions.onUpdateAmmoText?.Invoke(selectedWeapon.CurrentAmmo, selectedWeapon.MaxAmmo);
    }

    private void StartReloading()
    {
        if(selectedWeapon.MaxAmmo > 0)
        {
            reloadTimer.ToggleTimer(true);
            ToggleReloading(true);
        }        
    }

    private void CancelShooting()
    {
        weaponTimer.ToggleTimer(false);
        weaponTimer.RestartTimer();
    }

    private void ToggleShooting(bool status)
    {
        canShoot = status;
    }

    private void ToggleReloading(bool status)
    {
        isReloading = status;
    }
    #endregion
}
