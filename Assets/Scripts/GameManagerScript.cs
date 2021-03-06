﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // not sure what we'll use this for
    // public GameObject characterContainer;

    // character shortcuts, assigned in inspector
    public Character Barry, Buzz;

    // there will be no saving, 
    private VNA[] vn;
    private int actionIndex;

    private 

    void Start() {
        vn = new VNA[] {
            text(Barry, "hello"),
            asset(Barry, 1),
            choice(Buzz, "what color hair do I want", new string[] { "yellow", "black" })
        };

        actionIndex = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // parse action
            VNA currentAction = vn[actionIndex];
            switch (currentAction.type) {
                case "text":
                    break;
                case "choice":
                    break;
                case "asset":
                    break;
                default:
                    break;
            }

            // increment current action
            actionIndex++;
        }
    }


    // I am a bad person
    // make typing shorter heheheheheheheh
    // [choice, text, asset] VNA abstractions
    private VNA choice(Character character, string text, string[] choices) {
        return new VNA(character, text, choices);
    }
    private VNA text(Character character, string text) {
        return new VNA(character, text);
    }
    private VNA asset(Character character, int index) {
        return new VNA(character, index);
    }


    // VN Action
    struct VNA {
        public string type; // choice, text, asset
        
        public string name;
        public Color color;
        public string text;
        public string[] choices;

        public int assetIndex;

        // choice
        public VNA(Character character, string text, string[] choices) {
            this.type = "choice";

            this.name = character.name; 
            this.color = character.nameColor;
            this.text = text;
            this.choices = choices;
            this.assetIndex = -1;
        }

        // text
        public VNA(Character character, string text) {
            this.type = "text";

            this.name = character.name;
            this.color = character.nameColor;
            this.text = text;
            this.choices = null;
            this.assetIndex = -1;
        }

        // asset
        public VNA(Character character, int index) {
            this.type = "asset";

            this.name = character.name;
            this.color = character.nameColor;
            this.text = null;
            this.choices = null;
            this.assetIndex = index;
        }
    }
}