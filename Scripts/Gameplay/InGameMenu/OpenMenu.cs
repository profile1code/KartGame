//Controls how the menu opens in game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OpenMenu : MonoBehaviour {

    [SerializeField] GameObject resume;
    
    [SerializeField] GameObject settings;
    
    [SerializeField] GameObject exit;

    public bool showing;

    void Start() {
        showing = false;
    }

    // Update is called once per frame
    void Update() {
        //Checks what should happen when the button is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) {
            showing = !showing;
        }
        resume.SetActive(showing);
        settings.SetActive(showing);
        exit.SetActive(showing);

        
    }



}
