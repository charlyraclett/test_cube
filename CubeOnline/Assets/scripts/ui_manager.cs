using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;


public class ui_manager : MonoBehaviour{

    public static ui_manager inst;

    int[] point_players = new int[]{0,0,0,0};

    [Header("Edition")]
   
    public Text[] score_players;
    public GameObject menu;
    public Text text_menu;
    public Text text_network;
    public GameObject buttons_players_container;
    public GameObject button_mode_game_container;
    public GameObject container_button_game_found;
    public GameObject prefab_button_game;
    public GameObject button_mode_multi;
    public GameObject button_salon_multi;
    public GameObject button_back;
    public GameObject[] buttons_player;
    public int ui_position = 0;
   
    public GameObject canvas;
    public GameObject button_exit;
    public GameObject prefab_button_game_network;

    public GameObject[] choix_player;

    public GameObject buttons_type_cont;
    public GameObject cont_ui_cam_vehicule;
    public int choix_type = 0;

    public int id_event = 0;
    bool player_active_multi;

    public int game_found_id;

    
    void Awake(){

        if (inst == null){
            inst = this;
        }
        canvas.SetActive(true);
        show_menu_mode_game();
        button_exit.SetActive(false);
        for(int i = 0; i < score_players.Length;i++){
            score_players[i].transform.parent.gameObject.SetActive(false);
        }
    }

    void Update(){

        if(Input.GetKeyDown(KeyCode.U)){  
           show_menu_salon_multi();    
           show_games_in_network(4);
        }  
    }




    public void click_button_solo(){
        sound_manager.inst.sound_click();
        player_active_multi = false;
        show_menu_selection_player(); 
        ui_position = 1; 
    }

    public void click_button_multi(){
        sound_manager.inst.sound_click();
        player_active_multi = true;
        button_mode_game_container.SetActive(false);
        text_menu.text = "Connexion...";
        Invoke("connect_to_server",0.2f);
        ui_position = 1;  
    }

    public void connect_to_server(){
        network.inst.setupSocket();  
        print("setupSocket");  
    }



    public void click_create_game(){  // network
        sound_manager.inst.sound_click();
        text_network.text = "";
        network.inst.sendMessage("C/pseudo_game/");
        print("send to server : C/pseudo_game/");  
        show_menu_selection_player();
       // StartCoroutine(testReceivedSocket()); 
    }

    public void click_join_game(){ //network
        sound_manager.inst.sound_click();
        StartCoroutine(network.inst.start_listen_network());
        network.inst.sendMessage("G/"); 
        print("send to server : G/");   
    }


    public void click_button_player(int id){
        sound_manager.inst.sound_click();
        network.inst.myId = id;
        buttons_players_container.SetActive(false);
        text_menu.text = "Choose Your Vehicule";
        buttons_type_cont.SetActive(true);
        ui_position = 2;
    }
    

    public void click_button_type(int type){ // start game
        if(player_active_multi){
           // network.inst.sendMessage("C/"+network.inst.myId+"/"+type);
        }
        sound_manager.inst.sound_click();
        sound_manager.inst.sound_music_battle();
        network.inst.choix_type = type;
        menu.SetActive(false);
        buttons_type_cont.SetActive(false);
        button_exit.SetActive(true);
        button_back.SetActive(false);
        cont_ui_cam_vehicule.SetActive(false);
        StartCoroutine(network.inst.createplayer(network.inst.myId,true,type));
        StartCoroutine(camera_manager.inst.switch_cam_game());
        camera_manager.inst.target_cam_player = network.inst.avatars_pos[network.inst.myId];

    }

    


    public void add_score_for(int id_player){
        point_players[id_player]++;
        score_players[id_player].GetComponent<Animator>().SetTrigger("win");
        Invoke("play_sound_win",0.4f);
    }

    public void play_sound_win(){
        sound_manager.inst.sound_win_point();
        score_players[network.inst.myId].text = "" + point_players[ network.inst.myId];
    }



