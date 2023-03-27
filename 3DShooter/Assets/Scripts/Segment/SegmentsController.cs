using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SegmentsControllerActions
{
    public Func<Transform[]> onGetSegmentsTransforms = null;
}

public class SegmentsController : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private Segment[] segments = null;
    [SerializeField] private SegmentData data = null;
    #endregion

    #region PRIVATE_FIELDS
    private SegmentModel model = null;
    #endregion

    #region ACTIONS
    public SegmentsControllerActions segmentsControllerActions = new();
    private PlayerUIActions playerUIActions = null;
    #endregion

    #region INT
    public void Init(PlayerUIActions playerUIActions)
    {
        segmentsControllerActions.onGetSegmentsTransforms += GetSegmentsTransforms;

        this.playerUIActions = playerUIActions;

        model = new(data);

        for(int i = 0; i < segments.Length; i++)
        {
            segments[i].Init(TakeDamage);
        }

        playerUIActions.onUpdateWallHealthBar?.Invoke(model.HealthAmount, model.MaxHealthAmount);
    }
    #endregion

    #region PUBLIC_METHODS
    public SegmentsControllerActions GetActions()
    {
        return segmentsControllerActions;
    }
    #endregion

    #region PRIVATE_METHODS
    private void TakeDamage(int amount)
    {
        model.HealthAmount -= amount;

        playerUIActions.onUpdateWallHealthBar?.Invoke(model.HealthAmount, model.MaxHealthAmount);

        if(model.HealthAmount <= 0)
        {
            Debugger.DebugLog("Game over!", DebuggerConsts.Red);
        }
    }

    private void Repair(int amount)
    {
        model.HealthAmount += amount;

        Debugger.DebugLog("Wall repaired!", DebuggerConsts.Green);
    }

    private Transform[] GetSegmentsTransforms()
    {
        Transform[] transforms = new Transform[segments.Length];

        for(int i = 0; i < segments.Length; i++)
        {
            transforms[i] = segments[i].transform;
        }

        return transforms;
    }
    #endregion
}
