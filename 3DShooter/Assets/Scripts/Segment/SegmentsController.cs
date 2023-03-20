using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentsController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Segment[] segments = null;
    [SerializeField] private SegmentData data = null;
    #endregion

    #region PRIVATE_FIELDS

    #endregion

    #region INT
    public void Init()
    {
        for(int i = 0; i < segments.Length; i++)
        {
            segments[i].Init(data, null);
        }
    }
    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}
