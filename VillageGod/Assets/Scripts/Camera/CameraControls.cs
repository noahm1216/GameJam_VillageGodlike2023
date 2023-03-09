using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class CameraControls : MonoBehaviour
{
    [Tooltip("the buttons to move the camera")]
    [SerializeField] public List<KeyMapping> listOfKeyMaps = new List<KeyMapping>();

    public Space moveSpace; //suggested local
    public float moveSpeed = 10.0f; //suggested 10

    public bool isOrtho;
    public Space zoomSpace; //suggested world
    public Camera camMain;
    private Vector3 camPos;
    private float zoomOffset = 0;
    public float zoomSpeed = 100f; // suggested 100 if NOT Ortho
    public Vector2 zoomRange = new Vector2(-100, 100);
    public Vector2 zoomRangeOrtho = new Vector2(-10, 10);

    private void Start()
    {
        camPos = camMain.transform.localPosition;
        isOrtho = camMain.orthographic;
    }//end start

    // Update is called once per frame
    void Update()
    {
        //no keys means stop
        if (listOfKeyMaps.Count == 0) { print("No Keys Listed for Camera on: " + transform.name); return; }

        // move camera up
        if (Input.GetKey(listOfKeyMaps[0].keyMap1) || Input.GetKey(listOfKeyMaps[0].keyMap2)) { transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, moveSpace); }
        // move camera left
        if (Input.GetKey(listOfKeyMaps[1].keyMap1) || Input.GetKey(listOfKeyMaps[1].keyMap2)) { transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, moveSpace); }
        //move camera down
        if (Input.GetKey(listOfKeyMaps[2].keyMap1) || Input.GetKey(listOfKeyMaps[2].keyMap2)) { transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, moveSpace); }
        // move camera right
        if (Input.GetKey(listOfKeyMaps[3].keyMap1) || Input.GetKey(listOfKeyMaps[3].keyMap2)) { transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, moveSpace); }

       
        if (isOrtho) //changes ortho cam size
        { 
            //zoom camera in
            if (Input.mouseScrollDelta.y < 0 || Input.GetKey(listOfKeyMaps[4].keyMap2)) { if (zoomOffset <= zoomRangeOrtho.y) { camMain.orthographicSize += zoomSpeed * Time.deltaTime; zoomOffset++; if (camMain.orthographicSize <= 0.1f) { camMain.orthographicSize = 0.1f; } } }
            //zoom camera out
            if (Input.mouseScrollDelta.y > 0 || Input.GetKey(listOfKeyMaps[5].keyMap2)) { if (zoomOffset >= zoomRangeOrtho.x) { camMain.orthographicSize -= zoomSpeed * Time.deltaTime; zoomOffset--; } }
        }
        else //moves cam obj forward or outward
        {
            //zoom camera in
            if (Input.mouseScrollDelta.y > 0 || Input.GetKey(listOfKeyMaps[4].keyMap2)) { if (zoomOffset <= zoomRange.y) { camMain.transform.Translate(camMain.transform.forward * zoomSpeed * Time.deltaTime, zoomSpace); zoomOffset++; } }
            //zoom camera out
            if (Input.mouseScrollDelta.y < 0 || Input.GetKey(listOfKeyMaps[5].keyMap2)) { if (zoomOffset >= zoomRange.x) { camMain.transform.Translate(camMain.transform.forward * -zoomSpeed * Time.deltaTime, zoomSpace); zoomOffset--; } }
        }
        
        

    }//end of update


}//end of camera controls

//_____________________________________________________ a dictionary for key mappings
[Serializable]
public class KeyMapping : IComparable<KeyMapping>
{
    //______________________variables for the level loader
    public string keyName;
    public string keyMap1;
    public string keyMap2;




    // the data list to add to the dictionary
    public KeyMapping(string newKeyName, string newKeyMap1, string newKeyMap2)
    {
        //save info
        keyName = newKeyName;
        keyMap1 = newKeyMap1;
        keyMap2 = newKeyMap2;
    }// end of tile data dictionary


    //This method is required by the IComparable
    //interface. 
    public int CompareTo(KeyMapping other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return 0;
    }

}//end of tile data list class


