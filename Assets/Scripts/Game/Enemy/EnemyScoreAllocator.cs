using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int killScore;

    private ScoreController scoreController;

    private void Awake()
    {
        scoreController = FindObjectOfType<ScoreController>();
    }

    public void AllocateScore()
    {
        scoreController.AddScore(killScore);
    }
}
