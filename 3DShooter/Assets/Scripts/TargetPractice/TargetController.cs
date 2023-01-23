using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LIMB
{
    HEAD,
    BODY,
    ARM,
    FOOT
}

public class TargetController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Target target = null;
    #endregion

    #region INIT
    public void Init()
    {
        target.Init();
    }
    #endregion
}
