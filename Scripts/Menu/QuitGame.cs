//Quits the game when pressed

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
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
        Debug.Log("Quit");
        Application.Quit();
    }
}
