using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : SingletonMonoBehaviour<UnitActionSystem>
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction;

    public event Action<Unit> OnUnitSelected;
    public event Action<BaseAction> OnActionSelected;
    public event Action<bool> OnBusyChanged;

    private bool isBusy;

    private void Start()
    {
        if (selectedUnit != null)
        {
            SetSelectedUnit(selectedUnit);
        }
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            HandleUnitSelection();
        }

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance
                .GetGridPositionFromWorldPosition(MouseWorld.Instance.GetPositionOnGround());

            if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectedAction.Perform(mouseGridPosition, ClearBusy);
            }
        }
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        SetSelectedAction(selectedUnit.GetMoveAction());

        OnUnitSelected?.Invoke(selectedUnit);
        OnActionSelected?.Invoke(selectedAction);
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;        
        OnActionSelected?.Invoke(selectedAction);
    }

    private void SetBusy()
    {
        isBusy = true;
        OnBusyChanged?.Invoke(isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;
        OnBusyChanged?.Invoke(isBusy);
    }

    private void HandleUnitSelection()
    {
        Ray ray = MouseWorld.Instance.GetRayFromMousePosition();

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                if (selectedUnit == unit)
                {
                    return;
                }

                SetSelectedUnit(unit);
            }
        }
    }
}