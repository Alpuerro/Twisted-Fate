using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    GameState gameState;

    /// <summary> All existing enemies. </summary>
    [SerializeField]
    [Tooltip("All existing enemies.")]
    Enemy[] _enemiesList;

    /// <summary> The enemy in the current loop. </summary>
    [SerializeField]
    [Tooltip("The enemy in the current loop.")]
    Enemy _currentEnemy;

    void Start() { }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Menu:
                break;
            case GameState.Start:
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.Action:
                break;
            default: Debug.LogError("Entered an undefined game state."); break;
        }
    }

    void GameStart() { }
    void PlayerTurn() { }
    void EnemyTurn() { }
    void Action() { }
}

enum GameState
{
    Menu,
    Start,
    PlayerTurn,
    EnemyTurn,
    Action
}