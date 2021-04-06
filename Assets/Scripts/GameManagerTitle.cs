using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// secret sauce things
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerTitle : MonoBehaviour {
    public TextMeshProUGUI title;

    // title wiggle!
    private float wiggle;
    public int wiggleDegrees = 10;
    public int wiggleSpeed = 1;

    // this will have little bees flying around the screen
    public Sprite[] assets;
    public int beeCount = 5;

    void Start() {
        wiggle = 0;
    }

    // Update is called once per frame
    void Update() {
        wiggle += wiggleSpeed * Time.deltaTime;
        title.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Mathf.Sin(wiggle) * wiggleDegrees);
    }

    public void Play() {
        SceneManager.LoadScene("Story");
    }
}
