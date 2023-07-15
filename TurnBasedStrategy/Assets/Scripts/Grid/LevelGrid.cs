using UnityEngine;
using System.Collections.Generic;

public class LevelGrid : SingletonMonobehaviour<LevelGrid>
{
    [SerializeField] private Transform debugObjectPrefab;

    private GridSystem gridSystem;

    protected override void Awake()
    {
        base.Awake();

        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(debugObjectPrefab, transform);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        SetUnitAtGridPosition(toGridPosition, unit);
    }

    public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unitToRemove)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unitToRemove);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();
}