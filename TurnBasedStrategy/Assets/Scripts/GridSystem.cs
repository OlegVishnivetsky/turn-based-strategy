using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;

    private float cellSize;

    private GridObject[,] gridObject2DArray;
    private GridDebugObject[,] debugObject2DArray;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObject2DArray = new GridObject[width, height];

        for (int x = 0; x < width; x++) 
        { 
            for(int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                gridObject2DArray[x, z] = new GridObject(this, gridPosition);
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

    public void CreateDebugObjects(GridDebugObject debugObjectPrefab, Transform parent)
    {
        debugObject2DArray = new GridDebugObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                GridDebugObject debugObject = GameObject.Instantiate(debugObjectPrefab,
                    GetWorldPositionFromGridPosition(gridPosition), Quaternion.identity);

                debugObject.transform.SetParent(parent);
                debugObject.SetGridObject(gridObject2DArray[x, z]);
                debugObject.UpdateDebugText();

                debugObject2DArray[x, z] = debugObject;
            }
        }
    }

    public GridDebugObject GetDebugObjectAtGridPosition(GridPosition gridPosition)
    {
        return debugObject2DArray[gridPosition.x, gridPosition.z];
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObject2DArray[gridPosition.x, gridPosition.z];
    }


    public Vector3 GetWorldPositionFromGridPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0f, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPositionFromWorldPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    public bool IsValidGridPosition(GridPosition gridPositionToTest)
    {
        return gridPositionToTest.x >= 0f && 
               gridPositionToTest.z >= 0f && 
               gridPositionToTest.x < width && 
               gridPositionToTest.z < height;
    }
}