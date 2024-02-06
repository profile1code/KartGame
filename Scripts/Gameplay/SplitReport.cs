//Shows or hides the numbers on the side of the screen which show the gap to
//the ghost

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SplitReport : MonoBehaviour {
    // Start is called before the first frame update
    public Text text;
    bool beenCalledThisCheckpoint;
    
    // Start is called before the first frame update
    void Start() {
        text = GetComponent<Text>();
        text.text = "";
        
    }

    // Update is called once per frame
    void Update() {
        
        

    }

    public void showTime(float current, float best) {
        
        if (!beenCalledThisCheckpoint) {
            float rawTime = current - best;
            float simplifiedTime = Mathf.Round(rawTime * 1000) / 1000;
            String time = simplifiedTime + "";
            time = time.Substring(0, (time.IndexOf(".") + 4));

            if (simplifiedTime == 0 || GameObject.Find("Start/Finish").GetComponent<Timing>().bestLap == 0f) {
                text.text = time;
                text.color = Color.grey;
            }
            else if (simplifiedTime > 0) {
                text.text = "+" + time;
                text.color = Color.red;
            }
            else if (simplifiedTime < 0) {
                text.text = time;
                text.color = Color.green;

            beenCalledThisCheckpoint = false;
            StartCoroutine(textHider());
         }
        

        }
    }

    public void endLap() {
        beenCalledThisCheckpoint = false;
    }

    IEnumerator textHider() {
        yield return new WaitForSeconds(4);
        text.text = "";
    }
}
