//Second class controlling the behavior of the menu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class OpenSettings : MonoBehaviour {
    
    GameObject SettingsUI;

    public bool isShowing;

    [SerializeField] Button button;

    void Start() {
        isShowing = false;
        SettingsUI = GameObject.Find("SettingsUI");
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update() {
        SettingsUI.SetActive(isShowing);
        
        if (Input.GetKeyDown(KeyCode.Escape) && isShowing) {
            isShowing = false;
        }

    }

    void OnClick() {
        isShowing = true;
        GameObject.Find("MenuCanvas").GetComponent<OpenMenu>().showing = false;
    }
}
