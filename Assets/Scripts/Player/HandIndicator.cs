using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandIndicator : MonoBehaviour
{
    public HandController HandController;
    public Image Indicator;

    public Sprite DefaultLaneIndicator;
    public List<Sprite> LaneIndicators;

    void Start() {
        HandController.OnLaneChange?.AddListener(OnLaneChanged);
        Indicator.sprite = LaneIndicators[HandController.CurrentLane];
    }

    public void OnLaneChanged(int currentLane) {
        if(currentLane < 0 || currentLane >= LaneIndicators.Count) {
            Indicator.sprite = DefaultLaneIndicator;
            return;
        }

        Indicator.sprite = LaneIndicators[currentLane];
    }
}
