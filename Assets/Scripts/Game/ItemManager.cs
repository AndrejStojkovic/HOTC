using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemList {
    public KeyCode Key;
    public Item Item;
}

public class ItemManager : MonoBehaviour
{
    private GameManager gm;
    public ItemList[] Items;

    public UnityEvent<Item> ItemSelected = new UnityEvent<Item>();

    void Start() {
        gm = GameManager.Instance;

        foreach(ItemList item in Items) {
            item.Item.SetKey(item.Key.ToString());
        }
    }

    void Update() {
        if(gm.GameState == GameState.NotStarted || gm.GameState == GameState.GameOver) {
            return;
        }

        if(Input.anyKeyDown) {
            foreach(ItemList item in Items) {
                if(Input.GetKeyDown(item.Key) && item.Item != null) {
                    ItemSelected?.Invoke(item.Item);
                }
            }
        }

        // if(Input.GetKeyDown(KeyCode.Q) && Items[0] != null) {
        //     ItemSelected?.Invoke(Items[0]);
        // } else if(Input.GetKeyDown(KeyCode.W) && Items[1] != null) {
        //     ItemSelected?.Invoke(Items[1]);
        // } else if(Input.GetKeyDown(KeyCode.E) && Items[2] != null) {
        //     ItemSelected?.Invoke(Items[2]);
        // } else if(Input.GetKeyDown(KeyCode.R) && Items[3] != null) {
        //     ItemSelected?.Invoke(Items[3]);
        // } else if(Input.GetKeyDown(KeyCode.T) && Items[4] != null) {
        //     ItemSelected?.Invoke(Items[4]);
        // }
    }
}
