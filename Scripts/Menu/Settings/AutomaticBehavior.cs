using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomaticBehavior : MonoBehaviour {
    
    Toggle toggle;

    void Start() {
        toggle = GetComponent<Toggle>();
    }

    
    void Update() {
        bool show = GameObject.Find("MenuCanvas").GetComponent<FileManager>().currentKart().Equals("Shifter");
        if (show) {
            toggle.interactable = true;
        }
        else {
            toggle.interactable = false;
        }

    }
}
