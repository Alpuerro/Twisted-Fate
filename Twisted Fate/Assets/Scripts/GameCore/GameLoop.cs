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

    [Header("Cards variables")]
    [SerializeField] CardDeck _deck;
    [SerializeField] CardHand _cardHand;
    [SerializeField] CardsPlayedZone _cardZone;

    public int nextDamageAmount = -1;


    bool _isGameLoopPaused;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        gameState = GameState.Start;
        if(SceneTransition.instance != null) SceneTransition.instance.FadeOutTransition();
        GameLoopStart();
    }

    private void Update()
    {
        if (Input.anyKey) ManageInput();
    }

    private void RunCurrentState()
    {
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
    }

    #region States Functions
    async void GameLoopStart()
    {
        Debug.Log("Game loop start.");
        PickLoopEnemy();
        // TD Enemy aparece
        await enemy.Show();
        GameEvents.GameStart.Invoke();
        gameState = GameState.PlayerTurn;
        RunCurrentState();
    }

    async void PlayerTurn()
    {
        Debug.Log("Player turn.");
        _cardHand.DrawCard();
        _cardHand.DrawCard();
        _cardHand._playable = true;
        //Selecciona las cartas que vaya a jugar y le da a pasar turno
        await _cardZone.PlayCards();
        gameState = GameState.PlayerAction;
        RunCurrentState();
    }

    async void PlayerAction()
    {
        Debug.Log("Player action.");
        //Se calcula lo que hacen las cartas
        await TaskUtils.WaitUntil(() => nextDamageAmount != -1);
        if (enemy.TakeDamage(nextDamageAmount)) PlayerWin();
        _cardHand._playable = false;
        gameState = GameState.EnemyTurn;
        RunCurrentState();
    }

    void EnemyTurn()
    {
        Debug.Log("Enemy turn");
        if (enemy.isStunned)
        {
            if (enemy.numberOfTurnsStunned > 0)
            {
                enemy.isStunned = false;
            }
            else
            {
                enemy.numberOfTurnsStunned--;
            }
            gameState = GameState.PlayerTurn;
        }
        else
        {
            // TD Se aplica el efecto de la accion del enemigo
            ApplyEnemyAction(ref enemy.GetAction());
            enemy.SetAction();
            gameState = GameState.PlayerTurn;
            if (player.health <= 0) EnemyWin();

            void ApplyEnemyAction(ref EnemyAction enemyAction)
            {
                Debug.Log("Enemy action:" + enemyAction.ToString());
                if (enemyAction.enemyActionsType == EnemyActionsType.Attack)
                    player.DamagePlayer(enemy.AttackDamage);
                if (enemyAction.enemyActionsType == EnemyActionsType.Defend)
                    enemy.AddShield();
            }
            //TODO esperar a que se reproduzca una animacion de daño y todas esas cosas
        }
        RunCurrentState();
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

public enum GameState
{
    Pause,
    Start,
    PlayerTurn,
    EnemyTurn,
    PlayerAction,
    End
}