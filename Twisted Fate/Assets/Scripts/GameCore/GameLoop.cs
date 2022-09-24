using EnemyInfo.EnemyAction;
using SceneNamesspace;
using UnityEngine;
using Utils.Array;

public class GameLoop : MonoBehaviour
{
    public static GameLoop Instance;

    public GameState currentGameState;
    private int round;

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


    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        currentGameState = GameState.Start;
        if (SceneTransition.instance != null) SceneTransition.instance.FadeOutTransition();
        GameLoopStart();
    }

    private void Update()
    {

    }

    private void RunGameState(GameState gameState)
    {
        currentGameState = gameState;
        switch (gameState)
        {
            case GameState.Pause: break;
            case GameState.Start: GameLoopStart(); break;
            case GameState.PlayerTurn: PlayerTurn(); break;
            case GameState.PlayerAction: PlayerAction(); break;
            case GameState.EnemyTurn: EnemyTurn(); break;
            case GameState.End: EndLoop(); break;
            default: Debug.LogError("GAME LOOP | Entered an undefined game state"); break;
        }
    }

    #region States Functions
    async void GameLoopStart()
    {
        Debug.Log("GAME LOOP | Game loop start");
        PickLoopEnemy();
        // TD Enemy aparece
        await enemy.Show();
        GameEvents.GameStart.Invoke();
        RunGameState(GameState.PlayerTurn);
    }

    async void PlayerTurn()
    {
        Debug.Log($"ENEMY | Health: {enemy.health}  Shield: {enemy.shield}");
        Debug.Log("GAME LOOP | Player turn");
        _cardHand.DrawCard();
        _cardHand.DrawCard();
        _cardHand._playable = true;
        //Selecciona las cartas que vaya a jugar y le da a pasar turno
        await _cardZone.PlayCards();
        RunGameState(GameState.PlayerAction);
    }

    async void PlayerAction()
    {
        Debug.Log("GAME LOOP | Player action");
        GameState localGameState = GameState.EnemyTurn;
        //Se calcula lo que hacen las cartas
        await TaskUtils.WaitUntil(() => nextDamageAmount != -1);
        enemy.TakeDamage(nextDamageAmount);
        if (!enemy.IsAlive()) { PlayerWins(); localGameState = GameState.End; }
        _cardHand._playable = false;
        RunGameState(localGameState);
    }

    void EnemyTurn()
    {
        GameState localGameState = GameState.PlayerTurn;
        Debug.Log("GAME LOOP | Enemy turn");
        Debug.Log($"ENEMY | Health: {enemy.health}  Shield: {enemy.shield}");
        if (enemy.isStunned)
        {
            if (enemy.numberOfTurnsStunned > 0) Debug.Log("ENEMY | Not stunned");
            if (enemy.numberOfTurnsStunned > 0) enemy.isStunned = false;
            else enemy.numberOfTurnsStunned--;
            enemy.SetAction();
        }
        else
        {
            // TD Se aplica el efecto de la accion del enemigo
            ApplyEnemyAction(ref enemy.GetAction());
            enemy.SetAction();
            if (player.health <= 0) { EnemyWins(); localGameState = GameState.End; }

            void ApplyEnemyAction(ref EnemyAction enemyAction)
            {
                Debug.Log("ENEMY | Enemy action: " + System.Enum.GetName(typeof(EnemyActionsType), enemyAction.enemyActionsType));
                switch (enemyAction.enemyActionsType)
                {
                    case EnemyActionsType.Attack:
                        player.DamagePlayer(enemy.AttackDamage);
                        Debug.Log($"ENEMY | Damage {enemy.AttackDamage}");

                        break;
                    case EnemyActionsType.Defend:
                        enemy.AddShield();
                        Debug.Log($"ENEMY | Shield {enemy.shield}");
                        break;
                }
            }
            //TODO esperar a que se reproduzca una animacion de daño y todas esas cosas
        }

        RunGameState(localGameState);
    }

    void EndLoop() { }
    #endregion

    void PickLoopEnemy()
    {
        enemy.SetEnemyData(_enemiesList.GetRandomElement());
        enemy.SetUI();
        enemy.SetAction();
    }

    void PlayerWins() { Debug.Log("GAME LOOP | Player wins"); }
    void EnemyWins() { Debug.Log("GAME LOOP | Enemy wins"); }
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