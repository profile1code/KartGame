//This class controls the pitch of the engine audio

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudio : MonoBehaviour {

    public AudioSource engine;
    float RPM;
    float time;
    // Start is called before the first frame update
    void Start() {
        engine = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        RPM = GameObject.Find("Player").GetComponent<PlayerMain>().RPM;
        float pitch = (RPM) / (12500) * 6;
        
        engine.pitch = pitch;
        
        
    }

    void play() {
        engine.Play();
    }
}