    public void show_menu_mode_game(){
        text_menu.text = "";
        button_mode_game_container.SetActive(true);
        buttons_players_container.SetActive(false);
        button_salon_multi.SetActive(false);
        buttons_type_cont.SetActive(false);
        button_back.SetActive(false);
        ui_position = 0;   
    }

  
    public void show_menu_salon_multi(){
        button_back.SetActive(true);
        button_salon_multi.SetActive(true);
        button_mode_game_container.SetActive(false);
        ui_position = -1;

        if(container_button_game_found.transform.childCount > 0){
            foreach (Transform child in container_button_game_found.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }


    public void show_menu_selection_player(){
        button_mode_game_container.SetActive(false);
        container_button_game_found.SetActive(false);
        text_network.text = "";
        button_salon_multi.SetActive(false);
        buttons_type_cont.SetActive(false);
        text_menu.text = "Choose Your Player";
        buttons_players_container.SetActive(true);
        button_back.SetActive(true);
        ui_position = 1;
    }

    public void show_menu_selection_vehicule(){
        button_exit.SetActive(false);
        text_menu.text = "Choose Your Vehicule";
        buttons_players_container.SetActive(false);
        cont_ui_cam_vehicule.SetActive(true);
        buttons_type_cont.SetActive(true);
        button_back.SetActive(true);  
    }

   

    public void back_button(){

        sound_manager.inst.sound_click_back();
        text_network.text = "";
        switch(ui_position){
            case 1  : show_menu_mode_game(); break;
            case 2  : show_menu_selection_player(); break;
            case -1 : show_menu_mode_game(); break;
            case -2 : show_menu_salon_multi(); break;
        }  
    }


    public void exit_button(){
        sound_manager.inst.sound_music_menu();
        show_menu_selection_vehicule();
        menu.SetActive(true);
     
        if(network.inst.my_avatar != null){
            network.inst.my_avatar = null;
            Destroy(network.inst.avatars_pos[network.inst.myId].gameObject,0f);
            if(network.inst.player_active_multi)
            network.inst.sendMessage("D/"+network.inst.myId+"/0/0/0/0/0/0/0/0/");
        }
    }


    
    // trigger network
    public void server_is_online(){
        show_menu_salon_multi(); 
        sound_manager.inst.sound_server_ok();
        text_menu.text = ""; 
    }

    // trigger network
    public void no_server_found(){
        text_menu.text = "No server found.";
        sound_manager.inst.sound_server_error();
        button_back.SetActive(true);   
    }

    // trigger network game found
    public void show_games_in_network(int id_game){
        container_button_game_found.SetActive(true);
        ui_position = -2;
        button_salon_multi.SetActive(false);
        ui_manager.inst.text_menu.text = "";
        ui_manager.inst.text_network.text = "";
        StartCoroutine(create_button(id_game));
    }

    IEnumerator create_button(int id_game){// creation bouton pour rejoindre partie
        GameObject button_game = Instantiate(prefab_button_game_network);
        button_game.SetActive(true);
        sound_manager.inst.sound_game_found();
        Button myButton = button_game.GetComponentInChildren<Button>();
        myButton.GetComponentInChildren<Text>().text = "Game "+ (id_game+1);
        myButton.GetComponentInChildren<button_game_found>().id_button = id_game;
        button_game.transform.parent = container_button_game_found.transform;
        yield return new WaitForSeconds(0.3f);   
    }



    public void click_button_game_found(int id){ 
        network.inst.sendMessage("J/"+ id + "/");  
        print("send to server : J/"+ id + "/");  
        show_menu_selection_player();
    }



    
    public void hover_button(){
        sound_manager.inst.sound_hover();  
    }

    public void hover_back_quit_lobby(){
        if(ui_position == -2){
           text_network.text = "Quit Lobby ?";
        }
    }

    public void exit_hover_back(){
        if(ui_position == -2){ 
            text_network.text = "";
        }
    }


    
   


     
   


}
