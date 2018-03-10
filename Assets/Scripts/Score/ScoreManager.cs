using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    private static int Score;

    public static int GetScore()
    {
        return Score;
    }

    public static void AddScore(int s)
    {
        Score += s;
    }

    public static void InitScore()
    {
        Score = 0;
    }
}
