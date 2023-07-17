using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private MoveAction moveAction;
    [SerializeField] private SpinAction spinAction;

    private int actionPoints = 2;

    private BaseAction[] baseActions;

    private GridPosition gridPosition;

    private void Awake()
    {
        baseActions = GetComponents<BaseAction>();
    }

    private void OnEnable()
    {
        TurnSystem.OnTurnNumberChanged += Instance_OnTurnNumberChanged;
    }

    private void OnDisable()
    {
        TurnSystem.OnTurnNumberChanged -= Instance_OnTurnNumberChanged;
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

    public int GetActionPoints()
    {
        return actionPoints;
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

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpenActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionCost());
            
            return true;
        }

        return false;
    }

    private bool CanSpenActionPointsToTakeAction(BaseAction baseAction)
    {
        if (actionPoints >= baseAction.GetActionCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;
    }

    private void Instance_OnTurnNumberChanged(int turnNumber)
    {
        actionPoints = Settings.actionPointMax;
    }
}