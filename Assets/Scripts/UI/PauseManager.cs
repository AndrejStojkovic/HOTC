using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private GameManager gm;
    public GameObject PauseMenu;
    public Image Background;
    [Range(0f, 1f)]
    public float Opacity;

    public List<GameObject> ShowObjects;
    public List<GameObject> HideObjects;

    private bool pauseState;
    private bool pendingChange;

    void Start() {
        gm = GameManager.Instance;
        Background.color = new Color(0f, 0f, 0f, Opacity);
        pauseState = false;
        pendingChange = false;
        PauseMenu.SetActive(pauseState);
    }

    void Update() {
        if(gm.GameState == GameState.Active) {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                pauseState = !pauseState;
                pendingChange = true;
            }

            HandlePause();
        }
    }

    void HandlePause() {
        if(pendingChange) {
            Time.timeScale = pauseState ? 0f : 1f;
            PauseMenu.SetActive(pauseState);
            gm.TimePaused = pauseState;
            pendingChange = false;
            // SetObjects(HideObjects, !pauseState);
            // SetObjects(ShowObjects, pauseState);
        }
    }

    public void Pause() {
        pauseState = !pauseState;
        pendingChange = true;
    }

    protected void SetObjects(List<GameObject> objects, bool state) {
        for(int i = 0; i < objects.Count; i++) {
            objects[i].SetActive(state);
        }
    }
}
