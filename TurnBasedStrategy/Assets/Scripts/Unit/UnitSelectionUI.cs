using UnityEngine;

public class UnitSelectionUI : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private MeshRenderer meshRenderer;

    private void OnEnable()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += Instance_OnSelectedUnitChanged;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged -= Instance_OnSelectedUnitChanged;
    }

    private void Instance_OnSelectedUnitChanged()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}