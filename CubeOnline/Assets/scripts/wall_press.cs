using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_press : MonoBehaviour{


    Animator anim;
    AudioSource audio_source;
    public AudioClip move_wall;
    public AudioClip touch_ground_fx;
    public AudioClip alert;
    public ParticleSystem sparkle_effect;
    public BoxCollider colldier_press;
    public Light light_alert;


    public bool active_detection;
    bool toggle_auto_detection;
    bool ready;


    void Start(){
        anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>(); 
        ready = true;   
    }


    // trigger level_two_behaviour
    public void start_pressoir(float delay){
        StartCoroutine(press_player(delay));
        
    }


    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player" && active_detection && ready && !toggle_auto_detection){      
            StartCoroutine(press_player(0f));
            toggle_auto_detection = true; 
        }
    }

    void OnTriggerExit(){
        if(active_detection)
        toggle_auto_detection = false;         
    }


    IEnumerator press_player(float delay){

        ready = false;
        yield return new WaitForSeconds(delay);
        anim.SetBool("in_game",true);
        sound_fx_play(move_wall,1f);

        yield return new WaitForSeconds(0.5f);
        audio_source.Stop();

        yield return new WaitForSeconds(2f);
        anim.SetTrigger("press");
        colldier_press.isTrigger = true;
        StartCoroutine(touch_ground()); 
    }

    IEnumerator end_wall(){
        yield return new WaitForSeconds(3f);
        anim.SetBool("in_game",false);
        sound_fx_play(move_wall,1f);
        yield return new WaitForSeconds(1f);
        ready = true;
        if(!active_detection)
        level_manager.inst.remove_enemy_in_game(); 
        
    }


   
 
    IEnumerator touch_ground(){ 
        yield return new WaitForSeconds(1.1f);
        sound_fx_play(touch_ground_fx,1f);
        yield return new WaitForSeconds(0.1f);
        colldier_press.isTrigger = false;
        StartCoroutine(end_wall()); 
    }

    


    void sound_fx_play(AudioClip _clip,float vol){
        audio_source.clip = _clip;
        audio_source.volume = vol;
        audio_source.Play();
    }

}
