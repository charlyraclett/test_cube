using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class ui_manager : MonoBehaviour{


    
    public EventSystem eventSystem;

    public static ui_manager inst;
    int[] point_players = new int[]{0,0,0,0};

    [Header("Boutons")]

    public GameObject button_mode_multi;
    public GameObject button_salon_multi;
    public GameObject button_back;
    public GameObject[] buttons_player;
    public GameObject buttons_players_container;
    public GameObject button_mode_game_container;
    public Animator buttons_type_cont;
    public GameObject prefab_button_game;
    public GameObject container_button_level;
    public GameObject button_retry;
    public GameObject button_confirm;


    public GameObject button_resume_game;
    public GameObject button_game_aventure;
    public GameObject button_level_one;
    public GameObject button_next_level;


    [Header("UI buttons level")]
    public Button[] buttons_level;



    [Header("Menus")]
    public GameObject canvas;
    public GameObject menu;
    public GameObject menu_in_game;
    public GameObject menu_game_over;
    public GameObject black_menu_gameover;
    public GameObject perfect_container;
    public GameObject great_shot_container;
    public CanvasGroup ui_in_game;
    public Animator anim_start_level;
    public GameObject logo_cube;
    
    public Animator flash_dead;

    public Text[] score_players;
    public Text text_menu;
    public Text text_network;
 
    public GameObject container_button_game_found;
  
    public int ui_position = 0;

    public GameObject menu_end_level;
    public Text pt_precison;
    public Text duration_level;
    public Text best_duration_level;

  
    public GameObject prefab_button_game_network;

    public GameObject[] choix_player;

    public GameObject light_container;

    public GameObject cont_ui_cam_vehicule;
    public int choix_type = 0;

    public int id_event = 0;
    bool player_active_multi;

    public int game_found_id;

    public Text text_vague_nbr;
    public Text text_level_nbr;
    public Animator buttonA_action;
    public TMP_Text button_action_text;

    public Animator black_panel_end_level;
    public Animator black_panel_menu;

    
    void Awake(){
        inst = this;
    }

    public void show_menu_mode_game(){

        text_menu.text = "";
        for(int i = 0; i < score_players.Length;i++){
            score_players[i].transform.parent.gameObject.SetActive(false);
        }
        menu_game_over.SetActive(false);
        menu_in_game.SetActive(false);
        menu_end_level.SetActive(false);
        buttons_players_container.SetActive(false);
        button_salon_multi.SetActive(false);
        buttons_type_cont.SetBool("show_type",false);
        button_back.SetActive(false);
        container_button_level.SetActive(false);
        text_vague_nbr.gameObject.GetComponentInParent<Animator>().SetBool("show_vague_last",false);
        menu.SetActive(true);
        cont_ui_cam_vehicule.SetActive(true);
        button_mode_game_container.SetActive(true);
        ui_position = 0;   
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
        player_manager.inst.myId = id;
        show_menu_selection_vehicule();   
    }


    public void click_button_type(int type){ 
        sound_manager.inst.sound_click();
        player_manager.inst.choix_type = type;
        show_menu_selection_level();
    }

    

    public void click_button_level(int id_level){ 
        game_manager.inst.id_level = id_level;
        sound_manager.inst.sound_click();
        menu.GetComponent<Animator>().SetTrigger("hide_menu");
        logo_cube.GetComponent<Animator>().SetTrigger("hide_logo");
        button_back.SetActive(false);
        container_button_level.SetActive(false);
        black_panel_menu.SetBool("black_panel",true);
        StartCoroutine(game_manager.inst.launch_game());

        if(player_active_multi){
           // network.inst.sendMessage("C/"+network.inst.myId+"/"+type);
        }else{
        }
        Invoke("disable_menu",4f);
    }

    public void disable_menu(){
        menu.SetActive(false);
          buttons_type_cont.SetBool("show_type",false);
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
         buttons_type_cont.SetBool("show_type",false);
        text_menu.text = "Choose Your Player";
        buttons_players_container.SetActive(true);
        button_back.SetActive(true);
        ui_position = 1;
    }

    public void show_menu_selection_vehicule(){
        text_menu.text = "Choose Your Vehicule";
        buttons_players_container.SetActive(false);
        container_button_level.SetActive(false);
        cont_ui_cam_vehicule.SetActive(true);
         buttons_type_cont.SetBool("show_type",true);
        button_back.SetActive(true);
        ui_position = 2;  
    }

    public void show_menu_selection_level(){ 
        print("show level container ");
        text_menu.text = "";
        cont_ui_cam_vehicule.SetActive(false);
        buttons_type_cont.SetBool("show_type",false);
        buttons_players_container.SetActive(false);
        button_mode_game_container.SetActive(false);
        container_button_level.SetActive(true);
        active_buttons_acces_levels();
        button_back.SetActive(true);
        ui_position = 3;    
    }
    

   

    public void back_button(){

        print("back");
        sound_manager.inst.sound_click_back();
        text_network.text = "";
        switch(ui_position){
            case 1  : show_menu_mode_game(); break;
            case 2  : show_menu_selection_player(); break;
            case 3  : show_menu_selection_vehicule(); break;
            case -1 : show_menu_mode_game(); break;
            case -2 : show_menu_salon_multi(); break;
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

    // trigger game_manager
    public IEnumerator game_finish(){
        yield return new WaitForSeconds(2f);
        StartCoroutine(ui_manager.inst.set_alpha_ui_game(0.5f,0f));
        menu_end_level.SetActive(true);
        menu_end_level.GetComponent<Animator>().SetBool("show_result_end_level",true);
        eventSystem.firstSelectedGameObject = button_next_level;
        eventSystem.SetSelectedGameObject(button_next_level);
        text_vague_nbr.gameObject.GetComponentInParent<Animator>().SetBool("show_vague_last",false);
        player_manager.inst.reset_all_player();
    }



    public void refresh_text_vague_nbr(string value){
        text_vague_nbr.gameObject.GetComponentInParent<Animator>().SetTrigger("show_vague");
        text_vague_nbr.text = value;
    }

    public void refresh_text_level_nbr(string value){
        text_level_nbr.text = value;
    }


    public void refresh_pt_precision(int value){ // not use
        pt_precison.text = value.ToString();
    }

    public void refresh_duration_level(string value, string best){
        duration_level.text = value;
        best_duration_level.text = best;
    }


    
    public void show_menu_paused(){
        Time.timeScale = 0.0f;
        eventSystem.firstSelectedGameObject = button_resume_game;
        eventSystem.SetSelectedGameObject(button_resume_game);
        menu_in_game.SetActive(true);
        sound_manager.inst.pause_all_sources();
    }


    public void click_button_quit(){
        print("button quit menu pause");
        eventSystem.firstSelectedGameObject = button_game_aventure;
        eventSystem.SetSelectedGameObject(button_game_aventure);
        game_manager.inst.reset_level();
    }

    public void click_button_back_to_game(){
        Time.timeScale = 1.0f;
        menu_in_game.SetActive(false);
        sound_manager.inst.play_all_sources();
    }

    public void click_button_next_level(){
        StartCoroutine(show_level_after_level_finished());
    }

    IEnumerator show_level_after_level_finished(){
        black_panel_end_level.SetTrigger("hide_screen");
        yield return new WaitForSeconds(1.2f);
        player_manager.inst.reset_all_player();
        level_manager.inst.delete_level();
        camera_manager.inst.reset_cameras();
        menu_in_game.SetActive(false);
        menu_end_level.SetActive(false);
        menu.SetActive(true);
        light_container.SetActive(true);
        show_menu_selection_level();
        sound_manager.inst.sound_music_menu(); 
        eventSystem.firstSelectedGameObject = button_level_one;
        eventSystem.SetSelectedGameObject(button_level_one);
    } 

    public void click_button_restart_gameover(){
        StartCoroutine(game_manager.inst.reset_and_restart());
    }

    public void click_button_quit_gameover(){
        game_manager.inst.reset_level();
    }



    public IEnumerator show_menu_gameover(){
        yield return new WaitForSeconds(2f);
        StartCoroutine(set_alpha_ui_game(1f, 0f));
        menu_game_over.SetActive(true);
        eventSystem.firstSelectedGameObject = button_retry;
        eventSystem.SetSelectedGameObject(button_retry);
    }




    public IEnumerator show_last_text_vague(bool value){
        yield return new WaitForSeconds(2f);
        sound_manager.inst.sound_last_vague();
        text_vague_nbr.gameObject.GetComponentInParent<Animator>().SetBool("show_vague_last",value);
    }

    public IEnumerator show_perfect_vague(){
        yield return new WaitForSeconds(0.1f);
        perfect_container.GetComponent<Animator>().SetTrigger("show_perfect");
        sound_manager.inst.sound_perfect_vague();
    }

    public void show_great_shot(){
        great_shot_container.GetComponent<Animator>().SetTrigger("show_perfect");
    }


   

    public void reset_ui_game(){ 
        menu_game_over.SetActive(false);
        text_vague_nbr.gameObject.GetComponentInParent<Animator>().SetBool("show_vague_last",false);
        for(int i = 0; i < score_players.Length;i++){
            score_players[i].transform.parent.gameObject.SetActive(false);
            score_players[i].text = "0";
        }
        ui_manager.inst.text_vague_nbr.text = "";
        set_alpha_ui_game(1f,1f);
    }




    public  IEnumerator set_alpha_ui_game(float duration, float target_alpha){

        float currentTime = 0f;
        float value =  ui_in_game.alpha;
        float targetValue = Mathf.Clamp(target_alpha, 0.0001f, 1);

        while (currentTime < duration){
            currentTime += Time.deltaTime;
            float new_alpha = Mathf.Lerp(value, targetValue, currentTime / duration);
            ui_in_game.alpha = new_alpha;
            yield return null;
        }
        yield break;
    }



    public void active_buttons_acces_levels(){
        int level_complete = PlayerPrefs.GetInt("level_complete");

        if(dev_script.inst.all_access_level){
            level_complete = 2; // 0,1,2     3 levels
        }

        print("unlock level "+ (level_complete+1));
        for (int i = 0; i <= level_complete; i++){
            buttons_level[i].interactable = true;
            buttons_level[i].transform.GetChild(0).gameObject.SetActive(false);
            buttons_level[i].transform.GetChild(1).gameObject.SetActive(true);
        }  
    }


    public void show_button_action(bool value){
        buttonA_action.SetBool("show_buttonA",value);
        if(value){
            buttonA_action.transform.GetComponent<button_follow_player>().start_follow();
        }else{
            buttonA_action.transform.GetComponent<button_follow_player>().stop_follow();
        }
        
    }


    public void flash_effect_dead(){
        flash_dead.SetTrigger("dead");
    }


   
    
}
