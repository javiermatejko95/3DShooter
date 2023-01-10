using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [Header("Handlers")]
    [SerializeField] private WeaponHandler weaponHandler = null;

    [Space, Header("Timers")]
    [SerializeField] private Timer weaponTimer = null;
    [SerializeField] private Timer reloadTimer = null;

    [Space, Header("Recoil")]
    [SerializeField] private Recoil recoil = null;
    [SerializeField] private RecoilCamera recoilCamera = null;

    [Space, Header("Aim Down Sight")]
    [SerializeField] private AimDownSight aimDownSight = null;

    [Space, Header("Reload")]
    [SerializeField] private Reload reload = null;

    [Space, Header("Sway")]
    [SerializeField] private Sway sway = null;

    [Space, Header("Input")]
    [SerializeField] private KeyCode[] keys = null;
    
    [SerializeField] private GameObject weaponContainer = null;
    #endregion

    #region PRIVATE_FIELDS
    private Camera camera = null;    

    private Weapon selectedWeapon = null;
    private WeaponModel selectedWeaponModel = null;

    private bool canShoot = false;

    private bool initialized = false;
    #endregion

    #region ACTIONS
    private PlayerUIActions playerUIActions = null;

    private ReloadActions reloadActions = null;
    private RecoilActions recoilActions = null;
    private AimDownSightActions aimDownSightActions = null;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        SwitchWeapon();

        if(Input.GetKeyDown(KeyCode.R) && !reloadActions.onGetIsReloading.Invoke())
        {
            if(selectedWeaponModel.CurrentMaxAmmo > 0 && selectedWeaponModel.CurrentAmmo < selectedWeaponModel.MaxMagazineSize)
            {
                CancelShooting();
                StartReloading();
            }            
        }

        if(!reloadActions.onGetIsReloading.Invoke())
        {
            sway.UpdateSway();
            aimDownSightActions.onUpdate?.Invoke();

            if (!aimDownSightActions.onGetIsAiming.Invoke())
            {
                reloadActions.onUpdate?.Invoke();
            }

            if (Input.GetMouseButtonDown(1))
            {
                aimDownSightActions.onSetIsAiming?.Invoke(true);
            }

            if (Input.GetMouseButtonUp(1))
            {
                aimDownSightActions.onSetIsAiming?.Invoke(false);
            }

            if (Input.GetMouseButton(0))
            {
                Shoot();
            }            
        }
        else
        {
            aimDownSightActions.onSetIsAiming?.Invoke(false);
            aimDownSightActions.onUpdateFOV?.Invoke();
            reloadActions.onUpdate?.Invoke();
        }
    }
    #endregion

    #region INIT
    public void Init(PlayerUIActions playerUIActions)
    {
        this.camera = Camera.main;

        this.playerUIActions = playerUIActions;
        this.aimDownSightActions = aimDownSight.GetActions();
        this.reloadActions = reload.GetActions();

        weaponHandler.Init(weaponContainer.transform);

        SetWeaponByIndex(0);

        recoil.Init();
        sway.Init();

        recoilActions = recoil.GetActions();
        recoilCamera.Init(recoilActions);

        initialized = true;
    }
    #endregion

    #region PUBLIC_METHODS
    public void SetWeaponById(string id)
    {
        selectedWeapon = weaponHandler.GetWeaponById(id);
        selectedWeaponModel = selectedWeapon.WeaponModel;

        Transform weaponPosition = selectedWeapon.transform;

        aimDownSight.Init(weaponPosition, camera);
        reload.Init(weaponPosition);

        playerUIActions.onUpdateAmmoText?.Invoke(selectedWeaponModel.CurrentAmmo, selectedWeaponModel.CurrentMaxAmmo);

        weaponTimer.Init(selectedWeaponModel.RateOfFire, () =>
        {
            ToggleShooting(true);
        });

        reloadTimer.Init(selectedWeaponModel.ReloadTime, () =>
        {
            Reload();
            ToggleShooting(true);
            reloadActions.onSetIsReloading?.Invoke(false);
        });

        ToggleShooting(true);
    }

    public void SetWeaponByIndex(int index)
    {
        if(weaponHandler.GetCurrentWeaponIndex() == index)
        {
            return;
        }

        selectedWeapon = weaponHandler.GetWeaponByIndex(index);
        selectedWeaponModel = selectedWeapon.WeaponModel;

        Transform weaponPosition = selectedWeapon.transform;

        aimDownSight.Init(weaponPosition, camera);
        reload.Init(weaponPosition);

        playerUIActions.onUpdateAmmoText?.Invoke(selectedWeaponModel.CurrentAmmo, selectedWeaponModel.CurrentMaxAmmo);

        weaponTimer.Init(selectedWeaponModel.RateOfFire, () =>
        {
            ToggleShooting(true);
        });

        reloadTimer.Init(selectedWeaponModel.ReloadTime, () =>
        {
            Reload();
            ToggleShooting(true);
            reloadActions.onSetIsReloading?.Invoke(false);
        });

        ToggleShooting(true);
    }
    #endregion

    #region PRIVATE_METHODS
    private void Shoot()
    {
        if(!canShoot || reloadActions.onGetIsReloading.Invoke())
        {
            return;
        }

        recoilActions.onRecoil?.Invoke();
        selectedWeaponModel.CurrentAmmo--;

        ToggleShooting(false);
        weaponTimer.ToggleTimer(true);

        playerUIActions.onUpdateAmmoText?.Invoke(selectedWeaponModel.CurrentAmmo, selectedWeaponModel.CurrentMaxAmmo);

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100f))
        {
            Debug.Log(hit.transform.name);
        }

        if(selectedWeaponModel.CurrentAmmo <= 0)
        {
            CancelShooting();

            if (selectedWeaponModel.CurrentMaxAmmo > 0)
            {
                //start animation                
                StartReloading();
            }            
        }
    }

    private void Reload()
    {
        int currAmmoAux = selectedWeaponModel.MaxMagazineSize - selectedWeaponModel.CurrentAmmo;
        int maxAmmoAux = selectedWeaponModel.CurrentMaxAmmo - currAmmoAux;

        if(maxAmmoAux >= 0)
        {
            selectedWeaponModel.CurrentAmmo += currAmmoAux;
            selectedWeaponModel.CurrentMaxAmmo -= currAmmoAux;
        }
        else
        {
            selectedWeaponModel.CurrentAmmo += selectedWeaponModel.CurrentMaxAmmo;
            selectedWeaponModel.CurrentMaxAmmo = 0;
        }     

        playerUIActions.onUpdateAmmoText?.Invoke(selectedWeaponModel.CurrentAmmo, selectedWeaponModel.CurrentMaxAmmo);
    }

    private void StartReloading()
    {
        if(selectedWeaponModel.CurrentMaxAmmo > 0)
        {
            reloadActions.onSetIsReloading?.Invoke(true);
            reloadTimer.ToggleTimer(true);
        }        
    }

    private void StopReloading()
    {
        if(!reloadActions.onGetIsReloading.Invoke())
        {
            return;
        }

        reloadActions.onSetIsReloading?.Invoke(false);
        reloadTimer.ToggleTimer(false);
        reloadTimer.RestartTimer();
    }

    private void CancelShooting()
    {
        weaponTimer.ToggleTimer(false);
        weaponTimer.RestartTimer();        
    }

    private void SwitchWeapon()
    {
        if(!initialized)
        {
            return;
        }

        for(int i = 0; i < keys.Length; i++)
        {
            if(Input.GetKeyDown(keys[i]))
            {
                StopReloading();
                SetWeaponByIndex(i);
            }
        }
    }

    private void ToggleShooting(bool status)
    {
        canShoot = status;
    }
    #endregion
}
