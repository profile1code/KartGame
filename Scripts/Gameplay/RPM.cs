//Grabs the RPM of the kart and displays it

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RPM : MonoBehaviour {

    Text text;
    // Start is called before the first frame update
    void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        
        float RPM = Mathf.Round(GameObject.Find("Player").GetComponent<PlayerMain>().RPM);
        String RPMString;
        if (RPM < 10000f) {
            
            if (RPM == 0f) {
                RPMString = "00000 RPM";
            }
            else {
                RPMString = " " + RPM + " RPM";
            }
            
        }

        else {
            RPMString = RPM + " RPM";
        }

        text.text = RPMString;
    }
}
