using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthIndicator : MonoBehaviour
{
    public Health PlayerHealth;
    public GameObject HealthParent;
    public GameObject HeartPrefab;
    
    public Sprite ActiveSprite;
    public Sprite InactiveSprite;

    public List<GameObject> HeartObjects { get; }
    private List<GameObject> heartObjects;
    
    void Start() {
        heartObjects = new List<GameObject>();
        
        if(HealthParent) {
            HealthParent.GetComponent<RectTransform>().sizeDelta = new Vector2(PlayerHealth.Hearts / 2.0f, 0.5f);
            for(int i = 0; i < PlayerHealth.Hearts; i++) {
                GameObject heart = Instantiate(HeartPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                heart.transform.SetParent(HealthParent.transform, false);
                heartObjects.Add(heart.transform.GetChild(0).gameObject);
            }
        }

        UpdateHeartsUI();

        PlayerHealth.OnDamageTaken?.AddListener(UpdateHeartsUI);
    }

    void Update() {
        
    }

    public void UpdateHeartsUI() {
        for(int i = 0; i < heartObjects.Count; i++) {
            heartObjects[i].GetComponent<Image>().sprite = (i < PlayerHealth.Hearts ? ActiveSprite : InactiveSprite);
        }
    }
}
