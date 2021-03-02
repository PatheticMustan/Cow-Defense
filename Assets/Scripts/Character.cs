using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : MonoBehaviour {
    public string name;
    public Color nameColor;
    public Sprite[] assets;
    // private VNText vnt;

    public Character(string name, Color nameColor, Sprite[] assets) {
        this.name = name;
        this.nameColor = nameColor;
        this.assets = assets;
        // this.vnt = GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<VNText>();
    }
}