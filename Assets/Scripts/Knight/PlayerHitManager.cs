using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitManager : MonoBehaviour
{
    private GameManager gm;
    public PlayerController PlayerController;

    void Start() {
        gm = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == gm.ObstacleTag) {
            var item = other.gameObject.GetComponent<Obstacle>();
            PlayerController.Health.TakeDamage(item.DamageAmount);
        }
    }
}
