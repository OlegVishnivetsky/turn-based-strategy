using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private Camera cameraCache;

    private void Awake()
    {
        cameraCache = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveSelectedUnit();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            HandleUnitSelection();
        }
    }

    private void MoveSelectedUnit()
    {
        if (selectedUnit != null)
        {
            selectedUnit.SetTargetPosition(MouseWorld.GetPosition());
        }
    }

    private void HandleUnitSelection()
    {
        Ray ray = cameraCache.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out Unit unit))
            {
                selectedUnit = unit;
            }
        }
    }
}