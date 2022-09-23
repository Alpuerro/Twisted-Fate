using EnemyInfo.EnemyAction;
using SceneNamesspace;
using UnityEngine;
using Utils.Array;

public class GameLoop : MonoBehaviour
{
    public static GameLoop Instance;

    public GameState gameState;
    private GameState _previousState;

    /// <summary> All existing enemies. </summary>
    [SerializeField]
    [Tooltip("All existing enemies.")]
    EnemyData[] _enemiesList;

    /// <summary> The enemy in the current loop. </summary>
    [Tooltip("The enemy in the current loop.")]
    public Enemy enemy;
    public Player player;


    bool _isGameLoopPaused;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        gameState = GameState.Start;
    }

    private void Update()
    {
        if (Input.anyKey) ManageInput();

        switch (gameState)
        {
            case GameState.Pause: break;
            case GameState.Start: GameLoopStart(); break;
            case GameState.PlayerTurn: PlayerTurn(); break;
            // Realizar la acción, comprobar si ha terminado la partida y pasar al turno del enemigo
            case GameState.PlayerAction: PlayerAction(); break;
            // Escoger una acción aleatoria, hacerla, comprobar si ha terminado la partida y pasar turno
            case GameState.EnemyTurn: EnemyTurn(); break;
            // Escoger una acción aleatoria, hacerla, comprobar si ha terminado la partida y pasar turno
            case GameState.End: EndLoop(); break;
            default: Debug.LogError("Entered an undefined game state."); break;
        }

        #region States Functions
        void GameLoopStart()
        {
            Debug.Log("Game loop start.");
            PickLoopEnemy();
            // TD Enemy aparece
            // TD Se ponen las cartas en la mano
            gameState = GameState.PlayerTurn;
        }

        void PlayerTurn()
        {
            Debug.Log("Player turn.");
            // TD Se desbloquea el interactuar con las cartas
            // TD Selecciona las cartas que vaya a jugar y le da a pasar turno
            // if (le ha dado a pasar)
            gameState = GameState.PlayerAction;
        }

        void PlayerAction()
        {
            Debug.Log("Player action.");
            // TD Se calcula lo que hacen las cartas
            if (enemy.TakeDamage(0)) PlayerWin();
            gameState = GameState.EnemyTurn;
        }

        void EnemyTurn()
        {
            Debug.Log("Enemy turn");
            if (enemy.isStunned)
            {
                enemy.isStunned = false;
                gameState = GameState.PlayerTurn;
                return;
            }

            // TD Se aplica el efecto de la accion del enemigo
            ApplyEnemyAction(ref enemy.GetAction());
            enemy.SetAction();
            gameState = GameState.PlayerTurn;
            if (player.health <= 0) EnemyWin();

            void ApplyEnemyAction(ref EnemyAction enemyAction)
            {
                if (enemyAction.enemyActionsType == EnemyActionsType.Attack)
                    player.DamagePlayer(enemy.AttackDamage);
                if (enemyAction.enemyActionsType == EnemyActionsType.Defend)
                    enemy.AddShield();
            }
        }

        void EndLoop() { }
        #endregion

        void PickLoopEnemy()
        {
            enemy.enemyData = _enemiesList.GetRandomElement();
            enemy.SetAction();
        }

        void PlayerWin() { }
        void EnemyWin() { }

        void ManageInput()
        {
            if (Input.GetKeyDown("p"))
            {
                if (!_isGameLoopPaused)
                {
                    Debug.Log("Load Pause");
                    _isGameLoopPaused = !_isGameLoopPaused;
                    _previousState = gameState;
                    gameState = GameState.Pause;
                    ScenesController.LoadScene(SceneNames.Pause);
                }
                else
                {
                    Debug.Log("Unload Pause");
                    _isGameLoopPaused = !_isGameLoopPaused;
                    gameState = _previousState;
                    ScenesController.UnloadScene(SceneNames.Pause);
                }
            }
        }
    }
}

public enum GameState
{
    Pause,
    Start,
    PlayerTurn,
    EnemyTurn,
    PlayerAction,
    End
}