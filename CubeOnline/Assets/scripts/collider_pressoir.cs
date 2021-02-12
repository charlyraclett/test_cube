using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_pressoir : MonoBehaviour{



    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player"){      
            controller_cube cube = col.gameObject.GetComponent<controller_cube>();
            cube.controller_dead();      
        }
    }








    
}
