using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private MeshRenderer selectedVisualMeshRenderer;

    private void OnEnable()
    {
        UnitActionSystem.Instance.OnUnitSelected += UnitActionSystem_OnUnitSelected;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.OnUnitSelected -= UnitActionSystem_OnUnitSelected;
    }

    private void UnitActionSystem_OnUnitSelected(Unit selectedUnit)
    {
        if (unit == selectedUnit)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        selectedVisualMeshRenderer.enabled = true;
    }

    public void Hide()
    {
        selectedVisualMeshRenderer.enabled = false;
    }
}