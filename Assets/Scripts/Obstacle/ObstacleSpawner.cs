using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private GameManager gm;
    public bool GlobalItemCooldown;
    public float GlobalItemCooldownDuration;
    public float StartPositionX;
    private float startTime;

    void Start() {
        gm = GameManager.Instance;        
        startTime = gm.GameTime;
        gm.ItemManager.ItemSelected.AddListener(SpawnObstacle);
    }

    public void SpawnObstacle(Item item) {
        if(item.ItemSpawnCooldown && item.LocalStartTime != 0 && gm.GameTime - item.LocalStartTime <= item.ItemSpawnCooldownDuration) {
            // TO-DO
            // Play Error SFX
            return;
        }

        if(GlobalItemCooldown && startTime != 0 && gm.GameTime - startTime <= GlobalItemCooldownDuration) {
            // TO-DO
            // Play Error SFX
            return;
        }

        if(item.Amount > 0) {
            Vector3 pos = new Vector3(StartPositionX, gm.Lanes[gm.HandController.CurrentLane], 0f);
            GameObject obstacle = Instantiate(item.Prefab, pos, Quaternion.identity);
            Obstacle ob = obstacle.GetComponent<Obstacle>();
            ob.CurrentLane = gm.HandController.CurrentLane;
            ob.DamageAmount = item.DamageAmount;
            item.Amount--;
            startTime = gm.GameTime;
            item.LocalStartTime = gm.GameTime;
        }
    }
}
