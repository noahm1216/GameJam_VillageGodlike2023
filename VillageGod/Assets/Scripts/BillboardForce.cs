using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardForce : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(mainCam.transform);
        transform.Rotate(0, 180, 0);
    }

}//end of billboard force