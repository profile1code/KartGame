//Makes the resume menu button remove the menu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeBehavior : MonoBehaviour {
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
        GameObject.Find("MenuCanvas").GetComponent<OpenMenu>().showing = false;
    }
}
