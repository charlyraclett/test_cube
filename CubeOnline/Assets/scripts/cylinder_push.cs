using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cylinder_push : MonoBehaviour{

    bool is_rotate;
    Animator anim;
    AudioSource audio_source;
    public AudioClip sound_alert;
    public AudioClip sound_move;

    public Transform cylinder;
    public float speed_rotation;
    float temp_speed_rotation;
   
    bool is_running;
   
   
    void Start(){
        audio_source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // trigger switch_power_light
    public void active_cylinder(){
        StartCoroutine(start_rotation()); 
    }

    
    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player"&& !is_running){
         // StartCoroutine(start_rotation()); 
        }
    }

    
    IEnumerator start_rotation(){

        yield return new WaitForSeconds(2f);

        is_running = true;
        anim.SetBool("in_game",true);
        play_fx_sound(sound_move,1f);
        yield return new WaitForSeconds(2f);
        play_fx_sound(sound_alert,1f);
        StartCoroutine(set_speed());
        StartCoroutine(rotate_fan_anim());
    }



    // rotation animation pale
    IEnumerator rotate_fan_anim(){
        while(is_rotate){
            cylinder.transform.Rotate(Vector3.up, temp_speed_rotation * Time.deltaTime);   
            yield return null;
        }
    }





    IEnumerator set_speed(){ 

        temp_speed_rotation = 0f;
        is_rotate = true;

        float elapsed = 0.0f;
        float duree = 2f;

        while( elapsed < duree ){
            float new_value  = Mathf.Lerp(0f,speed_rotation, elapsed / duree);
            temp_speed_rotation = new_value;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }


    void play_fx_sound(AudioClip clip, float vol){
    
        audio_source.clip = clip;
        audio_source.volume = vol;
        audio_source.Play();
    }



}
