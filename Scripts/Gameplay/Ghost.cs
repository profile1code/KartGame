//This class controls the ghost behavior, and saves the player's position and 
//plays it back when a valid lap had been achieved


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Ghost : MonoBehaviour {
    
    //public Queue<GhostRecordingType> current = new Queue<>();
    public LinkedList<GhostRecordingType> current;
    LinkedListNode<GhostRecordingType> best;
    LinkedListNode<GhostRecordingType> save;
    float timer;
    float time;
    public int i;
    public int j;


    [SerializeField] GameObject ghost;
    bool showGhost;
    Transform transform;
    Rigidbody body;
    

    void Start() {
        transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody>();
        current  = new LinkedList<GhostRecordingType>();
        showGhost = true;
    }

    // Update is called once per frame
    void Update() {
        //saves the player's fields every frame
        time = GameObject.Find("Start/Finish").GetComponent<Timing>().currentLap;
        timer += Time.deltaTime;
        if (best != null) {
            while (best.Value.time <= timer) {
                best = best.Next;
                i++;
            }
                j++;
                transform.localEulerAngles = best.Value.orientation;
                transform.position = best.Value.position;
        }
        
        
        Vector3 position = GameObject.Find("Player").GetComponent<PlayerMain>().bodyPosition;
        Vector3 rotation = GameObject.Find("Player").GetComponent<PlayerMain>().rotationSave;
        
        current.AddLast(new GhostRecordingType (position, rotation, timer));
        //Can hide/show ghost on the fly
        if (Input.GetKeyDown(KeyCode.H)) {
            showGhost = !showGhost;
        }
        bool firstLap = GameObject.Find("Start/Finish").GetComponent<Timing>().firstLap;
        if (!showGhost || firstLap) {
            transform.position = new Vector3 (transform.position.x, transform.position.y - 1000f, transform.position.z);
        }

    }


    public void newBest() {
        best = current.First;
        save = best;
        current = new LinkedList<GhostRecordingType>();

     }

     public void resetLap() {
         current = new LinkedList<GhostRecordingType>();
         best = save;
         i = 0;
         j = 0;
         timer = 0f;

     }
    
}
//class to save all of the info each frame
public class GhostRecordingType {

    public Vector3 position;
    public Vector3 orientation;
    public float time;

    public GhostRecordingType (Vector3 newPosition, Vector3 newOrientation, float newTime) {
        this.position = newPosition;
        this.orientation = newOrientation;
        this.time = newTime;

    }

}
