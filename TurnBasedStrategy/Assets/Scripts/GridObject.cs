using System.Collections.Generic;

public class GridObject
{
    private Unit unit;
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    private List<Unit> units;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        units = new List<Unit>();

        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public void AddUnit(Unit unit)
    {
        units.Add(unit);
    }

    public void RemoveUnit(Unit unit) 
    { 
        units.Remove(unit);
    }

    public List<Unit> GetUnits()
    {
        return units;
    }

    public override string ToString()
    {
        string unitString = "";

        foreach (Unit unit in units) 
        {
            unitString += unit + "\n";
        }

        return $"{gridPosition}\n{unitString}";
    }
}