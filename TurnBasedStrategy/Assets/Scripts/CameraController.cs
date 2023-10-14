using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("MAIN COMPONENTS")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("MAIN PARAMETERS")]
    [SerializeField] private float moveSpeed;

    [Space(5)]
    [SerializeField] private float rotationSpeed;

    [Space(5)]
    [SerializeField] private float zoomAmount;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minZoomValue;
    [SerializeField] private float maxZoomValue;

    private CinemachineTransposer cinemachineTransposer;

    private Vector3 inputMoveDirection;
    private Vector3 rotationVector;
    private Vector3 followOffset;

    private void Start()
    {
        cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        followOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleCameraMovement();
        HandleCameraRotation();
        HandleCameraZoom();
    }

    private void HandleCameraMovement()
    {
        inputMoveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = 1f;
        }

        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;

        transform.position += moveVector * Time.deltaTime * moveSpeed;
    }

    private void HandleCameraRotation()
    {
        rotationVector = Vector3.zero;

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

    private void HandleCameraZoom()
    {
        float scrollDelta = Input.mouseScrollDelta.y;

        if (scrollDelta > 0f)
        {
            followOffset.y -= zoomAmount;
        }

        if (scrollDelta < 0f)
        {
            followOffset.y += zoomAmount;
        }

        followOffset = new Vector3(followOffset.x, 
            Mathf.Clamp(followOffset.y, minZoomValue, maxZoomValue), followOffset.z);

        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, 
            followOffset, zoomSpeed * Time.deltaTime);
    }
}