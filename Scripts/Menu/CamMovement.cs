//Works the movement of the camera in the main menu, and randomizes it every time it is loaded

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {
    Transform transform;
    float radius = 0;
    float speed = 0;
    bool inverse;
    void Start() {
        Screen.SetResolution(1200, 675, true, 0);
        transform = GetComponent<Transform>();
        transform.localEulerAngles = new Vector3 (0, 90, 0);
        float avgX = GameObject.Find("Checkpoint1").GetComponent<Transform>().position.x + GameObject.Find("Checkpoint2").GetComponent<Transform>().position.x + GameObject.Find("Checkpoint3").GetComponent<Transform>().position.x;
        avgX /= 3;
        float avgY = GameObject.Find("Checkpoint1").GetComponent<Transform>().position.y + GameObject.Find("Checkpoint2").GetComponent<Transform>().position.y + GameObject.Find("Checkpoint3").GetComponent<Transform>().position.y;
        avgY /= 3;
        float avgZ = GameObject.Find("Checkpoint1").GetComponent<Transform>().position.z + GameObject.Find("Checkpoint2").GetComponent<Transform>().position.z + GameObject.Find("Checkpoint3").GetComponent<Transform>().position.z;
        avgZ /= 3;
        
        while (Mathf.Abs(speed) < 5) {
            speed = Random.Range(-8, 15);
        }
        radius = Random.Range(25, 50);
        float random = Random.Range(0, 10);
        inverse = random < 5;
        if (inverse) {
            radius *= -1;
        }
        transform.position = new Vector3(avgX + radius * 2, avgY + 12, avgZ - radius);
        


    }

    // Update is called once per frame
    void FixedUpdate() {
        float rotation = -360 / ((2 * radius * Mathf.PI) / speed) * Time.deltaTime;
        transform.Rotate(new Vector3(0, rotation, 0));
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
}
