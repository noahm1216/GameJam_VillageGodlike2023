using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BuildingIdentity : MonoBehaviour
{

    public bool isBeingPlaced, isTouchingAnother;
    //the collider we want to show while placing
    [SerializeField] private GameObject colliderObj;
    //the material renderer
    [SerializeField] private Renderer colliderRen;
    //the materials to swap
    [SerializeField] private Material m_colGreen, m_colRed;
   
    private int trigCount;


    private void Update()
    {



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
    }//end of place building

}//end of building identity script
