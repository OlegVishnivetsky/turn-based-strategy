using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> actionButtonList = new List<ActionButtonUI>();

    private void OnEnable()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += Instance_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += Instance_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += Instance_OnActionStarted;
        TurnSystem.Instance.OnTurnNumberChanged += Instance_OnTurnNumberChanged;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged -= Instance_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged -= Instance_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted -= Instance_OnActionStarted;
        TurnSystem.Instance.OnTurnNumberChanged -= Instance_OnTurnNumberChanged;
    }

    private void Start()
    {
        CreateUnitActionButtons();
        UpdateSelectionVisual();
        UpdateActionPointsText();
    }

    private void Instance_OnSelectedUnitChanged()
    {
        CreateUnitActionButtons();
        UpdateSelectionVisual();
        UpdateActionPointsText();
    }

    private void Instance_OnActionStarted()
    {
        UpdateActionPointsText();
    }

    private void Instance_OnSelectedActionChanged()
    {
        UpdateSelectionVisual();
    }

    private void Instance_OnTurnNumberChanged(int turnNumber)
    {
        UpdateActionPointsText();
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in buttonContainer)
        {
            Destroy(buttonTransform.gameObject);
        }

        actionButtonList.Clear();

        Unit unit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in unit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, buttonContainer);

            ActionButtonUI actionButton = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButton.SetButtonText(baseAction);

            actionButtonList.Add(actionButton);
        }
    }

    public void UpdateSelectionVisual()
    {
        foreach (ActionButtonUI actionButton in actionButtonList)
        {
            actionButton.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPointsText()
    {
        Unit unit = UnitActionSystem.Instance.GetSelectedUnit();

        actionPointsText.text = "Action Points: " + unit.GetActionPoints();
    }
}