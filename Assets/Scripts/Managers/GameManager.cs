using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private EggCounterUI eggCounterUI;
    [SerializeField] private WinLoseUI winLoseUI;

    [Header("Settings")]
    [SerializeField] private int maxEggCount = 5;
    
    private GameState currentGameState;
    private int currentEggCount;
    
    private void Awake()
    {
        Instance = this;
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

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }
}
