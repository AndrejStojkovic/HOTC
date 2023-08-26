using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapTypes {
    None,
    Projectiles,
    Environment,
    Magic,
    Binding
}

[CreateAssetMenu(fileName = "New Trap", menuName = "HOTC/Trap")]
public class Trap : ScriptableObject
{
    [ScriptableObjectId]
    public string Id;
    public string Name;
    public GameObject Prefab;
    public Sprite Icon;
    public TrapTypes Type;

    [Header("Stats")]

    [Min(0f)]
    public int Amount;
    [Min(0f)]
    public int Damage;
    [Min(0f)]
    public float UseCooldown;
    [Min(0f)]
    public float ReloadCooldown;
    public bool Delayed;

    [Header("Upgrades")]
    public bool IsUpgradeable;
    [Tooltip("It's only used if the trap is upgradeable.")]
    public Trap Upgrade;

    private void OnValidate() {
        if(string.IsNullOrWhiteSpace(Id)) {
            AssignNewID();
        }
    }

    public void AssignNewID() {
        Id = Guid.NewGuid().ToString();
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
