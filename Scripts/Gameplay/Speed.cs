//Grabs the MPH of the kart and displays it

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Speed : MonoBehaviour {

    Text text;
    // Start is called before the first frame update
    void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        
        float speed = Mathf.Round(GameObject.Find("Player").GetComponent<PlayerMain>().MPH);
        String speedString;
        if (speed < 10) {
            speedString = "0" + speed + " MPH";
        }

        else {
            speedString = speed + " MPH";
        }

        text.text = speedString;
    }
}
