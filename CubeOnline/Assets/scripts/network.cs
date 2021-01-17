﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;


public class network : MonoBehaviour{

    public static network inst;

    public bool localhost;

    [Header("Info")]
    public int myId;
    string position_msg;
    public Transform[] avatars_pos;
    public string distantHost = "167.71.58.214";
    public controller_cube my_avatar;

    int bufferSize = 50;
    Int32 port = 7778;
    bool socket_ready = false;
    TcpClient tcp_socket;
    NetworkStream net_stream;
    StreamWriter socket_writer;
    StreamReader socket_reader;

    int[] point_players = new int[]{0,0,0,0};

    [Header("Edition")]
    public GameObject[] avatar_prefab;
    public Transform[] bases_pos;
    public Material[] color;
   
    public int choix_type = 0;
    public int id_event = 0;
    [HideInInspector] public bool player_active_multi;

    
    void Awake(){
        if (inst == null){
            inst = this;
        }
        distantHost = localhost ? "localhost" : distantHost;
    }

    
    
    public IEnumerator network_position_send(){
        position_msg = 
        "P/" 
        + myId +"/"
        + avatars_pos[myId].position.x + "/"
        + avatars_pos[myId].position.y + "/"
        + avatars_pos[myId].position.z + "/"
        + avatars_pos[myId].localRotation.eulerAngles.x + "/"
        + avatars_pos[myId].localRotation.eulerAngles.y + "/"
        + avatars_pos[myId].localRotation.eulerAngles.z + "/"
        + my_avatar.is_shooting + "/"
        + my_avatar.is_dead + "/"
        + point_players[myId] + "/"
        + choix_type + "/"
        + id_event + "/";
        sendMessage(position_msg);
        yield return new WaitForSeconds(0.02f); 
        network_position_send();  
    }



    // trigger controller_cube
    public void player_dead(){
        if(player_active_multi){
            sendMessage("P/"+ myId +"/");
        }
        avatars_pos[myId] = null;
        Invoke("new_my_player",2f);
    }

    void new_my_player(){
        StartCoroutine(createplayer(myId,true, choix_type));
    }





    public void receive_data(string value){

		string[] position_receive = value.Split('/');
        print("recu : "+value);

        string command = position_receive[0];

        if(command == "G"){
            int nbr = int.Parse(position_receive[1]);
            if(nbr == 0){
                ui_manager.inst.text_network.text = "No game found";
            }
            else{
               ui_manager.inst.show_games_in_network(nbr);
            }
            return;
        }

        int id_player = int.Parse(position_receive[1]);
        float posX = float.Parse(position_receive[2]);
        float posY = float.Parse(position_receive[3]); 
        float posZ = float.Parse(position_receive[4]); 
        float rotX = float.Parse(position_receive[5]); 
        float rotY = float.Parse(position_receive[6]); 
        float rotZ = float.Parse(position_receive[7]); 
        int shoot = int.Parse(position_receive[8]); 
        int dead = int.Parse(position_receive[9]); 
        int score = int.Parse(position_receive[10]); 
        int type = int.Parse(position_receive[11]); 
        int id_event = int.Parse(position_receive[12]); 


        if(id_player != myId){
            foreach(Transform avatars in avatars_pos){

                if(avatars_pos[id_player] == null){ // ajout player si inexistant
                    create_avatar(id_player,type);
                    return;  
                }
                    
                    if(id_player != myId){

                        controller_cube avatar_controller = avatars_pos[id_player].GetComponent<controller_cube>();
                        if(dead == 1){
                            avatar_controller.controller_dead();
                            avatars_pos[id_player] = null;
                            ui_manager.inst.choix_player[id_player].SetActive(true); 
                            return;
                        }
                        if(avatar_controller.is_moving){
                            avatars_pos[id_player].position = new Vector3(posX,posY,posZ);
                            avatars_pos[id_player].localRotation = Quaternion.Euler(rotX, rotY, rotZ);
                        }
                        if(shoot == 1){
                            StartCoroutine(avatar_controller.shoot());
                        }
                        ui_manager.inst.score_players[id_player].text  = score.ToString(); 
                    }
            }  

            if(id_event > 0){
                switch(id_event){
                    case 1 :  StartCoroutine(floor_creation.inst.delete_floor(0f)); break;
                }
            } 
        } 
         
    }




