using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>
/// Building Controls handles all building models and the process from UI to Placing
/// </para>
/// </summary>
public class BuildingControls : MonoBehaviour
{
    [SerializeField] private Camera camMain;
    [SerializeField] private Vector3 placementOffset;
    [SerializeField] private bool hasBuilding;
    [SerializeField] private Vector3 buildingPlacement;

    public LayerMask layer_maskHittable;

    private GameObject objToPlace;
    private BuildingIdentity scrpt_BuildId;

    // Start is called before the first frame update
    void Start()
    {
        //zero out vector 3
        buildingPlacement = Vector3.zero;
    }//end start

    // Update is called once per frame
    void Update()
    {
        //if is busy return; ??


        if (hasBuilding)
        {
            //----------------------------------------------------------Raycast
            Ray ray = camMain.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, layer_maskHittable))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("hit");
                buildingPlacement = hit.point;

                //left mouse click
                if (Input.GetMouseButtonDown(0))
                {
                    //if touching another object
                    if (scrpt_BuildId.isTouchingAnother)
                    {
                        print("cant place here");
                    }
                    else//not touching another obj
                    {
                        print("Place Building");
                        //place it and reset
                        objToPlace.transform.SetParent(null); // we will want to set the parent to a consistent obj
                        scrpt_BuildId.PlaceBuilding();
                        scrpt_BuildId = null;
                        objToPlace = null;
                        hasBuilding = false;
                    }
                    
                }//end of left mouse click
            }
            else
            {
                Debug.Log("not hit");
                buildingPlacement = Vector3.zero;
            }

            //---------------------------------------------Mouse Position 3D
            if(buildingPlacement != Vector3.zero)
            {
                transform.position = buildingPlacement;// + placementOffset;
            }
            
        }//end of has building
       
    }//end update


    //a function to spawn a building (mostly through UI buttons with a prefab reference)
    public void SpawnBuilding(GameObject _building)
    {
        //if no building assigned
        if (_building == null) { print("no asset to spawn"); return; }

        //if has a building
        if (hasBuilding) { print("have a building already... deleted"); Destroy(objToPlace, 0); hasBuilding = false; }

        //on click }nstantiate
        GameObject clone_buildingPrefab = Instantiate(_building, _building.transform);
        clone_buildingPrefab.transform.SetParent(this.transform);
        clone_buildingPrefab.transform.localPosition = Vector3.zero;
        objToPlace = clone_buildingPrefab;
        //script ref
        scrpt_BuildId = objToPlace.transform.GetComponent<BuildingIdentity>();
        scrpt_BuildId.isBeingPlaced = true;
        hasBuilding = true;

    }//end of spawn building

}//end of building controls
