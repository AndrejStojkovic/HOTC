using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewManager : MonoBehaviour
{
    public List<UIView> Views;
    private int idx;

    void Start() {
        idx = 0;
        UpdateViews();
    }

    void UpdateViews() {
        if(idx != -1) {
            Views[idx].ShowView();
        }

        for(int i = 0; i < Views.Count; i++) {
            if(idx == i) {
                continue;
            }
            Views[i].HideView();
        }
    }

    public void SetCurrentView(int targetIdx) {
        idx = targetIdx;
        UpdateViews();
    }

    public void SetCurrentView(string targetName) {
        idx = Views.FindIndex(x => x.Name == targetName);
        UpdateViews();
    }
}
