using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            unit.GetMoveAction().GetValidGridPositionList();
            GridSystemVisual.Instance.ShowGridVisualAtGridPositionList(unit.GetMoveAction()
                .GetValidGridPositionList());
        }
    }
}