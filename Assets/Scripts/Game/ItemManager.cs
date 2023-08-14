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
                    break;
                }
            }
        }
    }
}
