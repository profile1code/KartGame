//This class controls the timing system, which involves best and current lap
//It will be affected by whether the lap is valid or the first lap

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timing : MonoBehaviour {

    [SerializeField] public bool thisLapValid;
    [SerializeField] public float currentLap;
    [SerializeField] public float lastLap;
    [SerializeField] public float bestLap;
    public bool firstLap;
    public bool inStartRadar;

    // Start is called before the first frame update
    void Start() {
        thisLapValid = true;
        bestLap = 0.0f;
        currentLap = 0.0f;
        lastLap = 0.0f;
        firstLap = true;
        
    }

    // Update is called once per frame
    void Update() {
        //Logic on whether and when the lap should save
        inStartRadar = GameObject.Find("Start/Finish").GetComponent<Checkpoints>().inRadar;
        currentLap += Time.deltaTime;
        if (lapFinished() && inStartRadar) {
            GameObject.Find("Split").GetComponent<SplitReport>().showTime(currentLap, bestLap);
            if (thisLapValid && currentLap < bestLap || bestLap < 1f && thisLapValid) {
                GameObject.Find("Checkpoint1").GetComponent<Checkpoints>().newBestLap();
                GameObject.Find("Checkpoint2").GetComponent<Checkpoints>().newBestLap();
                GameObject.Find("Checkpoint3").GetComponent<Checkpoints>().newBestLap();
                GameObject.Find("Start/Finish").GetComponent<Checkpoints>().newBestLap();
                bestLap = currentLap;
                GameObject.Find("Ghost").GetComponent<Ghost>().newBest();
            } 
            currentLap = 0.0f;
            thisLapValid = true;
            resetAll();
            GameObject.Find("Ghost").GetComponent<Ghost>().resetLap();
            
            
        }

        if (firstLap && GameObject.Find("Start/Finish").GetComponent<Checkpoints>().beenPassed) {
            currentLap = 0.0f;
            thisLapValid = true;
            firstLap = false;
            GameObject.Find("Ghost").GetComponent<Ghost>().resetLap();
            resetAll();
        }

        bool frontOn = GameObject.Find("Front").GetComponent<TireBehavior>().isOnTrack;
        bool backOn = GameObject.Find("Back").GetComponent<TireBehavior>().isOnTrack;
        bool leftOn = GameObject.Find("Left").GetComponent<TireBehavior>().isOnTrack;
        bool rightOn = GameObject.Find("Right").GetComponent<TireBehavior>().isOnTrack;

        if (!(frontOn && backOn && leftOn && rightOn)) {
            thisLapValid = false;
        }
    }

    public bool lapFinished() {
        bool c1 = GameObject.Find("Checkpoint1").GetComponent<Checkpoints>().beenPassed;
        bool c2 = GameObject.Find("Checkpoint2").GetComponent<Checkpoints>().beenPassed;
        bool c3 = GameObject.Find("Checkpoint3").GetComponent<Checkpoints>().beenPassed;
        bool startfinish = GameObject.Find("Start/Finish").GetComponent<Checkpoints>().beenPassed;

        return c1 && c2 && c3 && startfinish;
    }

    public void resetAll() {
        GameObject.Find("Checkpoint1").GetComponent<Checkpoints>().beenPassed = false;
        GameObject.Find("Checkpoint2").GetComponent<Checkpoints>().beenPassed = false;
        GameObject.Find("Checkpoint3").GetComponent<Checkpoints>().beenPassed = false;
        GameObject.Find("Start/Finish").GetComponent<Checkpoints>().beenPassed = false;
        
    }
}
