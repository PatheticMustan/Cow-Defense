using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character {
    public string name;
    public Color nameColor;
    public Sprite[] assets;
    private VNText vnt;
    private GameObject instance;

    public Character(string name, Color nameColor, Sprite[] assets) {
        this.name = name;
        this.nameColor = nameColor;
        this.assets = assets;
        this.vnt = GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<VNText>();
    }

    // display text (text, speed)
    public void DisplayText(string text) {
        vnt.DisplayMessage(text);
    }

    // switchToAsset(assetIndex)
    // move position (x, y)
    // switch on/off
}