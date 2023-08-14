using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMan : MonoBehaviour
{
    private GameManager gm;
    public float StartPosition;
    public float EndPosition;

    void Start() {
        gm = GameManager.Instance;
        gm.OnGameStateChanged.AddListener(OnGameOver);
        transform.gameObject.SetActive(true);
        transform.localPosition = new Vector3(StartPosition, transform.localPosition.y, transform.localPosition.z);
    }

    void Update() {
        if(gm.GameState == GameState.Active) {
            float x = Mathf.Lerp(StartPosition, EndPosition, gm.GameTime / gm.GameDuration);
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }
    }

    public void OnGameOver(GameState gameState) {
        if(gameState == GameState.GameOver) {
            transform.gameObject.SetActive(false);
        }
    }
}
