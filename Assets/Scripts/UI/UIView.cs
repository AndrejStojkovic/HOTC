using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    public string Name;
    public GameObject Parent;
    public GameObject View;

    public List<UIView> ShowViews;
    public List<UIView> HideViews;

    void Start() {
        if(Parent == null) {
            Parent = gameObject;
        }
    }

    void Update() {
    }

    private void OnValidate() {
        if(Parent == null) {
            Parent = gameObject;
        }

        string targetName = "View - " + Name;
        if(gameObject.name != targetName) {
            gameObject.name = targetName;
        }
    }

    public void ShowView() {
        View.SetActive(true);
        SetViews(true);
    }

    public void HideView() {
        View.SetActive(false);
        SetViews(false);
    }

    protected void SetViews(bool state) {
        for(int i = 0; i < ShowViews.Count; i++) {
            if(state) {
                ShowViews[i].ShowView();
            } else {
                ShowViews[i].HideView();
            }
        }

        for(int i = 0; i < ShowViews.Count; i++) {
            if(state) {
                HideViews[i].HideView();
            } else {
                HideViews[i].ShowView();
            }
        }
    }
}
