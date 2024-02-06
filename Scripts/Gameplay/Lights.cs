//Controls the shifter lights

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lights : MonoBehaviour {
    
    [SerializeField] Image image;
    [SerializeField] GameObject imageObject;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject nums;
    [SerializeField] Text numsText;
    bool use;
    float time;

    void Start() {
        string kart = GameObject.Find("MenuCanvas").GetComponent<FileManager>().currentKart();
        use = kart.Equals("Shifter");
    }

    // Update is called once per frame
    void Update() {
        float RPM = GameObject.Find("Player").GetComponent<PlayerMain>().RPM;
        
        time += Time.deltaTime;
        imageObject.SetActive(use);
        panel.SetActive(use);
        nums.SetActive(use);
        if (use) {
            numsText.text = GameObject.Find("Player").GetComponent<Gearing>().currentGear + "";
            if (RPM > 8500) {
                image.color = Color.white;
                
            }
            if (RPM > 10000) {
                image.color = Color.green;
            }
            if (RPM > 11100) {
                image.color = Color.magenta;
            }
            if (RPM > 12000 || RPM < 100) {
                image.color = Color.red;
                if (Mathf.Round(time * 12) % 2 == 0) {
                    imageObject.SetActive(false);
                }
            }
            if (RPM < 8500 && RPM > 100) {
                imageObject.SetActive(false);
            }
           
        }
         else {
                imageObject.SetActive(use);
            }   
    }
}
