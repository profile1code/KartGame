//controls the image which lets the user know if the lap is invalid
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invalid : MonoBehaviour {
    // Start is called before the first frame update
    Image image;
    void Start() {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        if (GameObject.Find("Start/Finish").GetComponent<Timing>().thisLapValid == false) {
            
            image.enabled = true;
            
        }
        else {
            image.enabled = false;
    
        }
    }
}
