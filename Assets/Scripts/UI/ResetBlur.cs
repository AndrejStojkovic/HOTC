using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetBlur : MonoBehaviour
{
    public Image BlurredImage;
    private void OnApplicationQuit() {
        BlurredImage.material.SetFloat("_Focus", 0f);
    }
}
