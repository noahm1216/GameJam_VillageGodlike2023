using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingResources : MonoBehaviour
{

    // list of resources
    private int resChickenWood, resSquareFarm, resBunHouse;
    // list of UI for resources
    [SerializeField] private TextMeshProUGUI textChickenWood, textSquareFarm, textBunHouse;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown("space"))
        {
            AddResources(1, 1, 1);
        }
    }

    //function to add resources
    public void AddResources(int _incomingWood, int _incomingFarm, int _incomingHouse)
    {
        resChickenWood += _incomingWood;
        resSquareFarm += _incomingFarm;
        resBunHouse += _incomingHouse;

        //update UI
        UpdateUI();
    }//end of add resources

    //function to request resources (if we have enough then we remove them from counter)
    public int ResourceRequest(int _reqWood, int _reqFarm, int _reqHouse)
    {
        //if we subtract the requests and dont have any negatives then we're good
        if (resChickenWood - _reqWood >= 0 && resSquareFarm - _reqFarm >= 0 && resBunHouse - _reqHouse >= 0)
        {
            //subtract resources
            resChickenWood -= _reqWood;
            resSquareFarm -= _reqFarm;
            resBunHouse -= _reqHouse;
            //update UI
            UpdateUI();
            return 1; // 1 for enough resources available to fulfill request
        }
        else // else we would be in debt
            return 0;// 0 for not enough
    }//end of resource request

    // function to call to update resource counter
    private void UpdateUI()
    {
        textChickenWood.text = resChickenWood.ToString();
        textSquareFarm.text = resSquareFarm.ToString();
        textBunHouse.text = resBunHouse.ToString();
    }//end of update UI

}//end of building resources
