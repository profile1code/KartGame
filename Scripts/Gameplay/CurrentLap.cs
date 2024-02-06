//This class keeps track of the timing of the current lap, as well as
//whether or not the player is on the track and the lap should be valid

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CurrentLap : MonoBehaviour {


    Text text;
    // Start is called before the first frame update
    void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        //Rounds the time to the thousandth
        float time = GameObject.Find("Start/Finish").GetComponent<Timing>().currentLap;
        int minutes = (int)(time - (time % 60)) / 60;
        float seconds = Mathf.Round((time % 60) * 1000) / 1000;

        text.text = toStopwatch(minutes, seconds);

    }

    public string toStopwatch(int min, float sec) {
        if (sec < 10) {
            if (min < 10) {
            return "0" + min + ":0" + sec;
            }
            return min + ":0" + sec;
        }
        if (min < 10) {
        return "0" + min + ":" + sec;
        }
        return min + ":" + sec;
    }
}
