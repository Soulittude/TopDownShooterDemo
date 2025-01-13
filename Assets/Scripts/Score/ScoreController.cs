using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    public UnityEvent OnScoreChanged;
    public int score{get; private set;}

    public void AddScore(int value)
    {
        score += value;
        OnScoreChanged.Invoke();
    }
}
