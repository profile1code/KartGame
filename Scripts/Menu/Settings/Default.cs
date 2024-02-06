using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Default : MonoBehaviour {
    public Button button;
    // Start is called before the first frame update
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnClick() {
        GameObject.Find("FOV").GetComponent<Slider>().value = 60;
        GameObject.Find("Stiffness").GetComponent<Slider>().value = 0.7f * 100;
        GameObject.Find("Distance").GetComponent<Slider>().value = 3.5f * 10;
        GameObject.Find("Height").GetComponent<Slider>().value = 1f * 10;
        GameObject.Find("Angle").GetComponent<Slider>().value = 10f;
        GameObject.Find("Username").GetComponent<InputField>().text = "";
        GameObject.Find("Automatic").GetComponent<Toggle>().isOn = false;
        GameObject.Find("GearRatio").GetComponent<Slider>().value = 1.8f * 100;
    }
}
