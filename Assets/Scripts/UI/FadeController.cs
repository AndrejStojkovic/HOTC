using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    private static FadeController _instance;
    public static FadeController Instance {
        get {
            if(_instance == null) {
                Debug.Log("Fade Controller Instance is Null!");
            }
            return _instance;
        }
    }
    
    public UIManager UIManager;
    
    public Image Fade;
    public float Amount;
    public float Duration;

    public UnityEvent FadeInEnded = new UnityEvent();
    public UnityEvent FadeOutEnded = new UnityEvent();
    
    private bool pendingFadeIn;
    private bool pendingFadeOut;
    private float startTime;
    
    void Awake() {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start() {
        UIManager = UIManager.Instance;
        // pendingFadeIn = pendingFadeOut = false;
        startTime = 0f;
    }

    void Update() {
        if(pendingFadeIn && !pendingFadeOut) {
            setFade((UIManager.MenuTime - startTime) / Duration);

            if(Amount >= 1) {
                Amount = 1;
                pendingFadeIn = false;
                FadeInEnded?.Invoke();
            }
        }

        if(!pendingFadeIn && pendingFadeOut) {
            setFade(1 - ((UIManager.MenuTime - startTime) / Duration));

            if(Amount <= 0) {
                Amount = 0;
                pendingFadeOut = false;
                FadeOutEnded?.Invoke();
            }
        }
    }

    public void FadeIn(bool force = false) {
        Fade.gameObject.SetActive(true);
        startTime = UIManager.MenuTime;
        pendingFadeIn = true;
    }

    public void FadeOut(bool force = false) {
        Fade.gameObject.SetActive(true);
        startTime = UIManager.MenuTime;
        pendingFadeOut = true;
        Debug.Log("Fade Out");
    }
    
    public void ForceFade(float val) {
        Fade.gameObject.SetActive(true);
        setFade(val);
    }

    void setFade(float value) {
        Amount = value;
        Fade.color = new Color(0f, 0f, 0f, Amount);
    }
}
