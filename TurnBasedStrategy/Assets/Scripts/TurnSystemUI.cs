using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [Header("UI COMPONENTS")]
    [SerializeField] private TextMeshProUGUI turnNumberText;

    private void OnEnable()
    {
        TurnSystem.OnTurnNumberChanged += TurnSystem_OnTurnNumberChanged;
    }

    private void OnDisable()
    {
        TurnSystem.OnTurnNumberChanged -= TurnSystem_OnTurnNumberChanged;
    }

    private void TurnSystem_OnTurnNumberChanged(int turnNumber)
    {
        UpdateTurnNumberText(turnNumber);
    }

    public void UpdateTurnNumberText(int turnNumber)
    {
        turnNumberText.text = "turn " + turnNumber;
    }
}