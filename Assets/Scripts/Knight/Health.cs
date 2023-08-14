using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public PlayerController PlayerController;
    public int Hearts;

    public UnityEvent OnDamageTaken = new UnityEvent();
    public UnityEvent<bool> OnPlayerHit = new UnityEvent<bool>();
    public UnityEvent OnPlayerKill = new UnityEvent();

    public void TakeDamage(int val = 1) {
        Hearts -= val;

        if(val == 1 && Hearts > 0) {
            OnPlayerHit?.Invoke(false);
        } else if(val > 1 && Hearts > 0) {
            OnPlayerHit?.Invoke(true);
        }

        OnDamageTaken?.Invoke();

        if(Hearts <= 0) {
            OnPlayerKill?.Invoke();
        }
    }

}
