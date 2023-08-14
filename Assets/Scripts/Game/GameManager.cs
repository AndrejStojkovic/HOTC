using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState { NotStarted, Active, Paused, GameOver };

public enum GameDifficulty { Easy = 0, Medium = 1, Hard = 2 };

public enum GameOverState { None, Lose, Win };

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if(_instance == null) {
                Debug.Log("Game Manager Instance = Null");
            }
            return _instance;
        }
    }

    public GameState GameState;
    public GameDifficulty GameDifficulty;
    public GameOverState GameOverState;
    public float GameTime;
    public float GameDuration;
    public bool TimePaused;
    
    public Image DistanceBar;
    public Animator BackgroundAnimator;
    public Animator LaneAnimator;
    public PlayerController PlayerController;
    public HandController HandController;
    public ItemManager ItemManager;
    public float[] Lanes;
    public string ObstacleTag = "Obstacle";

    [Header("Start Counter Screens")]
    public GameObject StartCounterGO;
    public TextMeshProUGUI CounterText;
    public GameObject CounterMessage;
    public int CounterTimer;

    [Header("Game Over Screens")]
    public Image GameOverDim;
    private float GameOverDelay;
    [Range(0f, 1f)]
    public float DimValue;
    public int WinSceneId;
    public int LoseSceneId;
    public float WinScreenDelay = 3f;
    public float LoseScreenDelay = 1.75f;
    public Image RedVig;

    public UnityEvent<GameState> OnGameStateChanged = new UnityEvent<GameState>();
    public UnityEvent OnGameDifficultyChanged = new UnityEvent();

    void Awake() {
        _instance = this;    
    }    

    void Start() {
        StartCoroutine(StartCounter());

        GameOverState = GameOverState.None;
        
        if(PlayerController != null) {
            PlayerController.Health.OnPlayerKill.AddListener(PlayerKilled);
        }

        UpdateDistanceBar();
        UpdateRedVig();

    }

    void Update() {
        if(GameState == GameState.GameOver) {
            return;
        }

        GameTime += TimePaused ? 0f : Time.deltaTime;
        UpdateDistanceBar();
        UpdateRedVig();

        if(GameTime >= GameDuration) {
            GameOverState = GameOverState.Lose;
            GameOverDelay = LoseScreenDelay;
            GameOver();
        }
    }

    void UpdateDistanceBar() {
        DistanceBar.fillAmount = GameTime / GameDuration;
    }

    void UpdateRedVig() {
        RedVig.color = new Color(0f, 0f, 0f, GameTime / GameDuration);
    }

    IEnumerator StartCounter() {
        Time.timeScale = 0.0f;
        StartCounterGO.SetActive(true);
        CounterText.gameObject.SetActive(true);
        CounterMessage.SetActive(false);
        for(int i = CounterTimer; i > 0; i--) {
            CounterText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        CounterText.gameObject.SetActive(false);
        CounterMessage.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        StartCounterGO.SetActive(false);
        Time.timeScale = 1.0f;
        GameState = GameState.Active;
    }

    void PlayerKilled() {
        GameOverState = GameOverState.Win;
        GameOverDelay = WinScreenDelay;
        GameOver();
    }

    void GameOver() {
        GameState = GameState.GameOver;
        OnGameStateChanged?.Invoke(GameState);
        BackgroundAnimator.speed = 0f;
        LaneAnimator.speed = 0f;
        Invoke("ChangeScene", GameOverDelay);
    }

    void ChangeScene() {
        SceneManager.LoadScene(GameOverState == GameOverState.Win ? WinSceneId : LoseSceneId);
    }
}
