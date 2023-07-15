using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [Header("COMPONENTS")]
    [SerializeField] private Animator animator;
    [Header("MOVEMENT PARAMETERS")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int maxMoveDistance;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        float stoppingDistance = 0.1f;

        if (!isActive)
        {
            return;
        }

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            animator.SetBool(Settings.isWalking, true);
        }
        else
        {
            animator.SetBool(Settings.isWalking, false);
            onActionComplete?.Invoke();
            isActive = false;
        }
    }

    public void Move(GridPosition gridPosition, Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> gridPositionList = GetValidActionGridPositionList();
        return gridPositionList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return Settings.moveActionName;
    }
}