using System;

public class TurnSystem : SingletonMonobehaviour<TurnSystem>
{
    private int turnNumber = 1;

    public event Action OnTurnChanged;
    public event Action<int> OnTurnNumberChanged;

    private bool isPlayerTurn = true;

    private void Start()
    {
        OnTurnNumberChanged?.Invoke(turnNumber);
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }

    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;

        OnTurnChanged?.Invoke();
        OnTurnNumberChanged?.Invoke(turnNumber);
    }
}