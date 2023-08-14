using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    private GameManager gm;
    public PlayerController PlayerController;
    public ObstacleDetector Detection;
    public float[] ReactionDifficulty;
    [Min(0)]
    public float MinIdleTime;
    public float MaxIdleTime;
    private float currentIdle;
    private float MaxReactionTime;
    private float lastMoveTime;

    void Start() {
        gm = GameManager.Instance;
        int gdIdx = (int)gm.GameDifficulty;
        MaxReactionTime = ReactionDifficulty[gdIdx];
        Detection.OnObstacleDetected.AddListener(ObstacleDetected);
        lastMoveTime = gm.GameTime;
        currentIdle = MinIdleTime;
    }

    void Update() {
        if(gm.GameTime - lastMoveTime >= currentIdle) {
            MovePlayer();
        }
    }

    void ObstacleDetected() {
        float rand = Random.Range(0f, MaxReactionTime);
        Debug.Log(rand);
        Invoke("MovePlayer", rand);
    }

    void MovePlayer() {
        Debug.Log("Should Move");
        int newLane;

        if(PlayerController.CurrentLane == 0 || PlayerController.CurrentLane == 2) {
            newLane = 1;
        } else {
            newLane = Random.Range(0, 2) == 0 ? 0 : 2;
        }

        Debug.Log(newLane);

        PlayerController.SetLane(newLane);

        lastMoveTime = gm.GameTime;
        currentIdle = Random.Range(MinIdleTime, MaxIdleTime);
    }
}
