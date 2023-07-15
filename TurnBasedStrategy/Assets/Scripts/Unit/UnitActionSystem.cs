using System;
using UnityEngine;

public class UnitActionSystem : SingletonMonobehaviour<UnitActionSystem> 
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    public event Action OnSelectedUnitChanged;

    private Camera cameraCache;

    private bool isBusy;

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
        if (isBusy)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetBusy();
            MoveSelectedUnit();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            HandleUnitSelection();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
        }
    }

    public void SetBusy()
    {
        isBusy = true;
    }

    public void ClearBusy()
    {
        isBusy = false;
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    private void MoveSelectedUnit()
    {
        if (selectedUnit != null)
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.
                GetGridPosition(MouseWorld.GetMousePosition());

            if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                selectedUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
            }
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