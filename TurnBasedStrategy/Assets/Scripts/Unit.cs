using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField] private Animator animator;

    [Header("FLOAT PARAMETERS")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
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
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}