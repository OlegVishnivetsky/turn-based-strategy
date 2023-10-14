using UnityEngine;
using UnityEngine.UI;

public class ActionBusyUI : MonoBehaviour
{
    [SerializeField] private Image busyImage;

    private void OnEnable()
    {
        UnitActionSystem.Instance.OnBusyChanged += Instance_OnBusyChanged;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.OnBusyChanged -= Instance_OnBusyChanged;
    }

    private void Instance_OnBusyChanged(bool isBusy)
    {
        busyImage.gameObject.SetActive(isBusy);
    }

    private void Start()
    {
        busyImage.gameObject.SetActive(false);
    }
}