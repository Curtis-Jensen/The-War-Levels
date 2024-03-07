using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.instance.GenerateGrid();
                ChangeState(GameState.SpawnAllies);
                break;
            case GameState.SpawnAllies:
                break;
            case GameState.SpawnEnemies:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnAllies = 1,
    SpawnEnemies = 2,
    AlliesTurn = 3,
    EnemiesTurn = 4
}
