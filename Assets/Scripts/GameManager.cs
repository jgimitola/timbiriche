using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        start,
        player1,
        player2,
        end
    }

    public static GameManager Instance;
    private GameState _gameState;

    public GameState GetState => _gameState;

    public void UpdateGameState(GameState gameState)
    {
        _gameState = gameState;
    }

    public void SwitchPlayer()
    {
        if (_gameState == GameState.player1)
        {
            _gameState = GameState.player2;
        }
        else
        {
            _gameState = GameState.player1;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameState = GameState.start;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_gameState)
        {
            case GameState.start:
                UpdateGameState(GameState.player1);
                break;
            case GameState.player1:
                break;
            case GameState.player2:
                break;
            case GameState.end:
                break;
        }

    }
}
