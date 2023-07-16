using System;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedImageObject;

    private BaseAction baseAction;

    public void SetButtonText(BaseAction action)
    {
        baseAction = action;
        text.text = action.GetActionName();

        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(action);
        });
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedAction =  UnitActionSystem.Instance.GetSelectedAction();

        if (baseAction == selectedAction)
        {
            EnableSelectedImage();
        }
        else
        {
            DisableSelectedImage();
        }
    }

    public void EnableSelectedImage()
    {
        selectedImageObject.SetActive(true);
    }

    public void DisableSelectedImage()
    {
        selectedImageObject.SetActive(false);
    }
}