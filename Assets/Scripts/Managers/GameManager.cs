using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private CatController catController;
    [SerializeField] private EggCounterUI eggCounterUI;
    [SerializeField] private WinLoseUI winLoseUI;
    [SerializeField] private PlayerHealthUI playerHealthUI;

    [Header("Settings")]
    [SerializeField] private int maxEggCount = 5;
    [SerializeField] private float delay = .5f; 
    
    private GameState currentGameState;
    private int currentEggCount;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        catController.OnCatCatched += CatContoller_OnCatCatched;
    }

    private void CatContoller_OnCatCatched()
    {
        playerHealthUI.AnimateDamageForAll();
        StartCoroutine(OnGameOver());
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver());
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }

    public void ChangeGameState(GameState state)
    {
        OnGameStateChanged?.Invoke(state);
        currentGameState = state;
        Debug.Log("Game State: " + state);
    } 

    public void OnEggCollected()
    {
        currentEggCount++;
        eggCounterUI.SetEggCounterText(currentEggCount, maxEggCount);

        if (currentEggCount == maxEggCount)
        {
            eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            winLoseUI.OnGameWin();
        }
    }

    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(delay);
        ChangeGameState(GameState.GameOver);
        winLoseUI.OnGameLose();
    }

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }
}
