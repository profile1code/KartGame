using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameManagement : MonoBehaviour {
    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
        string name = GameObject.Find("Username").GetComponent<InputField>().text;
        if (name.Length > 16) {
            name = name.Substring(0, 16);  
        }
        string test = "";
        while (test.Length <= 16) {
            if (name.Equals(test)) {
                name = "-";
            }
            test += " ";
        }

        
        GameObject.Find("Username").GetComponent<InputField>().text = name;
    }
}
