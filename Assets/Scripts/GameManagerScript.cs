using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    // not sure what we'll use this for
    // public GameObject characterContainer;

    // character shortcuts, assigned in inspector
    public Character Barry, Buzz;

    // there will be no saving, 
    private VNA[] vn;
    public int actionIndex;

    // sorry for not putting this in VNText
    // love, Kevin ;))
    public TextMeshProUGUI nameText;

    // set it to true to skip the next action
    // the code is filled with messy workarounds, band-aids on bulletwounds, and barely held together bits and bobs. Sorry for any bugs.
    public bool skipAction;

    

    void Start() {
        vn = new VNA[] {
            text(Barry, "helloooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo"),
            text(Buzz, "greetings, fellow bee!"),
            text(Barry, "why are you so verbose"),
            text(Buzz, "i feel that talking in big words!"),
            text(Barry, "hello"),
            asset(Barry, 1),
            choice(Buzz, "what color hair do I want", new string[] { "yellow", "black" }),
            text(Barry, "I have transformed into Akko"),
            text(Buzz, "I hate Akko, I'm leaving"),
            asset(Buzz, -1),
            text(Barry, ":(")
        };

        actionIndex = 0;

        // skip first action :P
        skipAction = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || skipAction) {
            skipAction = false;

            if (!GetComponent<VNText>().isCompleted) {
                GetComponent<VNText>().SkipAhead();
            } else {
                // increment current action
                // if 1 vn, actionIndex will equal 0, 
                // edgecase for 0 vn but lmao I don't care, not gonna fix it
                if (actionIndex != vn.Length) {
                    // parse action
                    VNA currentAction = vn[actionIndex];
                    switch (currentAction.type) {
                        case "text":
                            nameText.text = currentAction.name;
                            nameText.GetComponent<TextMeshProUGUI>().color = currentAction.color;
                            GetComponent<VNText>().DisplayMessage(currentAction.text);
                            break;



                        case "choice":

                            break;




                        case "asset":
                            Character ch = currentAction.character;
                            if (ch.GetComponent<Character>().assets.Length - 1 < currentAction.assetIndex) {
                                Debug.LogError("Missing asset, " + currentAction.name + " is missing asset #" + currentAction.assetIndex);
                                break;
                            }

                            skipAction = true;

                            if (currentAction.assetIndex == -1) {
                                // if it's -1, make the character invisible
                                currentAction.character.GetComponent<CanvasGroup>().alpha = 0;
                                break;
                            } else {
                                currentAction.character.GetComponent<CanvasGroup>().alpha = 1;
                            }
                            currentAction.character.GetComponent<Image>().sprite = ch.assets[currentAction.assetIndex];
                            break;



                        default:
                            break;
                    }

                    actionIndex++;
                } else {
                    Debug.Log("end!");
                    Debug.Log("There's an interesting band called 'Death Grips', their music is weird, like a mix of random words screamed, and percussion. Check them out!");
                }
            }
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

        public Character character;

        // choice
        public VNA(Character character, string text, string[] choices) {
            this.type = "choice";

            this.name = character.name; 
            this.color = character.nameColor;
            this.text = text;
            this.choices = choices;
            this.assetIndex = -1;
            this.character = character;
        }

        // text
        public VNA(Character character, string text) {
            this.type = "text";

            this.name = character.name;
            this.color = character.nameColor;
            this.text = text;
            this.choices = null;
            this.assetIndex = -1;
            this.character = character;
        }

        // asset
        public VNA(Character character, int index) {
            this.type = "asset";

            this.name = character.name;
            this.color = character.nameColor;
            this.text = null;
            this.choices = null;
            this.assetIndex = index;
            this.character = character;
        }
    }
}