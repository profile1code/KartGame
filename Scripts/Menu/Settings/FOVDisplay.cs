using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FOVDisplay : MonoBehaviour {
    
    Text text;

    void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        text.text = GameObject.Find("FOV").GetComponent<Slider>().value + " FOV";      
    }
}
