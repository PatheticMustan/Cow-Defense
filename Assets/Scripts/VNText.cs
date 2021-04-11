using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VNText : MonoBehaviour {
    public TextMeshProUGUI textDisplay;
    public AudioSource charSound;
    
    // sucks! the interval at which a new character is added
    public float characterInterval = 0;
    public float timer;
    public string currentMessage;
    public bool isCompleted;
    public int characterIndex;

    void Start() {
        isCompleted = true;
    }

    void Update() {
        if (!isCompleted) {
            timer += Time.deltaTime;

            if (timer > characterInterval) {
                timer = 0;
                if (!(currentMessage.Length - 1 < characterIndex)) textDisplay.text += currentMessage[characterIndex++];

                if (characterIndex == currentMessage.Length) isCompleted = true;
            }
            
        }
    }

    public void DisplayMessage(string message) {
        characterIndex = 0;
        timer = 0;
        textDisplay.text = "";
        currentMessage = message;
        isCompleted = false;
    }

    public void SkipAhead() {
        characterIndex = 0;
        timer = 0;
        textDisplay.text = currentMessage;
        isCompleted = true;
    }
}