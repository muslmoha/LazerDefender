﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;
    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
