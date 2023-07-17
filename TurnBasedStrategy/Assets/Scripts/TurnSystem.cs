using System;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private int turnNumber = 1;

    public static event Action<int> OnTurnNumberChanged;

    private void Start()
    {
        OnTurnNumberChanged?.Invoke(turnNumber);
    }

    public void NextTurn()
    {
        turnNumber++;
        OnTurnNumberChanged?.Invoke(turnNumber);
    }
}