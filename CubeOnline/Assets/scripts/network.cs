using UnityEngine;
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

    public bool test_online;

    [Header("Info")]
    public int myId;
    string position_msg;
    public Transform[] avatars_pos;
    public const string distantHost = "167.71.58.214";
    public controller_cube my_avatar;


    [HideInInspector] int bufferSize = 50;
    Int32 port = 7779;
    public bool socket_ready = false;
    TcpClient tcp_socket;
    NetworkStream net_stream;
    StreamWriter socket_writer;
    StreamReader socket_reader;

    int[] point_players = new int[]{0,0,0,0};

    [Header("Edition")]
    public GameObject[] avatar_prefab;
    public Transform[] bases_pos;
    public Material[] color;
    public Text[] score_players;
    public GameObject menu;
    public Text text_menu;
    public GameObject button_container;
    public GameObject[] buttons;
    public Transform cam;
    public Transform pos_cam;
    public GameObject canvas;

    public GameObject buttons_type_cont;
    public int choix_type = 0;

    public floor_creation _floor_sc;
  


    void Awake(){

        if (inst == null){
            inst = this;
        }
        if(test_online){
            text_menu.text = "Connection...";
            button_container.SetActive(false);
            setupSocket(); 
        }
        canvas.SetActive(true);
    }

    
    


    IEnumerator network_position_send(float delay){

        yield return new WaitForSeconds(delay); 
      
        while(my_avatar.is_dead == 0){
            position_msg = myId +"/"+avatars_pos[myId].position.x +"/"+avatars_pos[myId].position.z +"/"+ avatars_pos[myId].eulerAngles.y+"/"+my_avatar.is_shooting+"/"+ my_avatar.is_dead+"/"+ point_players[myId]+"/"+choix_type;
            sendMessage(position_msg);
            yield return new WaitForSeconds(0.02f);
        } 
        sendMessage(myId+"/0/0/0/0/1/"+ point_players[myId]);
        avatars_pos[myId] = null;
        yield return new WaitForSeconds(2f); 
        //set_player(myId);
        StartCoroutine(createplayer(myId,true, choix_type));
        StartCoroutine(network_position_send(0f)); 
    }




    public void receive_data(string value){

		string[] position_receive = value.Split('/');
       
        int id_player = int.Parse(position_receive[0]);
		float posX = float.Parse(position_receive[1]);
        float posZ = float.Parse(position_receive[2]); 
        float rot = float.Parse(position_receive[3]); 
        int shoot = int.Parse(position_receive[4]); 
        int dead = int.Parse(position_receive[5]); 
        int score = int.Parse(position_receive[6]); 
        int type = int.Parse(position_receive[7]); 
      
        foreach(Transform avatars in avatars_pos){

            if(avatars_pos[id_player] == null && id_player != myId){ // ajout player si inexistant
                StartCoroutine(createplayer(id_player,false,type));
            }

            if(id_player != myId){
                controller_cube avatar_controller = avatars_pos[id_player].GetComponent<controller_cube>();
               
                if(dead == 1){
                    avatar_controller.controller_dead();
                    avatars_pos[id_player] = null;
                    return;
                }
                avatars_pos[id_player].position = new Vector3(posX,0.8f,posZ);
                avatars_pos[id_player].eulerAngles = new Vector3(avatars_pos[id_player].eulerAngles.x, rot, avatars_pos[id_player].eulerAngles.z);
                if(shoot == 1){
                    StartCoroutine(avatar_controller.shoot());
                }
               
            }
        }     
    }


    // void set_player(int id){
    //     Vector3 pos_base = new Vector3(bases_pos[id].position.x,1f,bases_pos[id].position.z);
    //     GameObject cube = Instantiate(avatar_prefab[choix_type],pos_base,bases_pos[id].rotation);
    //     avatars_pos[id] = cube.GetComponent<Transform>();
    //     cube.GetComponent<MeshRenderer>().material = color[id];


    //     my_avatar = cube.GetComponent<controller_cube>();
    //     my_avatar.host = true;
    //     my_avatar.id_avatar = id;
    //     //sendMessage(myId.ToString());
    // }

    // void set_other_player(int id, int type){
    
    //     Vector3 pos_base = new Vector3(bases_pos[id].position.x,1f,bases_pos[id].position.z);
    //     GameObject cube = Instantiate(avatar_prefab[type],pos_base,bases_pos[id].rotation);
    //     avatars_pos[id] = cube.GetComponent<Transform>();
    //     cube.GetComponent<MeshRenderer>().material = color[id];


    //     cube.GetComponent<controller_cube>().id_avatar = id;
    // }



    public void add_score_for(int id_player){
        point_players[id_player]++;
        score_players[id_player].text = "" + point_players[id_player];
    }


    
    public void click_button(int id){
        sound_manager.inst.sound_click();
        myId = id;
        button_container.SetActive(false);
        text_menu.text = "Choose Your Vehicule";
        buttons_type_cont.SetActive(true);
    }

    public void click_button_type(int type){
        sound_manager.inst.sound_click();
        choix_type = type;
        menu.SetActive(false);
        buttons_type_cont.SetActive(false);
      
        StartCoroutine(createplayer(myId,true,type));
        StartCoroutine(network_position_send(1f));
        StartCoroutine(_floor_sc.delete_floor(5f));
    }
    


    void show_menu_selection_player(){
        text_menu.text = "Choose Your Player";
        button_container.SetActive(true);
    }




     
    IEnumerator createplayer(int id,bool ismine, int type){

        float elapsed = 0.0f;
        float duree = 0.8f;
        float angle = 0;

        switch(id){
            case 0 : angle = 90; break;
            case 1 : angle = -90; break;
            case 2 : angle = 0; break;
            case 3 : angle = 180; break;
        }

        var start = Quaternion.Euler (0, angle, 180);
        var target = Quaternion.Euler (180, angle, 180);

        Vector3 pos_base = new Vector3( network.inst.bases_pos[id].position.x,1f, bases_pos[id].position.z);
        GameObject cube = Instantiate( avatar_prefab[type],pos_base, bases_pos[id].rotation);
        avatars_pos[id] = cube.GetComponent<Transform>();
        cube.GetComponent<MeshRenderer>().material = color[id];

        if(ismine){
            my_avatar = cube.GetComponent<controller_cube>();
            my_avatar.id_avatar = id;
        }
        
        cube.transform.parent = bases_pos[id];
        sound_manager.inst.sound_friction();

        while( elapsed < duree ){
            bases_pos[id].localRotation = Quaternion.Slerp(start, target,elapsed / duree);
            elapsed += Time.deltaTime;
            yield return null;
        } 

        cube.transform.parent = null;
        cube.GetComponent<Rigidbody>().useGravity = true;
        cube.GetComponent<Rigidbody>().isKinematic = false;
        my_avatar.host = ismine;

    }












    public IEnumerator testReceivedSocket(){

        if(socket_ready){
            byte[] received_data = readSocket();
            if(received_data != null){
                string dataBrut = System.Text.Encoding.UTF8.GetString(received_data);

                if(!string.IsNullOrEmpty(dataBrut)){
                    receive_data(dataBrut);
                }

            }
        }
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(testReceivedSocket());
    }



    public void setupSocket(bool isTest = false){  
        try
        {
            tcp_socket = new TcpClient(distantHost, port);
            net_stream = tcp_socket.GetStream();
            socket_ready = true;
            StartCoroutine(testReceivedSocket()); 
            show_menu_selection_player();   
        }
        catch (Exception e)
        {
            Debug.Log("could not setup socket : " + e.Message);
            StopAllCoroutines();
        }
    }


    public void sendMessage(string message){
       
        try{
            message = fill_with_sharps(message, bufferSize);
           // Debug.Log("send " + message);
            net_stream.Write(System.Text.Encoding.UTF8.GetBytes(message), 0, bufferSize);
        }catch(Exception e){
            //Debug.Log("ERROR in sendMessage() : " + e.Message + "\n" + message);
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
