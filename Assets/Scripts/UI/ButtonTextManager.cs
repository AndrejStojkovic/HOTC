using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTextManager : MonoBehaviour
{
    public float ButtonIdleTextPosition;
    public float ButtonPressedTextPosition;

    public static float IdleTextPosition { get; private set; }
    public static float PressedTextPosition { get; private set; }

    private void Awake() {
        IdleTextPosition = ButtonIdleTextPosition;
        PressedTextPosition = ButtonPressedTextPosition;
        DontDestroyOnLoad(this.gameObject);
    }
}
