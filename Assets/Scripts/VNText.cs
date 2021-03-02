using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VNText : MonoBehaviour {
    public TextMeshProUGUI textDisplay;
    public AudioSource charSound;
    public bool messageWriteDone;

    private void Start() {
        // DisplayMessage("the quick brown fox jumped over deez nuts lorum ipsum pee pee poo poo ahhhhh eric leo katie kevin eeeee hhhhhhhhthe quick brown fox jumped over deez nuts lorum ipsum pee pee poo poo ahhhhh eric leo katie kevin eeeee hhhhhhhhthe quick brown fox jumped over deez nuts lorum ipsum pee pee poo poo ahhhhh eric leo katie kevin eeeee hhhhhhhh");
    }

    public void DisplayMessage(string message) {
        StartCoroutine("WriteMessage", message);
        messageWriteDone = true;
    }

    IEnumerator WriteMessage(string message) {
        if (!messageWriteDone) {
            yield return new WaitUntil(() => messageWriteDone);
            yield return new WaitForSecondsRealtime(0.25f);
        }

        messageWriteDone = false;
        textDisplay.text = "";

        foreach (char i in message.ToCharArray()) {
            textDisplay.text += i;
            //charSound.Play();
            yield return 0;
        }

        messageWriteDone = true;
    }
}