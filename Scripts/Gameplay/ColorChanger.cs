using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {
    string kartType;
    public Material mat;
    void Start() {
        kartType = GameObject.Find("MenuCanvas").GetComponent<FileManager>().currentKart();
        if (kartType.Equals("Rental")) {
          mat.color = Color.blue;
        }
        if (kartType.Equals("Sprint")) {
           mat.color = Color.red;
        }
        if (kartType.Equals("Shifter")) {
            mat.color = Color.white;
        }
    }

    
    void Update() {
       
    }
}
