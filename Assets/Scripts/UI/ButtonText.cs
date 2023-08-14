using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Button))]
public class ButtonText : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform Text;
    
    void Start() {
        Text = GetComponentInChildren<TextMeshProUGUI>().rectTransform;
        SetPos(ButtonTextManager.IdleTextPosition);
    }

    public void OnPointerUp(PointerEventData pointerEventData) {
        SetPos(ButtonTextManager.IdleTextPosition);
    }

    public void OnPointerDown(PointerEventData pointerEventData) {
        SetPos(ButtonTextManager.PressedTextPosition);
    }

    protected void SetPos(float pos) {
        Text.offsetMax = new Vector2(Text.offsetMax.x, pos);
    }
}
