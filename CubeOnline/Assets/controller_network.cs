using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller_network : MonoBehaviour{


    public Transform test;
    public Vector3 offset;


   

    void Update(){

        transform.position = test.position + offset;
        transform.rotation = test.rotation;
    }
}
