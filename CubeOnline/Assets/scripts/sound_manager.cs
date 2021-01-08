using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_manager : MonoBehaviour{

    public static sound_manager inst;

    public AudioSource audio_source_player;
    public AudioSource audio_source_env;
    public AudioSource audio_source_bullet;
    public AudioSource audio_source_move;

    public AudioClip death_player;
    public AudioClip shoot_player;
    public AudioClip shoot_2_player;
    public AudioClip death_bullet;
    public AudioClip rocks;
    public AudioClip friction;
    public AudioClip move;
    public AudioClip click;
     public AudioClip hover;
   
   
    void Start(){
        inst = this;
    }

    public void sound_click(){
       
        audio_source_player.PlayOneShot(click,1f);
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

    public void sound_shoot2_player(){ 
        audio_source_bullet.PlayOneShot(shoot_2_player,1f);
    }

    public void sound_bullet_death(){  
        audio_source_bullet.PlayOneShot(death_bullet,0.6f);
    }

    public void sound_rocks(){
        audio_source_env.PlayOneShot(rocks,0.3f);
    }

    public void sound_friction(){
        audio_source_bullet.PlayOneShot(friction,1f);
    }

    public void sound_move(){

        if(audio_source_move.isPlaying)
        return;
        audio_source_move.volume = 1F;
        audio_source_move.clip = move;
        audio_source_move.Play();
    }
}
