using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class level_manager : MonoBehaviour{

    public static level_manager inst;

    [Range(0,1)]
    public int id_level;
    
    [Header("Edition")]
    public GameObject[] level;

    [Header("UI buttons")]
    public Button[] buttons_level;
    
    [HideInInspector]public int enemies_in_game;
    [HideInInspector]public int id_vague;

    map_manager map_level;



    void Awake(){  
        inst = this; 
    }

    // trigger game_manager
    public void load_level(){
        StartCoroutine(load_map(id_level));
        string level_number = id_level == 0 ? "I" : "II"; // todo a revoir 
        ui_manager.inst.refresh_text_level_nbr(level_number);
    }


    IEnumerator load_map(int id){
        GameObject my_level = Instantiate(level[id], new Vector3(0, 0, 0), Quaternion.identity); 
        yield return new WaitForSeconds(0.5f);
        map_level = my_level.GetComponentInChildren<map_manager>();
        map_level.launch_anim_build_map(0f,0.02f);
        print("load map");
    }


    // changement vague trigger death enemy
    public void remove_enemy_in_game(){
        enemies_in_game--;
        if(enemies_in_game == 0 && player_manager.inst.life_player > 0){
            id_vague++;
            StartCoroutine(change_vague(id_vague));
            player_manager.inst.check_if_shoot_perfect();
        }   
    }
    

    public IEnumerator change_vague(int id){
        yield return new WaitForSeconds(1f);
        switch(id){
            case 0 : vague_0();  break;
            case 1 : vague_1();  break;
            case 2 : vague_2();  break;
            case 3 : vague_3();  break;
            case 4 : vague_4();  break;
            case 5 : vague_5();  break;
            case 6 : finished(); break;
        }
    }




    public virtual void vague_0(){
        print("level manager vague 0");
        ui_manager.inst.refresh_text_vague_nbr("I");
        sound_manager.inst.sound_change_vague();
    }

    public virtual void vague_1(){
        ui_manager.inst.refresh_text_vague_nbr("II"); 
        sound_manager.inst.sound_change_vague();
    }

    public virtual void vague_2(){
        ui_manager.inst.refresh_text_vague_nbr("III"); 
        sound_manager.inst.sound_change_vague();
    }

    public virtual void vague_3(){
        ui_manager.inst.refresh_text_vague_nbr("IV"); 
        sound_manager.inst.sound_change_vague();
    }

    public virtual void vague_4(){
        ui_manager.inst.refresh_text_vague_nbr("V"); 
        sound_manager.inst.sound_change_vague();
    }

    public virtual void vague_5(){
        ui_manager.inst.refresh_text_vague_nbr("VI");
        StartCoroutine(ui_manager.inst.show_last_text_vague(true));
        sound_manager.inst.sound_change_vague();
    }

    public void finished(){
        game_manager.inst.level_complete();  
        PlayerPrefs.SetInt("level_complete", id_level + 1);
        PlayerPrefs.Save(); 
    }


    public virtual void stop_all(){
        StopAllCoroutines();
    }




    public void active_buttons_acces_levels(){
        int level_complete = PlayerPrefs.GetInt("level_complete");
        for (int i = 0; i <= level_complete; i++){
            buttons_level[i].interactable = true;
            buttons_level[i].gameObject.GetComponent<Animator>().enabled = true;
            buttons_level[i].transform.GetChild(0).gameObject.SetActive(false);
            buttons_level[i].transform.GetChild(1).gameObject.SetActive(true);
        }  
    }
     



}
