using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance {
        get {
            if(_instance == null) {
                Debug.Log("Menu Manager Instance is Null!");
            }
            return _instance;
        }
    }

    public FadeController FadeController;
    public float MenuTime;

    [Header("References")]
    public Image Background;
    public GameObject MenuParent;
    private Image[] MenuImageChildren;
    private TextMeshPro[] MenuTextChildren;
    private TextMeshProUGUI[] MenuTextGUIChildren;

    [Header("Intro Settings")]
    [Min(0)]
    public float MenuDuration;

    void Awake() {
        _instance = this;
    }
    
    void Start() {
        if(MenuParent != null) {
            MenuImageChildren = MenuParent.GetComponentsInChildren<Image>(true);
            MenuTextChildren = MenuParent.GetComponentsInChildren<TextMeshPro>(true);
            MenuTextGUIChildren = MenuParent.GetComponentsInChildren<TextMeshProUGUI>(true);
        }
        Background.material.SetFloat("_Focus", 0f);
        SetMenu(0f);
        FadeController.ForceFade(1f);
        FadeController.FadeOut();
        StartCoroutine("ShowMenu");
    }

    void Update() {
        MenuTime += Time.deltaTime;
    }

    private IEnumerator ShowMenu() {
        float elapsedTime = 0f;

        while(elapsedTime < MenuDuration) {
            SetMenu(elapsedTime / MenuDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void SetMenu(float val) {
        for(int i = 0; i < MenuImageChildren.Length; i++) {
            Image a = MenuImageChildren[i];
            a.color = new Color(a.color.r, a.color.g, a.color.b, val);
        }


        for(int i = 0; i < MenuTextChildren.Length; i++) {
            TextMeshPro ch = MenuTextChildren[i];
            ch.color = new Color(ch.color.r, ch.color.g, ch.color.b, val);
        }

        for(int i = 0; i < MenuTextGUIChildren.Length; i++) {
            TextMeshProUGUI ch = MenuTextGUIChildren[i];
            ch.color = new Color(ch.color.r, ch.color.g, ch.color.b, val);
        }
    }

}
