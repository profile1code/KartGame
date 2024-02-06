using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FileManager : MonoBehaviour {
    // Start is called before the first frame update

    string settingsDocName = Application.streamingAssetsPath + "/Data_Log/Settings.txt";
    void Start() {
       setResolution();
        
    }

    public void openSettings() {


        if (!File.Exists(Application.streamingAssetsPath + "/Data_Log/")) {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Data_Log/");
            
        }
        
        settingsPackage currentSettings = settingsGetter();
        setMenuSettings(currentSettings);

    }

    void Update() {
        
    }

    public settingsPackage settingsGetter() {
        settingsPackage data = new settingsPackage();
        if (!File.Exists(settingsDocName)) {
                makeDefaultSettingsFile();
            }
            
        StreamReader reader = new StreamReader(settingsDocName);
        while (!reader.EndOfStream) {
            string current = reader.ReadLine();
            
            int i = current.IndexOf(" ");
            
            string first = current.Substring(0, i); 
            string second =  current.Substring(i + 1);
            if (first.Equals("FOV")) {
                data.FOV = System.Convert.ToInt32(second);
            }
            if (first.Equals("Stiffness")) {
                 data.Stiffness = (float) System.Convert.ToDouble(second);
            }
            if (first.Equals("Distance")) {
                 data.Distance = (float) System.Convert.ToDouble(second);
            }
            if (first.Equals("Height")) {
                 data.Height = (float) System.Convert.ToDouble(second);
            }
            if (first.Equals("Angle")) {
                data.Angle = System.Convert.ToInt32(second);
            }
            if (first.Equals("Username")) {
                 data.Username = second;
            }
            if (first.Equals("Automatic")) {
                 data.Automatic = bool.Parse(second);
            }
            if (first.Equals("GearRatio")) {
                 data.GearRatio = (float) System.Convert.ToDouble(second);
            }                    

        }
        reader.Close();
        return data;
    }

    public void settingsWriter() {
        Debug.Log("Called");
    }

    public void makeDefaultSettingsFile() { 

        File.AppendAllText(settingsDocName, "FOV 90 \n");
        File.AppendAllText(settingsDocName, "Stiffness 0.5 \n");
        File.AppendAllText(settingsDocName, "Distance 3.5 \n");
        File.AppendAllText(settingsDocName, "Height 1.0 \n");
        File.AppendAllText(settingsDocName, "Angle 10 \n");
        File.AppendAllText(settingsDocName, "Unknown");
        File.AppendAllText(settingsDocName, "Automatic false");
        File.AppendAllText(settingsDocName, "GearRatio 1.8");

    }

    public void setMenuSettings(settingsPackage settings) {
        GameObject.Find("FOV").GetComponent<Slider>().value = settings.FOV;
        GameObject.Find("Stiffness").GetComponent<Slider>().value = settings.Stiffness * 100;
        GameObject.Find("Distance").GetComponent<Slider>().value = settings.Distance * 10;
        GameObject.Find("Height").GetComponent<Slider>().value = settings.Height * 10;
        GameObject.Find("Angle").GetComponent<Slider>().value = settings.Angle;
        GameObject.Find("Username").GetComponent<InputField>().text = settings.Username;
        GameObject.Find("Automatic").GetComponent<Toggle>().isOn = settings.Automatic;
        GameObject.Find("GearRatio").GetComponent<Slider>().value = settings.GearRatio * 100;
    }



    //End File -> Settings


    //Settings -> File


    public void saveSettings(settingsPackage settings) {
        
        string FOV = "FOV " + settings.FOV + " \n";
        string Stiffness = "Stiffness " + settings.Stiffness + " \n";
        string Distance = "Distance " + settings.Distance + " \n";
        string Height = "Height " + settings.Height + " \n";
        string Angle = "Angle " + settings.Angle + " \n";
        string Username = "Username " + settings.Username + " \n";
        string Automatic = "Automatic " + settings.Automatic + " \n";
        string GearRatio = "GearRatio " + settings.GearRatio + " \n";

        File.WriteAllText(settingsDocName, FOV);
        File.AppendAllText(settingsDocName, Stiffness);
        File.AppendAllText(settingsDocName, Distance);
        File.AppendAllText(settingsDocName, Height);
        File.AppendAllText(settingsDocName, Angle);
        File.AppendAllText(settingsDocName, Username);
        File.AppendAllText(settingsDocName, Automatic);
        File.AppendAllText(settingsDocName, GearRatio);
        

    }

    public string findVariable(string type) {
        if (!File.Exists(settingsDocName)) {
                makeDefaultSettingsFile();
            }
            
        StreamReader reader = new StreamReader(settingsDocName);
        
        string data = "Unknown";
        while (!reader.EndOfStream) {
            string current = reader.ReadLine();
                
            int i = current.IndexOf(" ");
                
             string first = current.Substring(0, i); 
             string second =  current.Substring(i + 1);
             if (first.Equals(type)) {
                    data = second;
             }   

         }
        reader.Close();
        return data;
    }

    public string currentKart() {
        
        if (!File.Exists(Application.streamingAssetsPath + "/Data_Log/")) {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Data_Log/");
        }
        string KartDocName = Application.streamingAssetsPath + "/Data_Log/Kart.txt";
        if (!File.Exists(KartDocName)) {
            File.WriteAllText(KartDocName, "Sprint" + "\n");
        }
        
        StreamReader reader = new StreamReader(KartDocName);
        string kartType = reader.ReadLine();
        reader.Close();
        return kartType;
    }

    public int getResolution() {
        string ResDocName = Application.streamingAssetsPath + "/Data_Log/Resolution.txt";
        if (!File.Exists(Application.streamingAssetsPath + "/Data_Log/")) {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Data_Log/");
        }
        if (!File.Exists(ResDocName)) {
            File.WriteAllText(ResDocName, "1920" + "\n");
        }
        StreamReader reader = new StreamReader(ResDocName);
        int resHorizontal = System.Convert.ToInt32(reader.ReadLine());
        reader.Close();
        return resHorizontal;

    }

    public void setResolution() {
    
        int horizontal = getResolution();
        int vertical = horizontal / 16;
        vertical *= 9;
        Screen.SetResolution(horizontal, vertical, true, 0);
    
    }

}

public class settingsPackage {

    public int FOV;
    public float Stiffness;
    public float Distance;
    public float Height;
    public int Angle;
    public string Username;
    public bool Automatic;
    public float GearRatio;

    public settingsPackage() {

    }

}
