using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform buttonContainer;

    private void OnEnable()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += Instance_OnSelectedUnitChanged;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged -= Instance_OnSelectedUnitChanged;
    }

    private void Start()
    {
        CreateUnitActionButtons();
    }

    private void Instance_OnSelectedUnitChanged()
    {
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in buttonContainer)
        {
            Destroy(buttonTransform.gameObject);
        }

        Unit unit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in unit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, buttonContainer);
            actionButtonTransform.GetComponent<ActionButtonUI>().SetButtonText(baseAction);
        }
    }
}