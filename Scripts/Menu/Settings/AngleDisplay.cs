using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleDisplay : MonoBehaviour {
    
    Text text;

    void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        text.text = GameObject.Find("Angle").GetComponent<Slider>().value + " Degrees";      
    }
}
