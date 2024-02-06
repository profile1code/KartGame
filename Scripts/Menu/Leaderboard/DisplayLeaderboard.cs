//This class controls the behavior of the leaderboard, and chooses
//what will be displayed based on the inputs from the user
//reads files

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class DisplayLeaderboard : MonoBehaviour {
    Text text;
    void Start() {
        text = GetComponent<Text>();
    }
    //Updates the leaderboard every frame, so that when the user changes the settings, the
    //leaderboard is rewritten 
    void Update() {
        string sceneToDisplay = GameObject.Find("TrackSelectorLabel").GetComponent<Text>().text;
        GameObject.Find("Track").GetComponent<Text>().text = sceneToDisplay;
        string address = Application.streamingAssetsPath + "/Data_Log/" + sceneToDisplay + "Times.txt";
        string kartToDisplay = GameObject.Find("KartSelectorLabel").GetComponent<Text>().text;

        StreamReader reader = new StreamReader(address);
        int position = 1;
        GameObject.Find("Username").GetComponent<Text>().text = "";
        GameObject.Find("Time").GetComponent<Text>().text = "";
        GameObject.Find("Position").GetComponent<Text>().text = "";
        GameObject.Find("Kart").GetComponent<Text>().text = "";
        while (!reader.EndOfStream) {
            string line = reader.ReadLine();
            int firstIndex = line.IndexOf(" ");
            float time = (float)System.Convert.ToDouble(line.Substring(0, firstIndex));
            line = line.Substring(firstIndex + 1);
            int secondIndex = line.IndexOf(" ");
            string name = line.Substring(secondIndex + 1);
            string kart = line.Substring(0, secondIndex);
            int minutes = (int)(time - (time % 60)) / 60;
            float seconds = Mathf.Round((time % 60) * 1000) / 1000;
            string newTime = toStopwatch(minutes, seconds);

            if (shouldDisplay(kartToDisplay, kart)) {

                GameObject.Find("Position").GetComponent<Text>().text += position + "\n";
                GameObject.Find("Username").GetComponent<Text>().text += name + "\n";
                GameObject.Find("Time").GetComponent<Text>().text += newTime + "\n";
                GameObject.Find("Kart").GetComponent<Text>().text += kart + "\n";
                position++;
            }
        }
        if (position == 1) {
            GameObject.Find("Username").GetComponent<Text>().text = "No data";
        }
        reader.Close();
    }

     public String toStopwatch(int min, float sec) {
        
        if (min == 0 && sec == 0) {
            return "00:00:000";
        }
        
        if (sec < 10) {
            if (min < 10) {
            return "0" + min + ":0" + sec;
            }
            return min + ":0" + sec;
        }
        if (min < 10) {
        return "0" + min + ":" + sec;
        }
        return min + ":" + sec;
    }

    bool shouldDisplay(string kartToDisplay, string kart) {
        if (kartToDisplay.Equals(kart) || kartToDisplay.Equals("All")) {
            return true;
        }
        return false;
    }
}