    public void create_avatar(int id_player,int type){
        StartCoroutine(createplayer(id_player,false,type));
        ui_manager.inst.choix_player[id_player].SetActive(false);
    }



  



    public IEnumerator createplayer(int id,bool ismine, int type){

        float elapsed = 0.0f;
        float duree = 0.8f; 
        float angle = 0;

        switch(id){
            case 0 : angle = 90; break;
            case 1 : angle = 270; break;
            case 2 : angle = 0; break;
            case 3 : angle = 180; break;
        }

        Vector3 pos_base = new Vector3(bases_pos[id].position.x, 1f, bases_pos[id].position.z);
        GameObject cube = Instantiate(avatar_prefab[type],pos_base, bases_pos[id].rotation);
        avatars_pos[id] = cube.GetComponent<Transform>();
        cube.GetComponent<MeshRenderer>().material = color[id];
        cube.GetComponent<controller_cube>().id_avatar = id;
        sound_manager.inst.sound_friction();
        ui_manager.inst.score_players[id].transform.parent.gameObject.SetActive(true);


       cube.transform.parent = bases_pos[id].transform;

        while( elapsed < duree ){
            bases_pos[id].localRotation = Quaternion.Slerp(Quaternion.Euler(0, angle, 180), Quaternion.Euler(180, angle, 180), elapsed / duree);
            elapsed += Time.deltaTime;
            yield return null;
        } 

        bases_pos[id].localRotation = Quaternion.Euler(180, angle, 180);
        cube.transform.parent = null;
      
       
        if(ismine){
            myId = id;  
            yield return new WaitForSeconds(0.1f);
            my_avatar = cube.GetComponent<controller_cube>();
            cube.GetComponent<Rigidbody>().useGravity = true;
            cube.GetComponent<Rigidbody>().isKinematic = false;
            my_avatar.host = ismine;
            if(player_active_multi)
            StartCoroutine("network_position_send"); 
        }
        cube.GetComponent<controller_cube>().is_moving = true;
        yield return null;
    }



















    public IEnumerator start_listen_network(){
           
        if(socket_ready){
            byte[] received_data = readSocket();
            if(received_data != null){
                string dataBrut = System.Text.Encoding.UTF8.GetString(received_data);
                if(!string.IsNullOrEmpty(dataBrut)){
                    receive_data(dataBrut);
                }
            }
        }
        yield return null;
        StartCoroutine(start_listen_network());
    }




    public void setupSocket(bool isTest = false){  
        try{
            tcp_socket = new TcpClient(distantHost, port);
            net_stream = tcp_socket.GetStream();
            socket_ready = true;
            ui_manager.inst.server_is_online(); 
        }
        catch (Exception e){
            Debug.Log("could not setup socket : " + e.Message);
            ui_manager.inst.no_server_found();
            StopAllCoroutines();
        }
    }





    public void sendMessage(string message){
        try{
            message = fill_with_sharps(message, bufferSize);
           // Debug.Log("send " + message);
            net_stream.Write(System.Text.Encoding.UTF8.GetBytes(message), 0, bufferSize);
        }catch(Exception e){
            Debug.Log("ERROR in sendMessage() : " + e.Message + "\n" + message);
        }
    }



    public string fill_with_sharps(string initial_string, int max_number){
        for (int i = initial_string.Length; i < max_number; i++){
            initial_string += "#";
        }
        return initial_string;
    }




    public byte[] readSocket(){
       
        if (net_stream.DataAvailable)
        {
            byte[] tempBuffer = new byte[bufferSize];
            int tempOffset = 0;
            int tempSize = tempBuffer.Length;

            try
            {
                net_stream.Read(tempBuffer, tempOffset, tempSize);
            }
            catch{}

            return tempBuffer;
        }
        return null;
    }


}
