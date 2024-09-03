using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : NetworkBehaviour
{
    public UnityEvent OnScoreChanged;
    private NetworkVariable<int> score = new NetworkVariable<int>();

    public int Score { get; private set; }

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged.Invoke();
    }
}
