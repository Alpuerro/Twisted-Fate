using System.Collections;
using EnemyInfo;
using SceneNamesspace;
using UnityEngine;
using Utils.Array;
using DG.Tweening;

public class GameLoop : MonoBehaviour
{
    public static GameLoop Instance;

    private int score;
    public GameState currentGameState;
    [Space(10)]
    public Enemy enemy;
    public Player player;
    public EnemyData[] enemieTypesList;
    public CanvasGroup victoryPanel;
    public CanvasGroup defeatPanel;

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
        player = GameObject.FindObjectOfType<Player>();
        GameLoopStart();
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
        StopAllCoroutines();
        if (SceneTransition.instance != null) SceneTransition.instance.FadeOutTransition();
        Debug.Log("GAME LOOP | Game loop start");
        PickLoopEnemy();
        // TD Enemy aparece
        await enemy.Show();
        GameEvents.GameStart.Invoke();
        RunGameState(GameState.PlayerTurn);
    }

    async void PlayerTurn()
    {
        Debug.Log("GAME LOOP | Player turn");
        _cardHand.DrawCard();
        _cardHand.DrawCard();
        _cardHand._playable = true;
        //el enemigo selecciona la accion para que el jugador pueda reaccionar a ella
        enemy.SetAction();
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
        await enemy.UpdateUI();
        if (!enemy.IsAlive()) { PlayerWins(); localGameState = GameState.End; }
        _cardHand._playable = false;
        RunGameState(localGameState);
    }

    async void EnemyTurn()
    {
        GameState localGameState = GameState.PlayerTurn;
        Debug.Log("GAME LOOP | Enemy turn");
        Debug.Log($"ENEMY | Health: {enemy.health}  Shield: {enemy.shield}");
        if (enemy.isStunned)
        {
            if (enemy.numberOfTurnsStunned <= 1) enemy.isStunned = false;
            enemy.numberOfTurnsStunned--;
            enemy.numberOfTurnsStunned = Mathf.Clamp(enemy.numberOfTurnsStunned, 0, enemy.enemyData.maxAccumulatedStuns);
        }
        else
        {
            // TD Se aplica el efecto de la accion del enemigo
            ApplyEnemyAction(ref enemy.GetAction());
            if (player.health <= 0) {
                EnemyWins();
                localGameState = GameState.End;
                RunGameState(localGameState);
                return;
            }

            void ApplyEnemyAction(ref EnemyAction enemyAction)
            {
                Debug.Log("ENEMY | Enemy action: " + System.Enum.GetName(typeof(EnemyActionTypes), enemyAction.enemyActionsType));
                switch (enemyAction.enemyActionsType)
                {
                    case EnemyActionTypes.Attack:
                        player.DamagePlayer(enemy.AttackDamage);
                        Debug.Log($"ENEMY | Damage {enemy.AttackDamage}");

                        break;
                    case EnemyActionTypes.Defend:
                        enemy.AddShield();
                        Debug.Log($"ENEMY | Shield {enemy.shield}");
                        break;
                }
            }
            //TODO esperar a que se reproduzca una animacion de daño y todas esas cosas
            await player.UpdateUIBars();
        }

        RunGameState(localGameState);
    }

    void EndLoop() { }
    #endregion

    void PickLoopEnemy()
    {
        enemy.SetEnemyData(enemieTypesList.GetRandomElement(), (int)SharedDataManager.GetDataByKey("round"));
        enemy.SetUI();
        enemy.SetAction();
    }

    void PlayerWins()
    {
        Debug.Log("GAME LOOP | Player wins");
        int round = (int)SharedDataManager.GetDataByKey("round");
        SharedDataManager.SetDataByKey("round", round + 1);
        FindObjectOfType<PlayerUIManager>().SetRoundText(round + 1);
        SharedDataManager.SetDataByKey("score", round);
        WinAnimation();
    }

    void EnemyWins()
    {
        Debug.Log("GAME LOOP | Enemy wins");
        if ((int)SharedDataManager.GetDataByKey("score") < (int)SharedDataManager.GetDataByKey("round"))
            SharedDataManager.SetDataByKey("score", (int)SharedDataManager.GetDataByKey("round"));
        DefeatAnimation();
    }

    private void WinAnimation()
    {
        victoryPanel.GetComponent<SoundSource>().PlaySound();
        Sequence sequence = DOTween.Sequence();
        victoryPanel.interactable = true;
        victoryPanel.blocksRaycasts = true;
        sequence.Append(victoryPanel.DOFade(1.0f, 1.0f));
        sequence.AppendCallback(() =>
        {
            RunGameState(GameState.Start);
        });
        sequence.Append(victoryPanel.DOFade(0.0f, 1.0f).SetDelay(1.0f));
        sequence.AppendCallback(() => {
            victoryPanel.interactable = false;
            victoryPanel.blocksRaycasts = false;
        });

        sequence.Play();
    }
    private void DefeatAnimation()
    {
        defeatPanel.GetComponent<SoundSource>().PlaySound();
        Sequence sequence = DOTween.Sequence();
        defeatPanel.interactable = true;
        defeatPanel.blocksRaycasts = true;
        sequence.Append(victoryPanel.DOFade(1.0f, 1.0f));
        sequence.AppendCallback(() =>
        {
            SceneTransition.instance.FadeInTransition(SceneNames.Menu, SceneNames.Combat, true, true);
        });

        sequence.Play();
    }

    public void Pause()
    {
        ScenesController.LoadScene(SceneNames.Pause);
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