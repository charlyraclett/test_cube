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

    public bool host;
    int myId;
    public Transform player_one_network;
    public Transform player_two_network;
    string position_msg;


    public Transform base_player_one;
    public Transform base_player_two;


    public GameObject player_one;
    public GameObject player_two;

    GameObject player_check_if_alive;
   


    public static network inst;

    [HideInInspector] int bufferSize = 50;

    public const string distantHost = "167.71.58.214";
   
    Int32 port = 7779;

    public bool socket_ready = false;
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;


    public bool player_one_is_dead;
    public bool player_two_is_dead;

    public Text score_one;
    int point_one = 0;
    public Text score_two;
    int point_two = 0;



  


    void Awake(){

        if (inst == null){
            inst = this;
        }


        setupSocket();
        StartCoroutine(network_position_send());  
       
        Vector3 set_position_start_one = new Vector3(base_player_one.position.x,0.78f,base_player_one.position.z);
        GameObject one = Instantiate(player_one,set_position_start_one,base_player_one.rotation);
        player_one_network = one.GetComponent<Transform>();

        Vector3 set_position_start_two = new Vector3(base_player_two.position.x,0.78f,base_player_two.position.z);
        GameObject two = Instantiate(player_two,set_position_start_two,base_player_two.rotation);
        player_two_network = two.GetComponent<Transform>();

        if(host){
            myId = 1;
            one.GetComponent<controller_cube>().host = true;
            two.GetComponent<controller_cube>().host = false;
        }else{
            myId = 2;
            one.GetComponent<controller_cube>().host = false;
            two.GetComponent<controller_cube>().host = true;
        }

        StartCoroutine(check_player_one()); 
        StartCoroutine(check_player_two()); 
    }

    


    IEnumerator check_player_two(){

        while(!player_two_is_dead){
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        add_score_one();
        StartCoroutine(player_one_to_base());
       
        yield return new WaitForSeconds(2f);
        Vector3 set_position_start = new Vector3(base_player_two.position.x,0.78f,base_player_two.position.z);
        GameObject two = Instantiate(player_two,set_position_start,base_player_two.rotation);
        player_two_network = two.GetComponent<Transform>();
        if(host){
            two.GetComponent<controller_cube>().host = false;
        }else{
            two.GetComponent<controller_cube>().host = true;
        }
        player_two_is_dead = false;
        StartCoroutine(check_player_two());
    }

    
    IEnumerator check_player_one(){
        
        while(!player_one_is_dead){
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        add_score_two();
        StartCoroutine(player_two_to_base());
       
        yield return new WaitForSeconds(2f);
        Vector3 set_position_start = new Vector3(base_player_one.position.x,0.78f,base_player_one.position.z);
        GameObject one = Instantiate(player_one,set_position_start,base_player_one.rotation);
        player_one_network = one.GetComponent<Transform>();
        if(host){
            one.GetComponent<controller_cube>().host = true;
        }else{
            one.GetComponent<controller_cube>().host = false;
        }
        player_one_is_dead = false;
        StartCoroutine(check_player_one());
    }





    IEnumerator player_one_to_base(){
        Vector3 set_position_start = new Vector3(base_player_one.position.x,0.78f,base_player_one.position.z);
        while(Vector3.Distance(player_one_network.position, set_position_start) > 0.01f){
            player_one_network.position = Vector3.MoveTowards(player_one_network.position, set_position_start, 30 * Time.deltaTime);
            player_one_network.rotation = Quaternion.Slerp( player_one_network.rotation,base_player_one.rotation, 10 * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator player_two_to_base(){
        Vector3 set_position_start = new Vector3(base_player_two.position.x,0.78f,base_player_two.position.z);
        while(Vector3.Distance(player_two_network.position, set_position_start) > 0.01f){
           
            player_two_network.position = Vector3.MoveTowards(player_two_network.position, set_position_start, 30 * Time.deltaTime);
            player_two_network.rotation = Quaternion.Slerp( player_two_network.rotation,base_player_two.rotation, 10 * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }
    }







    IEnumerator network_position_send(){
        yield return new WaitForSeconds(2f);

        if(host){
            while(true){
                position_msg = myId+"/"+player_one_network.position.x+"/"+player_one_network.position.z+"/"+player_one_network.eulerAngles.y+"/";
                sendMessage(position_msg);
                yield return new WaitForSeconds(0.02f);
            }
        }

        else if(!host){
            while(true){
                position_msg = myId+"/"+player_two_network.position.x+"/"+player_two_network.position.z+"/"+player_two_network.eulerAngles.y+"/";
                sendMessage(position_msg);
                yield return new WaitForSeconds(0.02f);
            }
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

        player_two_network.position = new Vector3(posX,0.78f,posZ);
        player_two_network.eulerAngles = new Vector3(player_two_network.eulerAngles.x, rot, player_two_network.eulerAngles.z);
        
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






    void add_score_one(){
        point_one++;
        score_one.text = ""+ point_one;
    }


    void add_score_two(){
        point_two++;
        score_two.text = ""+ point_two;   
    }








   








}
