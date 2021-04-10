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
    // if you're in the middle of a choice >:((
    public bool unskippable;

    public bool clicked;

    public GameObject choiceContainer,
        choiceOne,
        choiceTwo;

    void Start() {
        vn = new VNA[] {
            text(Barry, "helloooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo"),
            text(Buzz, "greetings, fellow bee!"),
            text(Barry, "why are you so verbose"),
            text(Buzz, "i feel that talking in big words!"),
            text(Barry, "hello"),
            asset(Barry, 1),
            text(Barry, "I have transformed into Akko"),
            choice(Buzz, "what color hair do I want", new (string, int)[] { ("yellow", 8), ("black", 10) }),

            text(Buzz, "Wow, yellow hair! Snazzy!"),
            jump(11),

            text(Buzz, "ew, black hair. I'm leaving."),
            asset(Buzz, -1),

            text(Barry, "END")
        };

        actionIndex = 0;

        // skip first action :P
        skipAction = true;
        unskippable = false;

        choiceContainer.GetComponent<CanvasGroup>().alpha = 0;

        // I am so sorry to anyone reading my code from here on out
        choiceOne = choiceContainer.GetComponent<Transform>().GetChild(0).GetChild(0).gameObject;
        choiceTwo = choiceContainer.GetComponent<Transform>().GetChild(1).GetChild(0).gameObject;
    }

    void Update() {
        if (!unskippable && (Input.GetKeyDown(KeyCode.Space) || skipAction)) {
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
                            // display the text, all good and well
                            nameText.text = currentAction.name;
                            nameText.GetComponent<TextMeshProUGUI>().color = currentAction.color;
                            GetComponent<VNText>().DisplayMessage(currentAction.text);

                            // make it unskippable, show the choice container
                            unskippable = true;
                            choiceContainer.GetComponent<CanvasGroup>().alpha = 1;
                            choiceContainer.GetComponent<CanvasGroup>().blocksRaycasts = true;
                            choiceOne.GetComponent<Text>().text = currentAction.choices[0].choiceText;
                            choiceTwo.GetComponent<Text>().text = currentAction.choices[1].choiceText;

                            clicked = false;
                            break;




                        case "asset":
                            if (currentAction.character.GetComponent<Character>().assets.Length - 1 < currentAction.assetIndex) {
                                Debug.LogError("Missing asset, " + currentAction.name + " is missing asset #" + currentAction.assetIndex);
                                break;
                            }

                            if (currentAction.assetIndex == -1) {
                                // if it's -1, make the character invisible
                                currentAction.character.GetComponent<CanvasGroup>().alpha = 0;
                            } else {
                                currentAction.character.GetComponent<CanvasGroup>().alpha = 1;
                                currentAction.character.GetComponent<Image>().sprite = currentAction.character.assets[currentAction.assetIndex];
                            }

                            skipAction = true;
                            break;


                        case "jump":
                            actionIndex = currentAction.actionIndex;
                            skipAction = true;
                            break;



                        default:
                            break;
                    }

                    actionIndex++;
                } else {
                    Debug.Log("end!");
                }
            }
        }
    }


    public void choice(int choice) {
        if (!clicked) {
            clicked = true;

            VNA currentAction = vn[actionIndex - 1];

            choiceContainer.GetComponent<CanvasGroup>().alpha = 0;
            choiceContainer.GetComponent<CanvasGroup>().blocksRaycasts = true;

            Debug.Log(currentAction.choices);
            actionIndex = currentAction.choices[choice].actionIndex;

            unskippable = false;
            skipAction = true;
        }
        
    }





    // I am a bad person
    // make typing shorter heheheheheheheh
    // [choice, text, asset] VNA abstractions
    private VNA choice(Character character, string text, (string choiceText, int actionIndex)[] choices) {
        return new VNA(character, text, choices);
    }
    private VNA text(Character character, string text) {
        return new VNA(character, text);
    }
    private VNA asset(Character character, int index) {
        return new VNA(character, index);
    }
    private VNA jump(int index) {
        return new VNA(index);
    }


    // VN Action
    struct VNA {
        public string type; // choice, text, asset
        
        public string name;
        public Color color;
        public string text;
        public (string choiceText, int actionIndex)[] choices;

        public int assetIndex;
        public int actionIndex;

        public Character character;

        // choice
        public VNA(Character character, string text, (string choiceText, int actionIndex)[] choices) {
            this.type = "choice";

            this.name = character.name; 
            this.color = character.nameColor;
            this.text = text;
            this.choices = choices;
            this.assetIndex = -1;
            this.actionIndex = -1;
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
            this.actionIndex = -1;
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
            this.actionIndex = -1;
            this.character = character;
        }

        // jump to action index
        public VNA(int index) {
            this.type = "jump";

            this.name = null;
            this.color = Color.black;
            this.text = null;
            this.choices = null;
            this.assetIndex = 0;
            this.actionIndex = index;
            this.character = null;

            
        }
    }
}