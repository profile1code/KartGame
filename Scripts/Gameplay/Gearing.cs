//This class controls the ratios for the karts,
//and allows the shifter kart to work

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Gearing : MonoBehaviour {

    public float finalDrive;
    float first = 4.5f;
    float second = 2.5f;
    float third = 1.9f;
    float fourth = 1.5f;
    float fifth = 1.2f;
    float sixth = 1f;
    public int currentGear;
    public bool isShifter;
    public bool Auto;
    float RPM;

    void Start() {
        currentGear = 1;
    }

    // Update is called once per frame
    void Update() {
        string kartType = GameObject.Find("MenuCanvas").GetComponent<FileManager>().currentKart();
        //Checks for shifter enabled
        isShifter = kartType.Equals("Shifter");
        Auto = bool.Parse(GameObject.Find("MenuCanvas").GetComponent<FileManager>().findVariable("Automatic"));
        RPM = GameObject.Find("Player").GetComponent<PlayerMain>().RPM;
        finalDrive = (float) System.Convert.ToDouble(GameObject.Find("MenuCanvas").GetComponent<FileManager>().findVariable("GearRatio"));

        if (isShifter) {
            if (Auto) {
                if (RPM > 12650 && currentGear < 6) {
                    currentGear += 1;
                }
                if (RPM < (6000 + (500 * currentGear))  && currentGear > 1 && RPM > 0) {
                    currentGear -= 1;
                }
            }
            else {
                if ((Input.GetKeyDown(KeyCode.L) || Input.GetMouseButtonDown(1)) && currentGear < 6) {
                    currentGear += 1;
                }
                if ((Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0)) && currentGear > 1) {
                    currentGear -= 1;
                }
            }
        }
        else {
            currentGear = 1;
        }
    }
    //Grabs the ratio (shifter only)
    public float getRatio() {
        
        if (isShifter) {
            if (currentGear == 1) {
                return first * finalDrive;
            }
            if (currentGear == 2) {
                return second * finalDrive;
            }
            if (currentGear == 3) {
                return third * finalDrive;  
            }
            if (currentGear == 4) {
                return fourth * finalDrive;
            }
            if (currentGear == 5) {
                return fifth * finalDrive;
            }
            return sixth * finalDrive;
        }

        else {
            return finalDrive;
        }
    }
}
