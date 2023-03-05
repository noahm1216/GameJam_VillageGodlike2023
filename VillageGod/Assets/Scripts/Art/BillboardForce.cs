using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardForce : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    void OnEnable()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = mainCam.transform.rotation;
    }

}//end of billboard force