using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class level_manager : MonoBehaviour{

    public static level_manager inst;

   
    public int enemies_in_game;
    public int id_vague;

   
 


    void Awake(){  
        inst = this;   
    }

   
    //changement vague trigger death enemy
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
        sound_manager.inst.sound_change_vague();
        StartCoroutine(ui_manager.inst.show_last_text_vague(true));
    }

    public void finished(){
        game_manager.inst.level_complete();  
    }


    public virtual void stop_all(){
        StopAllCoroutines();
    }


    public virtual void reset_level(){
        level_manager.inst.id_vague = 0;
    }

    public virtual void delete_level(){
        StopAllCoroutines();
        print("stop level");
    }
    public virtual void reinitialize_position_mechanism(){}
    





}
