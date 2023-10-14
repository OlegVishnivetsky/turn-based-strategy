using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : SingletonMonoBehaviour<GridSystemVisual>
{
    [SerializeField] private GridVisualObject gridVisualObjectPrefab;

    private GridVisualObject[,] gridVisualObject2DArray;

    private void Start()
    {
        gridVisualObject2DArray = new GridVisualObject[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for(int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                GridVisualObject gridVisualObject = Instantiate(gridVisualObjectPrefab,
                    LevelGrid.Instance.GetWorldPositionFromGridPosition(gridPosition), Quaternion.identity);

                gridVisualObject.transform.SetParent(transform);
                gridVisualObject2DArray[x, z] = gridVisualObject;
            }
        }

        HideAllGridVisualObjects();
    }

    private void Update()
    {
        UpdateGridVisual();
    }

    private void UpdateGridVisual()
    {
        HideAllGridVisualObjects();

        BaseAction action = UnitActionSystem.Instance.GetSelectedAction();

        ShowGridVisualAtGridPositionList(action.GetValidGridPositionList());
    }

    public void ShowGridVisualAtGridPositionList(List<GridPosition> gridPositions)
    {
        HideAllGridVisualObjects();

        foreach (GridPosition gridPosition in gridPositions)
        {
            gridVisualObject2DArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    public void HideAllGridVisualObjects()
    {
        foreach (GridVisualObject gridVisualObject in gridVisualObject2DArray)
        {
            gridVisualObject.Hide();
        }
    }
}