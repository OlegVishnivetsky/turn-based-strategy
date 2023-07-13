using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;

    [SerializeField] private LayerMask mousePlaneLayerMask;

    private Camera cameraCache;

    private void Awake()
    {
        instance = this;
        cameraCache = Camera.main;
    }

    public static Vector3 GetPosition()
    {
        Ray ray = instance.cameraCache.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, instance.mousePlaneLayerMask);

        return hitInfo.point;
    }
}