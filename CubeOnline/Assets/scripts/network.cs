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

    [Header("Choose Player")]
    public Player _player;
    public enum Player {
        player1,
        player2,
        player3,
        player4
    };


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
    public GameObject avatar_prefab;
    public Transform[] bases_pos;
    public Material[] color;
    public Text[] score_players;
    public GameObject menu;
  





  


    void Awake(){

        if (inst == null){
            inst = this;
        }

        setupSocket(); 
      
        // switch (_player){
        //     case Player.player1: set_player(0); break;
        //     case Player.player2: set_player(1); break;
        //     case Player.player3: set_player(2); break;
        //     case Player.player4: set_player(3); break;
        // }
      
    }





    IEnumerator network_position_send(float delay){

        yield return new WaitForSeconds(delay); 
      
        while(my_avatar.is_dead == 0){
            position_msg = myId +"/"+avatars_pos[myId].position.x +"/"+avatars_pos[myId].position.z +"/"+ avatars_pos[myId].eulerAngles.y+"/"+my_avatar.is_shooting+"/"+ my_avatar.is_dead+"/";
            sendMessage(position_msg);
            yield return new WaitForSeconds(0.02f);
        } 
        sendMessage(myId+"/0/0/0/0/1/");
        avatars_pos[myId] = null;
        yield return new WaitForSeconds(2f); 
        set_player(myId);
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
      
      
        foreach(Transform avatars in avatars_pos){

            if(avatars_pos[id_player] == null && id_player != myId){ // ajout player si inexistant
                set_other_player(id_player);
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


    void set_player(int id){
        GameObject cube = Instantiate(avatar_prefab,bases_pos[id].position,bases_pos[id].rotation);
        avatars_pos[id] = cube.GetComponent<Transform>();
        my_avatar = cube.GetComponent<controller_cube>();
        my_avatar.host = true;
        my_avatar.id_avatar = id;
        cube.GetComponent<MeshRenderer>().material = color[id];
        myId = id;
    }

    void set_other_player(int id){
        print("add player :"+ id);
        GameObject cube = Instantiate(avatar_prefab,bases_pos[id].position,bases_pos[id].rotation);
        avatars_pos[id] = cube.GetComponent<Transform>();
        cube.GetComponent<controller_cube>().id_avatar = id;
        cube.GetComponent<MeshRenderer>().material = color[id];
    }



    public void add_score_for(int id_player){

        point_players[id_player]++;
        score_players[id_player].text = "" + point_players[id_player];
    }


    public void _button1(){ 
        menu.SetActive(false);
        set_player(0);
        StartCoroutine(network_position_send(1f));
    }

    public void _button2(){
        menu.SetActive(false);
        set_player(1);
        StartCoroutine(network_position_send(1f));

    }

    public void _button3(){ 
        menu.SetActive(false);
        set_player(2);
        StartCoroutine(network_position_send(1f));

    }

    public void _button4(){  
        menu.SetActive(false);
        set_player(3);
        StartCoroutine(network_position_send(1f));
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
