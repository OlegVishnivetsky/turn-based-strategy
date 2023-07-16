using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    [SerializeField] private GameObject blockerObject;

    private void OnEnable()
    {
        UnitActionSystem.Instance.OnBusyChanged += Instance_OnBusyChanged;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.OnBusyChanged -= Instance_OnBusyChanged;
    }

    private void Start()
    {
        Hide();
    }

    private void Instance_OnBusyChanged(bool isBusy)
    {
        if (isBusy)
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
        blockerObject.SetActive(true);
    }

    public void Hide()
    {
        blockerObject.SetActive(false);
    }
}