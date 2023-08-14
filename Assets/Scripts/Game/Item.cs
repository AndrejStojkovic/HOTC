using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    private GameManager gm;
    public GameObject Prefab;
    public Sprite Icon;
    public bool Disabled = false;
    [Header("Reload Cooldown")]
    public float ReloadCooldownDuration = 1;
    private bool ReloadCooldown = false;
    [Header("Item Spawn Cooldown")]
    public bool ItemSpawnCooldown;
    public float ItemSpawnCooldownDuration = 0f;
    public float LocalStartTime {
        get { return localStartTime; }
        set { localStartTime = value; }
    }
    private float localStartTime = 0f;
    [Header("Amount Stats")]
    [Min(0)]
    public int Amount;
    [Min(1)]
    public int DamageAmount = 1;
    public int initialAmount;
    private float startTime;

    [Header("References")]
    public Image IconRef;
    public Image WaitRef;
    public TextMeshProUGUI AmountRef; 

    void Start() {
        gm = GameManager.Instance;

        if(Icon && IconRef) {
            IconRef.sprite = Icon;
        }

        if(AmountRef) {
            AmountRef.text = Amount.ToString();
        }

        initialAmount = initialAmount > 0 ? initialAmount : Amount;
        startTime = gm.GameTime; 
        UpdateItemUI();
    }

    void Update() {
        UpdateItemUI();
    }

    public void UpdateItemUI() {
        if(Disabled) {
            // AmountRef.gameObject.SetActive(false);
            AmountRef.text = " ";
            return;
        }

        AmountRef.text = Amount.ToString();

        if(Amount <= 0 && !ReloadCooldown) {
            WaitRef.gameObject.SetActive(true);
            startTime = gm.GameTime;
            ReloadCooldown = true;
        }

        if(ReloadCooldown) {
            float amount = 1 - ((gm.GameTime - startTime) / ReloadCooldownDuration);

            if(amount <= 0) {
                Amount = initialAmount;
                ReloadCooldown = false;
                WaitRef.gameObject.SetActive(false);
                WaitRef.fillAmount = 1;
            } else {
                WaitRef.fillAmount = amount;
            }
        }
    }
}
