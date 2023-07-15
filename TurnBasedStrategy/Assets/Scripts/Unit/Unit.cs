using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private MoveAction moveAction;
    [SerializeField] private SpinAction spinAction;

    private BaseAction[] baseActions;

    private GridPosition gridPosition;

    private void Awake()
    {
        baseActions = GetComponents<BaseAction>();
    }

    private void Start()
    {   
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(gridPosition, this);
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return baseActions;
    }
}