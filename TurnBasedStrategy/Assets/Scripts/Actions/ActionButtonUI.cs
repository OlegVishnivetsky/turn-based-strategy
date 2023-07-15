using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;

    public void SetButtonText(BaseAction action)
    {
        text.text = action.GetActionName();
    }
}