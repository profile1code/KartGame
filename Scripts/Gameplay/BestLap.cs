//This class changes the "BestLap" Display appropriately to show the correct info


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BestLap : MonoBehaviour {


    Text text;
    
    // Start is called before the first frame update
    void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        
        float time = GameObject.Find("Start/Finish").GetComponent<Timing>().bestLap;
        int minutes = (int)(time - (time % 60)) / 60;
        float seconds = Mathf.Round((time % 60) * 1000) / 1000;

        text.text = toStopwatch(minutes, seconds);

    }

    public String toStopwatch(int min, float sec) {
        
        if (min == 0 && sec == 0) {
            return "00:00:000";
        }
        
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
