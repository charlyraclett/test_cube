using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dev_script : MonoBehaviour{
    
    public bool skip_menu;
    public GameObject canvas;
    [Range(0,20)]
    public float posX;
    [Range(0,20)]
    public float posY;
    [Range(0,360)]
    public float rot;

    string data = "/0/0/0/0/0/0/0/0/0/0/0/";
    public int id = 1;


    void Start(){

        if(skip_menu){  
            Invoke("start_without_menu",0.2f);  
        }  
    }



    void start_without_menu(){
        canvas.SetActive(!skip_menu);
        StartCoroutine(network.inst.createplayer(0,true, 1));  
        StartCoroutine(network.inst.network_position_send());
    }

   
    void Update(){

        if(Input.GetKeyDown(KeyCode.Q)){ 
            id = 1;
            print("send: "+" P/" + id + data);
            network.inst.receive_data("P/" + id + data);
            //StartCoroutine(simule_network_msg());     
        } 
        
        if(Input.GetKeyDown(KeyCode.W)){  
            id = 2;
            network.inst.receive_data("P/" + id + data);  
           // StartCoroutine(simule_network_msg());   
        } 
        
        
        if(Input.GetKeyDown(KeyCode.E)){  
            id = 3;
            network.inst.receive_data("P/" + id + data); 
            //StartCoroutine(simule_network_msg());    
        } 

    

        if(Input.GetKeyDown(KeyCode.F)){     
            StartCoroutine(floor_creation.inst.delete_floor(0f));
            network.inst.id_event = 1;   
        }      
    }



    IEnumerator simule_network_msg(){

        yield return new WaitForSeconds(1f);

        while(true){   
            //network.inst.receive_data("P/"+id+"/"+posX+"/"+posY+"/"+rot+"/0/0/0/0/2/0");
            yield return new WaitForSeconds(0.02f);
        }
    }

}
