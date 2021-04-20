using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarScript : MonoBehaviour {
    public float barSize;
    public RectTransform lifeFill;

    void Start() {
        barSize = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        updateLifeFill(0);
    }

    void Update() {}

    public void updateLifeFill(float fill) {
        lifeFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fill);
    }
}