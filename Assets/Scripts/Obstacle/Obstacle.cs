using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject Prefab;
    public float Speed;
    [Min(1)]
    public int DamageAmount = 1;
    public int CurrentLane;

    void Update() {
        gameObject.transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}
