using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : SingletonMonoBehaviour<LevelGrid>
{
    [SerializeField] private GridDebugObject gridDebugObjectPrefab;

    private GridSystem gridSystem;

    protected override void Awake()
    {
        base.Awake();

        gridSystem = new GridSystem(10, 10, 2);        
    }

    private void Start()
    {
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab, transform);
    }

    public int GetWidth()
    {
        return gridSystem.GetWidth();
    }

    public int GetHeight()
    {
        return gridSystem.GetHeight(); 
    }

    public void AddUnitAtGridPosition(Unit unit, GridPosition gridPosition)
    {
        gridSystem.GetGridObject(gridPosition).AddUnit(unit);
    }

    public List<Unit> GetUnitsAtGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetGridObject(gridPosition).GetUnits();
    }

    public void RemoveUnitAtGridPosition(Unit unit, GridPosition gridPosition)
    {
        gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
    }

    public GridPosition GetGridPositionFromWorldPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPositionFromWorldPosition(worldPosition);
    }

    public Vector3 GetWorldPositionFromGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPositionFromGridPosition(gridPosition);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(unit, fromGridPosition);
        AddUnitAtGridPosition(unit, toGridPosition);

        gridSystem.GetDebugObjectAtGridPosition(fromGridPosition).UpdateDebugText();
        gridSystem.GetDebugObjectAtGridPosition(toGridPosition).UpdateDebugText();
    }

    public bool IsValidGridPosition(GridPosition gridPositionToTest)
    {
        return gridSystem.IsValidGridPosition(gridPositionToTest);
    }
    
    public bool HasAnyUnitAtGridPosition(GridPosition gridPosition)
    {
        List<Unit> units = gridSystem.GetGridObject(gridPosition).GetUnits();

        return units.Count > 0;
    }
}