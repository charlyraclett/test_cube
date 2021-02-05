using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class replace_floor_dev : MonoBehaviour
{
   
    void Start(){

        falling_floor[] floor = FindObjectsOfType<falling_floor>();
        base_id[] bases = FindObjectsOfType<base_id>();
       



        foreach (falling_floor _floor_ground in floor){
            _floor_ground.transform.localPosition = new Vector3(0, 0,0);
        }  


        foreach (base_id _base in bases){
           _base.transform.position = new Vector3(0, 0, 0);
        } 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
