//Works the sliders and buttons in the game menu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class IGSettingsManager : MonoBehaviour {


    float localTime;
    // Start is called before the first frame update
    void Start() {
        GameObject.Find("MenuCanvas").GetComponent<FileManager>().openSettings();
        
    }   

    // Update is called once per frame
    void Update() {
        
        GameObject.Find("MenuCanvas").GetComponent<FileManager>().saveSettings(findInputedSettings());
        
        
    }

    public settingsPackage findInputedSettings() {
        settingsPackage package = new settingsPackage();
        package.FOV = (int) GameObject.Find("FOV").GetComponent<Slider>().value;
        package.Stiffness = (float) GameObject.Find("Stiffness").GetComponent<Slider>().value / 100;
        package.Distance = (float) GameObject.Find("Distance").GetComponent<Slider>().value / 10;
        package.Height = (float) GameObject.Find("Height").GetComponent<Slider>().value / 10;
        package.Angle = (int) GameObject.Find("Angle").GetComponent<Slider>().value;
        package.Username = GameObject.Find("Username").GetComponent<InputField>().text;
        package.Automatic = GameObject.Find("Automatic").GetComponent<Toggle>().isOn;
        package.GearRatio = GameObject.Find("GearRatio").GetComponent<Slider>().value / 100;
        return package;
    }

}
