using UnityEngine;

public class MouseWorld : SingletonMonoBehaviour<MouseWorld>
{
    [SerializeField] private Transform mouseVisualTransform;
    [SerializeField] private LayerMask groundLayerMask;

    private Camera cameraCache;

    protected override void Awake()
    {
        base.Awake();
        cameraCache = Camera.main;
    }

    private void Update()
    {
        mouseVisualTransform.position = GetPositionOnGround();
    }

    public Vector3 GetPositionOnGround()
    {
        Ray ray = GetRayFromMousePosition();
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, groundLayerMask);

        return raycastHit.point;
    }

    public Ray GetRayFromMousePosition()
    {
        return cameraCache.ScreenPointToRay(Input.mousePosition);
    }
}