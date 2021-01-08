using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dev_script : MonoBehaviour{
    
    public bool skip_menu;
    public GameObject canvas;
    string data = "/0/0/0/0/0/0/0/";
    int inc = 1;

     
        // int id_player = int.Parse(position_receive[0]);
		// float posX = float.Parse(position_receive[1]);
        // float posZ = float.Parse(position_receive[2]); 
        // float rot = float.Parse(position_receive[3]); 
        // int shoot = int.Parse(position_receive[4]); 
        // int dead = int.Parse(position_receive[5]); 
        // int score = int.Parse(position_receive[6]); 
        // int type = int.Parse(position_receive[7]); 

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
            inc = 1; 
            network.inst.receive_data(inc+data+ network.inst.id_event);   
        } 
        
        if(Input.GetKeyDown(KeyCode.W)){  
            inc = 2;
            network.inst.receive_data(inc+data+ network.inst.id_event);   
        } 
        
        
        if(Input.GetKeyDown(KeyCode.E)){  
            inc = 3;
            network.inst.receive_data(inc+data+ network.inst.id_event);   
        } 

        if(Input.GetKeyDown(KeyCode.F)){  
            
            StartCoroutine(floor_creation.inst.delete_floor(0f));
            network.inst.id_event = 1;   
        } 
        
           
    }








}
