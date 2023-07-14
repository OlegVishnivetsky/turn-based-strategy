using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Animator animator;

    [Header("FLOAT PARAMETERS")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private GridPosition gridPosition;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;

        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(gridPosition, this);
    }

    private void Update()
    {
        float stoppingDistance = 0.1f;

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }


        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}