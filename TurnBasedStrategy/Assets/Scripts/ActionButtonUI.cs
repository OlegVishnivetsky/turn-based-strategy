using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image selectedImage;
    [SerializeField] private TextMeshProUGUI buttonText;

    private BaseAction baseAction;

    public void SetUp(string text, BaseAction action)
    {
        baseAction = action;
        buttonText.text = text;

        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(action);
        });
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();

        selectedImage.enabled = (baseAction == selectedAction);
    }
}