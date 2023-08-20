using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [Header("UI COMPONENTS")]
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private GameObject enemyTurnVisualGameObject;

    private void OnEnable()
    {
        TurnSystem.Instance.OnTurnChanged += Instance_OnTurnChanged;
        TurnSystem.Instance.OnTurnNumberChanged += TurnSystem_OnTurnNumberChanged;
    }

    private void OnDisable()
    {
        TurnSystem.Instance.OnTurnChanged -= Instance_OnTurnChanged;
        TurnSystem.Instance.OnTurnNumberChanged -= TurnSystem_OnTurnNumberChanged;
    }

    private void Start()
    {
        endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonInteractivity();
    }

    private void Instance_OnTurnChanged()
    {
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonInteractivity();
    }

    private void TurnSystem_OnTurnNumberChanged(int turnNumber)
    {
        UpdateTurnNumberText(turnNumber);
    }

    public void UpdateTurnNumberText(int turnNumber)
    {
        turnNumberText.text = "turn " + turnNumber;
    }

    public void UpdateEnemyTurnVisual()
    {
        enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    public void UpdateEndTurnButtonInteractivity()
    {
        endTurnButton.interactable = TurnSystem.Instance.IsPlayerTurn();
    }
}