using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingIdentity : MonoBehaviour
{
    //--------------------Placing The Building
    public bool isBeingPlaced, isTouchingAnother;
    //the collider we want to show while placing
    [SerializeField] private GameObject colliderObj;
    //the material renderer
    [SerializeField] private Renderer colliderRen;
    //the materials to swap
    [SerializeField] private Material m_colGreen, m_colRed;
    // number triggers we're currently touching
    private int trigCount;


    //-------------------Building Animations
    // a bool for if it's done / ready
    public bool doneBuilding;
    // % of building progress
    [SerializeField] [Range(0,1)] private float buildPercent;
    // the location offset for the building being built (we'll animate it rising for now)
    //lerp pos
    [SerializeField] private float buildYStartPos, buildYFinishPos, buildTimeOffset;
    private float buildTime;
    private float lerpVal;


    private void Update()
    {
        //-------------------------------Building Animation
        //if we are still building the building after we place it
        if(!doneBuilding && !isBeingPlaced)
        {
            // the value to handle our lerped Y position
            lerpVal = Mathf.Lerp(buildYStartPos, buildYFinishPos, buildTime);

            //if the building isn't up fully then move it
            if (buildTime < 1)
                buildTime += Time.deltaTime * buildTimeOffset;
            else
                doneBuilding = true;
            
            //display for logging
            buildPercent = Mathf.Round(buildTime * 100f) / 100f;

            //move building by offset
            transform.position = new Vector3(transform.position.x, lerpVal, transform.position.z);
        }//end of not done building but done being place


        //-------------------------------Placing the building
        //if we are placing the building
        if (isBeingPlaced)
        {
            //any collisions
            if (trigCount > 1) // ground is assumed to collide but no others
            {
                colliderRen.sharedMaterial = m_colRed;
                isTouchingAnother = true;
            }
            else // no collisions
            {

                colliderRen.material = m_colGreen;
                isTouchingAnother = false;
            }
        }//end of is being place

    }//end of update



    //trigger enter
    void OnTriggerEnter(Collider trig)
    {
        print("CAREFUL WITH THAT BUILDING");
        ++trigCount;
    }

    //triger exit
    void OnTriggerExit(Collider trig)
    {
        --trigCount;
    }


    //a function to place the object
    public void PlaceBuilding()
    {
        //turn off collider visual
        colliderObj.SetActive(false);
        //set as placed
        isBeingPlaced = false;
        //get trigger and set it to a collider
        transform.GetComponent<Collider>().isTrigger = false;

        // UPDATE NAVMESH HERE IF REASONABLE || OR RIGIDBODY

    }//end of place building

}//end of building identity script
