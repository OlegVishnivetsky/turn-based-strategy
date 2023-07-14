using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("MOVEMENT PARAMETERS")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 40f;
    [Header("ZOOM PARAMETERS")]
    [SerializeField] private float zoomAmount;
    [SerializeField] private float zoomSmoothingValue;
    [Header("COMPONENTS")]
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineTransposer cinemachineTransposer;

    private Vector3 targetFollowOffset;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        MoveCamera();
        RotateCamera();
        ZoomCamera();
    }

    private void ZoomCamera()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, Settings.minFollowYOffset, Settings.maxFollowYOffset);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset,
            targetFollowOffset, Time.deltaTime * zoomSmoothingValue);
    }

    private void RotateCamera()
    {
        Vector3 rotationVector = Vector3.zero;

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        transform.eulerAngles += rotationVector * Time.deltaTime * rotationSpeed;
    }

    private void MoveCamera()
    {
        Vector3 movementInput = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            movementInput.z = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementInput.x = -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementInput.z = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementInput.x = 1f;
        }

        Vector3 moveVector = transform.forward * movementInput.z + transform.right * movementInput.x;

        transform.position += moveVector * Time.deltaTime * moveSpeed;
    }
}