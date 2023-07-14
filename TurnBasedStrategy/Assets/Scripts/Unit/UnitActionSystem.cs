using System;
using UnityEngine;

public class UnitActionSystem : SingletonMonobehaviour<UnitActionSystem> 
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    public event Action OnSelectedUnitChanged;

    private Camera cameraCache;

    protected override void Awake()
    {
        base.Awake();
        cameraCache = Camera.main;
    }

    private void Start()
    {
        if (selectedUnit != null)
        {
            OnSelectedUnitChanged?.Invoke();
        }
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

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    private void MoveSelectedUnit()
    {
        if (selectedUnit != null)
        {
            selectedUnit.SetTargetPosition(MouseWorld.GetMousePosition());
        }
    }

    private void HandleUnitSelection()
    {
        Ray ray = cameraCache.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out Unit unit))
            {
                if (selectedUnit != unit)
                {
                    selectedUnit = unit;
                    OnSelectedUnitChanged?.Invoke();
                }
            }
        }
    }
}