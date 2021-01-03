using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller_cube : MonoBehaviour{


    public float speed = 5;
    public float speed_rotation = 1;
   
   

  
    void Update(){

        if (Input.GetKey(KeyCode.UpArrow)){  
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);  
        }  
         
        if (Input.GetKey(KeyCode.DownArrow)){  
            this.transform.Translate(Vector3.back * Time.deltaTime * speed);  
        }  
         
        if (Input.GetKey(KeyCode.LeftArrow)){  
            this.transform.Rotate(Vector3.up,-speed_rotation);  
        }  
        
        if (Input.GetKey(KeyCode.RightArrow)){  
            this.transform.Rotate(Vector3.up,speed_rotation);  
        }  
    }




}
