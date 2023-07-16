using System.Collections.Generic;
using UnityEngine;

public class GridSystemUI : SingletonMonobehaviour<GridSystemUI>
{
    [SerializeField] private Transform gridSystemUIPrefab;

    private GridSystemUISingle[,] gridSystemUISingles;

    private void Start()
    {
        gridSystemUISingles = new GridSystemUISingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform gridSystemUITransform = Instantiate(gridSystemUIPrefab, 
                    LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                gridSystemUISingles[x, z] = gridSystemUITransform.GetComponent<GridSystemUISingle>();
            }
        }
    }

    private void Update()
    {
        UpdateGridUI();
    }

    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridSystemUISingles[x, z].DisableMeshRenderer();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            gridSystemUISingles[gridPosition.x, gridPosition.z].EnableMeshRenderer();
        }
    }

    private void UpdateGridUI()
    {
        HideAllGridPosition();

        BaseAction action = UnitActionSystem.Instance.GetSelectedAction();

        ShowGridPositionList(action.GetValidActionGridPositionList());
    }
}