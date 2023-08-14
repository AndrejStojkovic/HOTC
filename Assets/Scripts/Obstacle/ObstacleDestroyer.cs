using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    private GameManager gm;

    void Start() {
        gm = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == gm.ObstacleTag) {
            Destroy(other.gameObject);
        }
    }
}
