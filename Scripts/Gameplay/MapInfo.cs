//Saves the information for the player's spawn position as well as a heat map of the paint used

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour {

    [SerializeField] Vector3 SpawnLocation;
    [SerializeField] Vector3 SpawnRotation;

    public Terrain terrain;
    public int xPos;
    public int zPos;
    public float[] textureValues;

    


    // Start is called before the first frame update
    void Start() {
        StartDriving();
        terrain = GetComponent<Terrain>();
        textureValues = new float[1];
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            StartDriving();
        }
        
    }

    void StartDriving() {
        GameObject.Find("Player").GetComponent<PlayerMain>().transform.position = SpawnLocation;
        GameObject.Find("Player").GetComponent<PlayerMain>().transform.localEulerAngles = SpawnRotation;
        GameObject.Find("Start/Finish").GetComponent<Timing>().firstLap = true;
        GameObject.Find("Start/Finish").GetComponent<Timing>().currentLap = 0.0f;
        GameObject.Find("Start/Finish").GetComponent<Timing>().lastLap = 0.0f;
    }
    //Gives each pixel on the map a terrainData
    public bool isOnTrack(Vector3 Position) {
        
        //found most of this on the internet https://johnleonardfrench.com/terrain-footsteps-in-unity-how-to-detect-different-textures/
        Vector3 terrainPos = Position - terrain.transform.position;
        Vector3 mapPos = new Vector3(terrainPos.x / terrain.terrainData.size.x, 0, terrainPos.z / terrain.terrainData.size.z);
        float xCoord = mapPos.x * terrain.terrainData.alphamapWidth;
        float zCoord = mapPos.z * terrain.terrainData.alphamapHeight;
        xPos = (int)xCoord;
        zPos = (int)zCoord;
        float[,,] aMap = terrain.terrainData.GetAlphamaps(xPos, zPos, 1, 1);
        textureValues[0] = aMap[0,0,0];
        if (!(textureValues[0] > 0)) {
            return true;
        }
        return false;
    }
}
