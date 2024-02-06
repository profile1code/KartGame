//This class controls the behavior of the checkpoints on the track,
//which record whether the player is passing them

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Checkpoints : MonoBehaviour {

    [SerializeField] public bool beenPassed;
    [SerializeField] public bool inRadar;
    [SerializeField] float xDifference;
    [SerializeField] float yDifference;
    [SerializeField] float zDifference;

    [SerializeField] float currentSplit;
    [SerializeField] float bestSplit;

    //Minimum distance to the player
    float errorMargin = 10f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        //Checks every frame if the player is within range
        Vector3 playerPosition = GameObject.Find("Player").GetComponent<PlayerMain>().bodyPosition;
        Vector3 myPosition = transform.position;
        xDifference = Mathf.Abs(playerPosition.x - myPosition.x);
        yDifference = Mathf.Abs(playerPosition.y - myPosition.y);
        zDifference = Mathf.Abs(playerPosition.z - myPosition.z);
        
        if (xDifference < errorMargin && yDifference < errorMargin && zDifference < errorMargin) {
            
            inRadar = true;
            
            if (beenPassed == false) {
                currentSplit = GameObject.Find("Start/Finish").GetComponent<Timing>().currentLap;
                
                if (!GameObject.Find("Start/Finish").GetComponent<Timing>().inStartRadar) {
                    GameObject.Find("Split").GetComponent<SplitReport>().showTime(currentSplit, bestSplit);
                }
                
            }
            
            beenPassed = true;
        }
        else {
            inRadar = false;
        }
        
        
    }

    public void newBestLap() {
        bestSplit = currentSplit;
    }

    
}
