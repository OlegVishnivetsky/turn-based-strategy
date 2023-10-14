using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [Header("MAIN COMPONENTS")]
    [SerializeField] private Animator animator;

    [Header("MAIN PARAMETERS")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxMoveDistance;

    [Space(5)]
    [SerializeField] private float rotationSpeed;

    private Vector3 targetPosition;

    private const float stoppingDistance = 0.1f;
    private const string actionName = "Move";

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
        {
            animator.SetBool("IsWalking", true);

            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
        else
        {
            isActive = false;
            InvokeActionCompletedEvent();
            animator.SetBool("IsWalking", false);
        }

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }

    public override void Perform(GridPosition targetGridPosition, Action onComplete)
    {
        isActive = true;
        targetPosition = LevelGrid.Instance.GetWorldPositionFromGridPosition(targetGridPosition);
        OnActionCompleted += onComplete;
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();

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

                if (LevelGrid.Instance.HasAnyUnitAtGridPosition(testGridPosition))
                {
                    continue;
                }

                validGridPositions.Add(testGridPosition);
            }
        }

        return validGridPositions;
    }

    public override string GetActionName()
    {
        return actionName;
    }
}