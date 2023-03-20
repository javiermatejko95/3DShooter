using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] protected EntityData data = null;
    #endregion
}
