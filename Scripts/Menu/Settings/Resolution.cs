//This class controls the resolution in the application, and allows the user
//to change it to suit the device's performance

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;

public class Resolution : MonoBehaviour {

    public Button apply;
    string DocName = Application.streamingAssetsPath + "/Data_Log/Resolution.txt";
    int maxHorizontal = 2560;
    int minHorizontal = 320;
    int minVertical = 200;

    void Start() {
        apply = GameObject.Find("ApplyResolution").GetComponent<Button>();
        apply.onClick.AddListener(OnClick);
        if (!File.Exists(Application.streamingAssetsPath + "/Data_Log/")) {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Data_Log/");
        }
        if (!File.Exists(DocName)) {
            File.WriteAllText(DocName, "1920" + "\n");
        }
        int res = GameObject.Find("Canvas").GetComponent<FileManager>().getResolution();
        GameObject.Find("InputRes").GetComponent<InputField>().text = res + "";
    }

    
    void Update() {
        //Sets the text fields to display the correct info
        if (GameObject.Find("InputRes").GetComponent<InputField>().text.Length > 0) {
            int input = System.Convert.ToInt32(GameObject.Find("InputRes").GetComponent<InputField>().text);
        if (input < minHorizontal) {
            input = minHorizontal;
        }
        if (input > maxHorizontal) {
            input = maxHorizontal;
        }
        GameObject.Find("HorizontalResNum").GetComponent<Text>().text = input + "";
        input /= 16;
        input *= 9;
        GameObject.Find("VerticalResNum").GetComponent<Text>().text = input + "";
        }
        else {
            GameObject.Find("HorizontalResNum").GetComponent<Text>().text = minHorizontal + "";
            GameObject.Find("VerticalResNum").GetComponent<Text>().text = minVertical + "";
        }
        
    }

    void OnClick() {
        string horizontal = GameObject.Find("HorizontalResNum").GetComponent<Text>().text;
        string vertical = GameObject.Find("VerticalResNum").GetComponent<Text>().text;
        Debug.Log(horizontal + " " + vertical);
        int horizontalInt = System.Convert.ToInt32(horizontal);
        int verticalInt = System.Convert.ToInt32(vertical);
        Screen.SetResolution(horizontalInt, verticalInt, true, 0);
        
        File.WriteAllText(DocName, horizontal + "\n");
            
    }
        
}
