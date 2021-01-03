using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;




public class network : MonoBehaviour{

    public int myId;
    public Transform player_main;
    public Transform second_player;
    public Vector3 offset;
    public string position_msg;


    public static network inst;

    [HideInInspector] int bufferSize = 50;

    public const string distantHost = "167.71.58.214";
   
    Int32 port = 7779;

    public bool socket_ready = false;
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;

  


    void Awake(){

        if (inst == null){
            inst = this;
        }

        setupSocket();
        StartCoroutine(network_position_send());  
    }



    IEnumerator network_position_send(){
        yield return new WaitForSeconds(2f);

        while(true){
        position_msg = myId+"/"+player_main.position.x+"/"+player_main.position.z+"/"+player_main.eulerAngles.y+"/";
        sendMessage(position_msg);

        yield return new WaitForSeconds(0.02f);
        }
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



    public void receive_data(string value){

        
		string[] position_receive = value.Split('/');
        int id_player = int.Parse(position_receive[0]);
		float posX = float.Parse(position_receive[1]);
        float posZ = float.Parse(position_receive[2]); 
        float rot = float.Parse(position_receive[3]); 

        if(id_player != myId){

            second_player.position = new Vector3(posX,0.78f,posZ) + offset;
            second_player.eulerAngles = new Vector3(second_player.eulerAngles.x, rot, second_player.eulerAngles.z);
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
