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

[CreateAssetMenu()]
public class Trap : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
    public Sprite Icon;
    public TrapTypes Type;

    [Header("Stats")]

    [Min(0f)]
    public int Amount;
    [Min(0f)]
    public float Damage;
    [Min(0f)]
    public float UseCooldown;
    [Min(0f)]
    public float ReloadCooldown;

    [Header("Upgrades")]
    public bool IsUpgradeable;
    [Tooltip("It's only used if the trap is upgradeable.")]
    public Trap Upgrade;
}
