using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public GameState gameState;

    /// <summary> All existing enemies. </summary>
    [SerializeField]
    [Tooltip("All existing enemies.")]
    Enemy[] _enemiesList;

    /// <summary> The enemy in the current loop. </summary>
    [SerializeField]
    [Tooltip("The enemy in the current loop.")]
    Enemy _currentEnemy;

    bool _isGameLoopPaused;

    void Start()
    {
        gameState = GameState.Start;
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Pause:
                break;
            case GameState.Start:
                // Setear las cartas, elegir el enemigo y su acción y pasar al turno del jugador.
                GameLoopStart();
                break;
            case GameState.PlayerTurn:
                // Navegar por las cartas, poner las que vaya a jugar y pasar a acción
                PlayerTurn();
                break;
            case GameState.EnemyTurn:
                // Escoger una acción, de momento aleatoria, y pasar a acción
                EnemyTurn();
                break;
            case GameState.PlayerAction:
                // Realizar la acción, comprobar si ha terminado la partida y pasar al turno del enemigo
                PlayerAction();
                break;
            case GameState.EnemyAction:
                // Realizar la acción, comprobar si ha terminado la partida y pasar al turno del jugador
                EnemyAction();
                break;
            default: Debug.LogError("Entered an undefined game state."); break;
        }
    }

    void Pause()
    {
        if (!_isGameLoopPaused)
        {
            Debug.Log("Game loop paused.");
            _isGameLoopPaused = true;
        }
    }
    void GameLoopStart()
    {
        Debug.Log("Game loop start.");
        gameState = GameState.PlayerTurn;
    }
    void PlayerTurn()
    {
        Debug.Log("Player turn.");
        gameState = GameState.PlayerAction;
    }
    void EnemyTurn()
    {
        Debug.Log("Enemy turn");
        gameState = GameState.EnemyAction;
    }
    void PlayerAction()
    {
        Debug.Log("Player action.");
        gameState = GameState.EnemyTurn;
    }
    void EnemyAction()
    {
        Debug.Log("Player action.");
        gameState = (GameState.PlayerTurn);
    }
}

public enum GameState
{
    Pause,
    Start,
    PlayerTurn,
    EnemyTurn,
    PlayerAction,
    EnemyAction
}