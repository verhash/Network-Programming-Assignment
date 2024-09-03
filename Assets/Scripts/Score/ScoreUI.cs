using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class ScoreUI : NetworkBehaviour
{
    private TMP_Text scoreText;
    private NetworkVariable<string> scoreUI = new NetworkVariable<string>();

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    public void UpdateScore(ScoreController scoreController)
    {
        scoreText.text = $"Score: {scoreController.Score}";
    }
}
