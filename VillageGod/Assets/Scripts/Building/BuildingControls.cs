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

    //get the mask to raycast against either the player or BuildingGround layer
    //int layer_maskHittable = LayerMask.GetMask("Player", "BuildingGround");
    public LayerMask layer_maskHittable;

    public bool hasBuilding;
    public float offset = 20;
    public Vector3 buildingPlacement;

    // Start is called before the first frame update
    void Start()
    {
        //zero out vector 3
        buildingPlacement = Vector3.zero;

    }//end start

    // Update is called once per frame
    void Update()
    {
        if (hasBuilding)
        {
            Debug.Log("have building");


            //----------------------------------------------------------Raycast
            Ray ray = camMain.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, layer_maskHittable))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("hit");
                buildingPlacement = hit.point;
            }
            else
            {
                Debug.Log("not hit");
                buildingPlacement = Vector3.zero;
            }

            //---------------------------------------------Mouse Position 3D
            if(buildingPlacement != Vector3.zero)
            {
                transform.position = buildingPlacement;
                //Vector3 mousePos = Input.mousePosition;
                //mousePos.z = camMain.nearClipPlane + offset;
                //Vector3 Worldpos = camMain.ScreenToWorldPoint(mousePos);
                //transform.position = Worldpos;
            }
            
        }
       
    }//end update



}//end of building controls
