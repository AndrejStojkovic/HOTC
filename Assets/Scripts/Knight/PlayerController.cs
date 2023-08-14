using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gm;
    public GameObject Player;
    public Transform PlayerTransform;
    public Animator PlayerAnimator;
    public Health Health;
    public PlayerHitManager HitManager;
    public int CurrentLane = 1;
    public float PlayerEndSpeed;
    private bool pendingChange;

    void Start() {
        gm = GameManager.Instance;

        gm.OnGameStateChanged.AddListener(GameStateChanged);
        Health.OnPlayerHit.AddListener(PlayerHit);
        Health.OnPlayerKill.AddListener(PlayerKilled);

        if(!PlayerTransform && Player) {
            PlayerTransform = Player.GetComponent<Transform>();
        }

        CurrentLane = 1;
        UpdatePlayerPosition();
    }

    void Update() {
        if(pendingChange) {
            UpdatePlayerPosition();
            pendingChange = false;
        }

        if(gm.GameOverState == GameOverState.Lose) {
            gameObject.transform.position += (Vector3.right * (PlayerEndSpeed / 100));
        }
    }

    void UpdatePlayerPosition() {
        var pos = PlayerTransform.transform.position;
        PlayerTransform.transform.position = new Vector3(pos.x, gm.Lanes[CurrentLane], pos.z);
    }

    public void SetLane(int idx) {
        CurrentLane = idx;
        pendingChange = true;
    }

    void PlayerHit(bool longAnim) {
        PlayerAnimator.Play(longAnim ? "Player_Hit_1" : "Player_Hit");
    }

    void PlayerWin() {
        // PlayerAnimator.Play("Player_Win");
    }

    void PlayerKilled() {
        PlayerAnimator.Play("Player_Death");
    }

    public void GameStateChanged(GameState newGameState) {
        if(newGameState == GameState.GameOver) {
            PlayerWin();
        }
    }
}
