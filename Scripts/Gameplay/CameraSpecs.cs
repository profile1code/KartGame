//This class controls the camera, which is modified in settings

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraSpecs : MonoBehaviour {

    Camera cam;
    Transform transform;

    int FOV;
    float Stiffness;
    float Distance;
    float Height;
    int Angle;

    void Start() {
        cam = GetComponent<Camera>();
        transform = GetComponent<Transform>();
        updateSettings(GameObject.Find("MenuCanvas").GetComponent<FileManager>().settingsGetter());
        
    }

    void FixedUpdate() {
        //Updates cam multiple times a frame to make it less stuttery
        updateSettings(GameObject.Find("MenuCanvas").GetComponent<FileManager>().settingsGetter());
        float vel = Mathf.Round(GameObject.Find("Player").GetComponent<PlayerMain>().velocity * 1000) / 1000;
        float newSide = Mathf.Sqrt((Distance * Distance) + (Height + Height));
        float camMovement = (vel * (1 - Stiffness) * (newSide + 1)) / 35;
        float radiansAngle = Angle * Mathf.PI / 180;
        transform.localPosition = new Vector3(0f, Height + (Mathf.Sin(radiansAngle) * camMovement), (Distance * -1) - (Mathf.Cos(radiansAngle) * camMovement));
        transform.localEulerAngles = new Vector3(Angle, 0f, 0f);
        cam.fieldOfView = FOV;
    
    }

    //Grabs the camera settings from the file
    public void updateSettings(settingsPackage settings) {
        FOV = settings.FOV;
        Stiffness = settings.Stiffness;
        Distance = settings.Distance;
        Height = settings.Height;
        Angle = settings.Angle;

    }

}
