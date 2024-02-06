//This class controls how the time is saved when the user exits the track to the menu
//Changes what file is written to depending on the scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class SaveTimes : MonoBehaviour {
    string DocName;
    Scene scene;
    public Button button;
    
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        scene = SceneManager.GetActiveScene();
        DocName = Application.streamingAssetsPath + "/Data_Log/" + scene.name + "Times.txt";
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnClick() {
        if (GameObject.Find("Start/Finish").GetComponent<Timing>().bestLap > 1f) {
            saveTime();
        }
    }
    //saves the time if it has been called
    void saveTime() {
        if (!File.Exists(Application.streamingAssetsPath + "/Data_Log/")) {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Data_Log/");
        }

        float lapTime = GameObject.Find("Start/Finish").GetComponent<Timing>().bestLap;
        string username = GameObject.Find("MenuCanvas").GetComponent<FileManager>().findVariable("Username");
        string kartUsed = GameObject.Find("MenuCanvas").GetComponent<FileManager>().currentKart();

        makeNewLeaderboard(lapTime + " " + kartUsed + " " + username);
    }

    //This updates the file and slots in the new time in the correct position so that
    //it is in order when it gets to the leaderboard
    void makeNewLeaderboard(string newLine) {
        
        if (!File.Exists(DocName)) {
                File.AppendAllText(DocName, newLine + "\n");
            }
        else {
            StreamReader reader = new StreamReader(DocName);
            Queue<string> times = new Queue<string>();
            bool timeAdded = false;

            
            string newTime = newLine.Substring(0, newLine.IndexOf(" "));
            
            
            while (!reader.EndOfStream) {
                string thisLine = reader.ReadLine();
                string thisTime = thisLine.Substring(0, thisLine.IndexOf(" "));
                if (System.Convert.ToDouble(thisTime) < System.Convert.ToDouble(newTime) || timeAdded) {
                    times.Enqueue(thisLine);
                }
                else {
                    times.Enqueue(newLine);
                    times.Enqueue(thisLine);
                    timeAdded = true;
                }

            }
            if (!timeAdded) {
                times.Enqueue(newLine);
            }

            reader.Close();
            File.WriteAllText(DocName, "");
            foreach(string line in times) {
                File.AppendAllText(DocName, line + "\n");
            }
        }

    } 


}
