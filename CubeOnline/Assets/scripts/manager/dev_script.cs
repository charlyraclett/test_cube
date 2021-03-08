using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class dev_script : MonoBehaviour{

    public static dev_script inst;

    
    public bool skip_menu;
    public bool no_enemy;
    public bool unlimit_life;
    public bool invincible;
    public bool all_access_level;

    

    public choix_level _level;
    public enum choix_level {
        level1,
        level2, 
        level3
    };
   
    public vague start_vague;
    public enum vague {
        I,
        II,
        III,
        IV,
        V,
        the_last,
        end_Level
    };

    public player player_color;
    public enum player {
        green,
        blue,
        red,
        purple
    };

    public choix_vehicule _type;
    public enum choix_vehicule {
        classic,
        catapult,
        canon   
    };

    [TextArea(10,20)]
    public string Shortcuts;
   
    string data = "/0/0/0/0/0/0/0/0/0/0/0/";
    int id = 1;
    int type_vehicule;
    public int start_at;
    int level_id;

    public GameObject menu;
    public GameObject[] level;
  




    void Awake(){
        inst = this;
        set_player();
        set_vehicule();
        set_vague();
        if(skip_menu){  
            StartCoroutine(start_without_menu());
        }else{
            menu.SetActive(true);
            desactive_all_level_test();
        }
        if(unlimit_life){
            player_manager.inst.life_player = 1000;
        }
        level_manager.inst.id_vague = start_at;
    }

    
    IEnumerator start_without_menu(){
        yield return new WaitForSeconds(0.1f); 
        menu.SetActive(false);
        ui_manager.inst.light_container.SetActive(false);
        set_level();
        yield return new WaitForSeconds(1f); 
        StartCoroutine(player_manager.inst.create_player(player_manager.inst.myId,true, type_vehicule,0f));
        yield return new WaitForSeconds(0.2f); 
        StartCoroutine(camera_manager.inst.switch_cam_game());
        yield return new WaitForSeconds(1f);
        if(!no_enemy){
            StartCoroutine(level_manager.inst.change_vague(start_at));
        }
        sound_manager.inst.sound_music_level(game_manager.inst.id_level);

    }

   
    void Update(){

        // if(Input.GetKeyDown(KeyCode.Q)){ 
        //     id = 1;
        //     print("send: "+" P/" + id + data);
        //     network.inst.receive_data("P/" + id + data);      
        // } 
        
        // if(Input.GetKeyDown(KeyCode.W)){  
        //     id = 2;
        //     network.inst.receive_data("P/" + id + data);   
        // } 
          
        // if(Input.GetKeyDown(KeyCode.E)){  
        //     id = 3;
        //     network.inst.receive_data("P/" + id + data); 
        // } 

        // if(Input.GetKeyDown(KeyCode.P)){  
        //     StartCoroutine(enemies_manager.inst.create_enemy_floor(0,2,false, 58));
        // }
  
        if(Input.GetKeyDown(KeyCode.V)){  
            StartCoroutine(level_manager.inst.change_vague(6));
        }

        if(Input.GetKeyDown(KeyCode.G)){    
            player_manager.inst.life_player = 1; 
            invincible = false;
            player_manager.inst.my_avatar.controller_dead();
            StartCoroutine(game_manager.inst.game_over());
        } 

        if(Input.GetKeyDown(KeyCode.R)){    
            StartCoroutine(consommable_manager.inst.create_box(0.1f,1));   

        } 

        if(Input.GetKeyDown(KeyCode.T)){    
            StartCoroutine(consommable_manager.inst.create_box(0.1f,0));   

        }  

        if(Input.GetKeyDown(KeyCode.Y)){    
            player_manager.inst.life_player--;
            player_manager.inst.pv_player_ui.SetInteger("pv_player", player_manager.inst.life_player);
        }                  
    }


    void killed_all_enemy(){
        foreach(GameObject enemy in enemies_manager.inst.enemy_in_game){
            enemy.GetComponent<navmesh_agent>().StopAllCoroutines();
        }
    }



    IEnumerator simule_network_msg(){
        yield return new WaitForSeconds(1f);
        while(true){   
            //network.inst.receive_data("P/"+id+"/"+posX+"/"+posY+"/"+rot+"/0/0/0/0/2/0");
            yield return new WaitForSeconds(0.02f);
        }
    }


    void set_player(){
        switch(player_color){
            case player.green :   player_manager.inst.myId = 0; break;
            case player.blue :    player_manager.inst.myId = 1; break;
            case player.red :     player_manager.inst.myId = 2; break;
            case player.purple :  player_manager.inst.myId = 3; break;
        }
    }

    void set_vehicule(){
        switch(_type){
            case choix_vehicule.classic :  type_vehicule = 0; break;
            case choix_vehicule.catapult : type_vehicule = 1; break;
            case choix_vehicule.canon :    type_vehicule = 2; break; 
        }
        player_manager.inst.choix_type = type_vehicule;   
    }

    void set_vague(){
        switch(start_vague){
            case vague.I         : start_at = 0; break;
            case vague.II        : start_at = 1; break;
            case vague.III       : start_at = 2; break; 
            case vague.IV        : start_at = 3; break; 
            case vague.V         : start_at = 4; break; 
            case vague.the_last  : start_at = 5; break; 
            case vague.end_Level : start_at = 6; break; 
        } 
    }

    void set_level(){
        switch(_level){
            case choix_level.level1 : level_id = 0; break;
            case choix_level.level2 : level_id = 1; break;
            case choix_level.level3 : level_id = 2; break;
        }
        for (int i = 0; i < level.Length; i++){
            if(i == level_id){
                level[i].SetActive(true);
            }else{
                level[i].SetActive(false);
            }   
        }  
    }


    void desactive_all_level_test(){
        foreach(GameObject _level in level){
            _level.SetActive(false);
        }
    }


}
