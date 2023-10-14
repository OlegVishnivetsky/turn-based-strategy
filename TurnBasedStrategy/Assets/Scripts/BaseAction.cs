using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive;

    protected event Action OnActionCompleted;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public void InvokeActionCompletedEvent()
    {
        OnActionCompleted?.Invoke();
        OnActionCompleted = null;
    }
    public abstract void Perform(GridPosition gridPosition, Action onComplete);

    public abstract string GetActionName();

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> gridPositions = GetValidGridPositionList();

        return gridPositions.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidGridPositionList();
}