using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private ActionButtonUI actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;

    private List<ActionButtonUI> actionButtons = new List<ActionButtonUI>();

    private void OnEnable()
    {
        UnitActionSystem.Instance.OnUnitSelected += UnitActionSystem_OnUnitSelected;
        UnitActionSystem.Instance.OnActionSelected += Instance_OnActionSelected;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.OnUnitSelected -= UnitActionSystem_OnUnitSelected;
        UnitActionSystem.Instance.OnActionSelected -= Instance_OnActionSelected;
    }

    private void Start()
    {
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnUnitSelected(Unit unit)
    {
        CreateUnitActionButtons(unit);      
    }

    private void Instance_OnActionSelected(BaseAction obj)
    {
        UpdateSelectedVisual();
    }

    private void CreateUnitActionButtons(Unit unit)
    {
        ClearAllActionButtons();

        foreach (BaseAction action in unit.GetUnitActions())
        {
            ActionButtonUI actionButtonObject = Instantiate(actionButtonPrefab, actionButtonContainerTransform);

            actionButtonObject.SetUp(action.GetActionName(), action);
            actionButtons.Add(actionButtonObject);
        }
    }

    private void ClearAllActionButtons()
    {
        actionButtons.Clear();

        foreach (Transform actionButtonTransform in actionButtonContainerTransform)
        {
            Destroy(actionButtonTransform.gameObject);
        }
    }

    public void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButton in actionButtons)
        {
            actionButton.UpdateSelectedVisual();
        }
    }
}