using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private MoveAction moveAction;
    [SerializeField] private SpinAction spinAction;

    private BaseAction[] baseActions;

    private GridPosition currentGridPosition;

    private void Awake()
    {
        baseActions = GetComponents<BaseAction>();

        currentGridPosition = LevelGrid.Instance.GetGridPositionFromWorldPosition(transform.position);

        LevelGrid.Instance.AddUnitAtGridPosition(this, currentGridPosition);
    }

    private void Start()
    {
        CalculateUnitGridPosition();
    }

    private void Update()
    {
        CheckUnitGridPositionChanges();
    }

    public BaseAction[] GetUnitActions()
    {
        return baseActions;
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
        return currentGridPosition;
    }

    private void CalculateUnitGridPosition()
    {
        currentGridPosition = LevelGrid.Instance.GetGridPositionFromWorldPosition(transform.position);
    }

    private void CheckUnitGridPositionChanges()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPositionFromWorldPosition(transform.position);

        if (currentGridPosition != newGridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, currentGridPosition, newGridPosition);
            currentGridPosition = newGridPosition;
        }
    }
}