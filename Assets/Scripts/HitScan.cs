using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScan : MonoBehaviour
{
    Camera myCamera = null;

    // Start is called before the first frame update
    void Awake()
    {
        myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask layermask = new LayerMask();
        layermask += LayerMask.NameToLayer("CustomIgnoreRaycast");

        RaycastHit hit = new RaycastHit();
        Ray ExampleRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ExampleRay,out hit,layermask))
        {
            transform.position = hit.point;
        }
    }
}
