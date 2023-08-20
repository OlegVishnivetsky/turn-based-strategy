using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private void OnEnable()
    {
        TurnSystem.Instance.OnTurnChanged += Instance_OnTurnChanged;
    }

    private void OnDisable()
    {
        TurnSystem.Instance.OnTurnChanged -= Instance_OnTurnChanged;
    }

    private void Instance_OnTurnChanged()
    {
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            StartCoroutine(EndEnemyTurnRoutine());
        }
    }

    private IEnumerator EndEnemyTurnRoutine()
    {
        yield return new WaitForSeconds(2f);

        TurnSystem.Instance.NextTurn();
    }
}