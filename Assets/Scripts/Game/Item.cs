using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    private GameManager gm;
    public Trap Trap;
    public float CurrentAmount;
    public bool Disabled = false;
    public float LocalStartTime { get; set; }
    private float initialAmount;
    private bool reloadCooldown;

    [Header("References")]
    public Image IconRef;
    public Image WaitRef;
    public TextMeshProUGUI AmountRef; 
    public TextMeshProUGUI KeyRef;

    void Start() {
        gm = GameManager.Instance;

        if(Trap.Icon && IconRef) {
            IconRef.sprite = Trap.Icon;
        }

        if(AmountRef) {
            AmountRef.text = Trap.Amount.ToString();
        }

        CurrentAmount = Trap.Delayed ? 0f : Trap.Amount;
        initialAmount = Trap.Amount;
        LocalStartTime = gm.GameTime; 
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

        AmountRef.text = CurrentAmount.ToString();

        if(!reloadCooldown && CurrentAmount <= 0 && Trap.ReloadCooldown > 0f) {
            WaitRef.gameObject.SetActive(true);
            LocalStartTime = gm.GameTime;
            reloadCooldown = true;
        }

        if(reloadCooldown) {
            float amount = 1 - ((gm.GameTime - LocalStartTime) / Trap.ReloadCooldown);

            if(amount <= 0) {
                CurrentAmount = initialAmount;
                reloadCooldown = false;
                WaitRef.gameObject.SetActive(false);
                WaitRef.fillAmount = 1;
            } else {
                WaitRef.fillAmount = amount;
            }
        }
    }

    public void SetKey(string key) {
        KeyRef.text = key;
    }
}
