using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandController : MonoBehaviour
{
    private GameManager gm;
    public GameObject Hand;
    public Transform HandTransform;
    public GameObject[] LaneHighlights;
    public int CurrentLane = 1;
    private bool pendingChange;

    public UnityEvent<int> OnLaneChange = new UnityEvent<int>();

    void Start() {
        gm = GameManager.Instance;

        gm.OnGameStateChanged.AddListener(GameStateChanged);

        if(!HandTransform && Hand) {
            HandTransform = Hand.GetComponent<Transform>();
        }

        CurrentLane = 1;
        OnLaneChange?.Invoke(CurrentLane);
        UpdateHandPosition();
    }

    void Update() {
        if(gm.GameState == GameState.Active) {
            if(pendingChange) {
                UpdateHandPosition();
                pendingChange = false;
            }

            if(Input.GetKeyDown(KeyCode.UpArrow) && CurrentLane > 0) {
                CurrentLane--;
                pendingChange = true;
            } else if(Input.GetKeyDown(KeyCode.DownArrow) && CurrentLane < gm.Lanes.Length - 1) {
                CurrentLane++;
                pendingChange = true;
            }
        }
    }

    void UpdateHandPosition() {
        var pos = HandTransform.transform.position;
        HandTransform.transform.position = new Vector3(pos.x, gm.Lanes[CurrentLane], pos.z);
        OnLaneChange?.Invoke(CurrentLane);
        UpdateLaneHighlight();
    }

    void UpdateLaneHighlight() {
        for(int i = 0; i < LaneHighlights.Length; i++) {
            LaneHighlights[i].SetActive(CurrentLane == i);
        }
    }

    public void GameStateChanged(GameState newGameState) {
        if(newGameState == GameState.GameOver) {
            CurrentLane = -1;
            UpdateLaneHighlight();
            gameObject.SetActive(false);
        }
    }
}
