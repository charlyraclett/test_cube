using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_manager : MonoBehaviour{

    public static sound_manager inst;

    public AudioSource audio_source_player;
    public AudioSource audio_source_env;
    public AudioSource audio_source_bullet;
    public AudioSource audio_source_move;
    public AudioSource audio_source_zic;

    public AudioClip death_player;
    public AudioClip shoot_player;
    public AudioClip shoot_2_player;
    public AudioClip death_bullet;
    public AudioClip rocks;
    public AudioClip friction;
    public AudioClip move;
    public AudioClip click;
    public AudioClip click_back;
    public AudioClip hover;
    public AudioClip zic_battle;
    public AudioClip zic_menu;
    public AudioClip network_ok;
    public AudioClip error;
    public AudioClip point_win;
    public AudioClip game_found;
    public AudioClip enemy_nest;

   

    public AudioClip fire;
   
   
   
    void Start(){
        inst = this;
        sound_manager.inst.sound_music_menu();

    }

    public void sound_music_battle(){
        audio_source_zic.volume = 0.3f;
        audio_source_zic.clip = zic_battle;
        audio_source_zic.Play(); 
    }

     public void sound_music_menu(){
        audio_source_zic.volume = 0.2f;
        audio_source_zic.clip = zic_menu;
        audio_source_zic.Play(); 
    }

    public void sound_game_found(){
        audio_source_player.PlayOneShot(game_found,1f);
    }


    public void sound_win_point(){
        audio_source_player.PlayOneShot(point_win,1f);
    }

    public void sound_click(){
        audio_source_player.PlayOneShot(click,1f);
    }

    public void sound_server_ok(){
        audio_source_player.PlayOneShot(network_ok,1f);
    }

    public void sound_server_error(){
        audio_source_player.PlayOneShot(error,1f);
    }

    public void sound_click_back(){
        audio_source_player.PlayOneShot(click_back,1f);
    }

    public void sound_hover(){  
        audio_source_player.PlayOneShot(hover,0.6f);
    }


    
    public void sound_death_player(){
        audio_source_player.PlayOneShot(death_player,0.5f);
    }

    public void sound_shoot_player(){ 
        audio_source_bullet.PlayOneShot(shoot_player,1f);
    }

     public void sound_shoot_fire(){ 
        audio_source_bullet.PlayOneShot(fire,0.6f);
    }

    public void sound_shoot2_player(){ 
        audio_source_bullet.PlayOneShot(shoot_2_player,1f);
    }

    public void sound_bullet_death(){  
        audio_source_bullet.PlayOneShot(death_bullet,0.6f);
    }

    public void sound_rocks(){
        audio_source_env.PlayOneShot(rocks,0.3f);
    }

    public void sound_enemy_nest(){
        audio_source_env.PlayOneShot(enemy_nest,0.4f);
    }
    

    public void sound_friction(){
        audio_source_bullet.PlayOneShot(friction,1f);
    }

    public void sound_move(){

        if(!audio_source_move.isPlaying){
            audio_source_move.volume = 1f;
            audio_source_move.clip = move;
            audio_source_move.Play();
        }
    }
}
