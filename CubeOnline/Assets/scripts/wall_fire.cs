using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_fire : MonoBehaviour{

    AudioSource audio_source;
    Animator anim;
    [Header("Edition")]
    public ParticleSystem fire_effect;
    public AudioClip sound_fire;
    public AudioClip move_wall;
    public AudioClip alert_flame;

   
    void Start(){
        audio_source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();   
    }



    public void lauch_fire_wall(){
        StartCoroutine(start_fire());
    }

  

    IEnumerator start_fire(){
        anim.SetBool("wall_start",true); 
        play_fx_sound(move_wall,1f);
        yield return new WaitForSeconds(2.8f);
        play_fx_sound(alert_flame,0.4f);
        yield return new WaitForSeconds(2f);
        play_fx_sound(sound_fire,0.2f);
        yield return new WaitForSeconds(0.5f);
        fire_effect.Play();
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("start_fire");
        yield return new WaitForSeconds(11f);
        audio_source.Stop();
        fire_effect.Stop();
        yield return new WaitForSeconds(2f);
        anim.SetBool("wall_start",false);
        play_fx_sound(alert_flame,0.4f);
        yield return new WaitForSeconds(0.5f);
        play_fx_sound(move_wall,1f);
        level_manager.inst.remove_enemy_in_game(); 
    }


    public void play_fx_sound(AudioClip clip, float vol){
        audio_source.clip = clip;
        audio_source.volume = vol;
        audio_source.Play();
    }



    public void stop_wall(){
        anim.SetBool("wall_start",false);
    }

   

}
