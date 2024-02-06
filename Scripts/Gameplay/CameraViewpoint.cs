//This class changes the camera viewpoint

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewpoint : MonoBehaviour {
    
    Transform transform;
    [SerializeField] GameObject ThirdPerson;
    [SerializeField] GameObject RearViewThirdPerson;
    [SerializeField] GameObject FirstPerson;
    [SerializeField] GameObject RearViewFirstPerson;
    [SerializeField] GameObject Bumper;
    [SerializeField] GameObject RearViewBumper;
    

    int viewNum;
    
    
    void Start() {
        transform = GetComponent<Transform>();
        viewNum = 1;
        
    }

    // Update is called once per frame
    void Update() {
        disableAll();
        if (viewNum == 1) {
            if (Input.GetKey(KeyCode.R)) {
                RearViewThirdPerson.SetActive(true);
            }
            else {
                ThirdPerson.SetActive(true);
            }
        }
        else if (viewNum == 2) {
            if (Input.GetKey(KeyCode.R)) {
                RearViewFirstPerson.SetActive(true);
            }
            else {
                FirstPerson.SetActive(true);
            }
        }
        else if (viewNum == 3) {
            if (Input.GetKey(KeyCode.R)) {
                RearViewBumper.SetActive(true);
            }
            else {
                Bumper.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (viewNum == 1 || viewNum == 2) {
                viewNum++;
            }
            else {
                viewNum = 1;
            }
        }
    }

    void disableAll() {
        ThirdPerson.SetActive(false);
        RearViewThirdPerson.SetActive(false);
        FirstPerson.SetActive(false);
        RearViewFirstPerson.SetActive(false);
        Bumper.SetActive(false);
        RearViewBumper.SetActive(false);
    }

}