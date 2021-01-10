using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dev_script : MonoBehaviour{
    
    public bool skip_menu;
    public GameObject canvas;
    string data = "/0/0/0/0/0/0/0/";
    int id = 1;

    public int testid;

    void Start(){

        if(skip_menu){  
            Invoke("start_without_menu",0.2f);  
        }  
    }



    void start_without_menu(){
        canvas.SetActive(!skip_menu);
        StartCoroutine(network.inst.createplayer(0,skip_menu, 1));  
        StartCoroutine(network.inst.network_position_send());
    }

   
    void Update(){

        if(Input.GetKeyDown(KeyCode.Q)){ 
            id = 1; 
            network.inst.receive_data("P/"+id+data+network.inst.id_event);   
        } 
        
        if(Input.GetKeyDown(KeyCode.W)){  
            id = 2;
            network.inst.receive_data("P/"+id+data+ network.inst.id_event);   
        } 
        
        
        if(Input.GetKeyDown(KeyCode.E)){  
            id = 3;
            network.inst.receive_data("P/"+id+data+ network.inst.id_event);   
        } 

        if(Input.GetKeyDown(KeyCode.F)){     
            StartCoroutine(floor_creation.inst.delete_floor(0f));
            network.inst.id_event = 1;   
        }      
    }



    IEnumerator simule_network_msg(){

        yield return new WaitForSeconds(1f);

        while(true){
            network.inst.receive_data("P/"+testid+"/0/0/0/0/0/0/2/0");
            yield return new WaitForSeconds(1f);
        }
    }

}
