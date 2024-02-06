//This class controls where the tire is in relation to the road by constantly adjusting height
//It also checks if the tire is on the track or not

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TireBehavior : MonoBehaviour {
    
[SerializeField] public bool onGround;
[SerializeField] public float distance;
Transform transform;

[SerializeField] public bool isOnTrack;

[SerializeField] public LayerMask roadLayer;
[SerializeField] public LayerMask playerLayer;
 

    void Start() {
        transform = GetComponent<Transform>();

    }

    
    void Update() {
        Vector3 position = transform.position;
        //Feeds a position of where the object is as well as where the ground is relative to it
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(downRay, out hit, 1000f, ~playerLayer)) {

            distance = transform.position.y - hit.point.y;
        }

        onGround = Physics.Raycast(position, Vector3.down, 0.8f, ~playerLayer);
        isOnTrack = GameObject.Find("Track").GetComponent<MapInfo>().isOnTrack(position);

        if (Physics.Raycast(downRay, 3f, roadLayer)) {
            isOnTrack = true;
        }

        

    }
   
}
