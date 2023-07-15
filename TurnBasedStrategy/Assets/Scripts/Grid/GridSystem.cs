using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;

    private float cellSize;

    private GridObject[,] gridObjectArray;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public Vector3 GetWorldPositionFromCoordinates(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize), 
            Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return GetWorldPositionFromCoordinates(gridPosition);
    }

    public void CreateDebugObjects(Transform debugPrefab, Transform parentTransfrom)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform gridDebugObjectTransform = GameObject.Instantiate(debugPrefab, 
                    GetWorldPositionFromCoordinates(gridPosition), Quaternion.identity);
                gridDebugObjectTransform.parent = parentTransfrom;

                GridDebugObject gridDebugObject = gridDebugObjectTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 &&
                gridPosition.z >= 0 &&
                gridPosition.x < width &&
                gridPosition.z < height;
    }

}