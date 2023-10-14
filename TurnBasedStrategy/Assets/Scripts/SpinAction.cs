using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    [SerializeField] private float spinAmount;

    private float totalSpinAmount;

    private const string actionName = "Spin";

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float spinAddAmount = spinAmount * Time.deltaTime;

        transform.eulerAngles += new Vector3(0f, spinAddAmount, 0f);
        totalSpinAmount += spinAddAmount;

        if (totalSpinAmount >= 360f)
        {
            isActive = false;
            InvokeActionCompletedEvent();
        }
    }

    public override void Perform(GridPosition gridPosition, Action onComplete)
    {
        isActive = true;
        totalSpinAmount = 0;
        OnActionCompleted += onComplete;
    }

    public override string GetActionName()
    {
        return actionName;
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition> { unitGridPosition };
    }
}