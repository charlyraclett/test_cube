using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;




public class network : MonoBehaviour{


    public static network inst;

    [HideInInspector] public int bufferSize;

    public string distantHost = "167.71.58.214";
    public bool isLocalhost = true;

    public Int32 port = 7778;

    public bool socket_ready = false;
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;

    [HideInInspector] public int serverTimeOut = 0;
    public float sending_rate = 0.02f;
    public string player_game_id;







    void Awake(){

        if (inst == null){
            inst = this;
        }
       // bufferSize = network_player.network_message_size_constructor.Sum();
        Debug.Log("bufferSize : " + bufferSize);
    }




    

    public IEnumerator testReceivedSocket(){
        if(socket_ready){
            byte[] received_data = readSocket();
            if(received_data != null){
                string dataBrut = System.Text.Encoding.UTF8.GetString(received_data);
                receive_data(dataBrut);
            }
        }
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(testReceivedSocket());
    }






    public void setupSocket(bool isTest = false){  
        try
        {
            tcp_socket = new TcpClient((isLocalhost ? "localhost" : distantHost), port);
            net_stream = tcp_socket.GetStream();
            socket_ready = true;
            if(!isTest){
               // StartCoroutine(receiveData()); 
            }
        }
        catch (Exception e)
        {
            Debug.Log("could not setup socket : " + e.Message);
            StopAllCoroutines();
        }
    }



    public void receive_data(string value){

		string[] values_enemies = value.Split('/');
        string  command = values_enemies[0];
		int _id = int.Parse(values_enemies[1]);
        int _pv = int.Parse(values_enemies[2]);   

        if(command == "dead"){
            
        }
    }









    public void sendMessage(string message){
       
        try{
            //Debug.Log("send " + message.Length);
           // message = network_utilities.inst.fill_with_sharps(message, bufferSize);
            net_stream.Write(System.Text.Encoding.UTF8.GetBytes(message), 0, bufferSize);
        }catch(Exception e){
            Debug.Log("ERROR in sendMessage() : " + e.Message + "\n" + message);
        }
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







    public void quit_network(){ 
            try{
                net_stream.Close();
                tcp_socket.Close();
            }catch(Exception e){
                Debug.Log("erreur in quit_network : " + e.Message );
            }
            StopAllCoroutines();  
    }
    









}
