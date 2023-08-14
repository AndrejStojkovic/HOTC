using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleDetector : MonoBehaviour
{
    private GameManager gm;
    public UnityEvent OnObstacleDetected = new UnityEvent();
    
    void Start() {
        gm = GameManager.Instance;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == gm.ObstacleTag) {
            OnObstacleDetected?.Invoke();
        }
    }
}
