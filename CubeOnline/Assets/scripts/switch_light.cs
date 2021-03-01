using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_light : MonoBehaviour{

    Animator anim;
    AudioSource audio_source;

    public bool is_generator;
   
    [Header("Edit")]
    public AudioClip switch_sound;
    public AudioClip light_on_sound;
    public AudioClip no_power;
    public Animator[] lights_to_switch;
    public cylinder_push _cylinder_push;
    public electric_generator _generator;
    public bool has_power;
   
    
    void Start(){
        audio_source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }



    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player"){
            interactable_manager.inst.player_is_in_area_interactable(this);  
        }
    }

    void OnTriggerExit(Collider col){ 
        if(col.tag == "Player"){   
            interactable_manager.inst.quit_aera_interactable(); 
        }
    }


   
    // trigger interactable manager
    public void active_light(){   
        if(!has_power){
            play_fx(no_power);
            interactable_manager.inst.quit_aera_interactable(); 
            return;
        }
        StartCoroutine(power_on());
    }



    IEnumerator power_on(){ 
        play_fx(switch_sound);
        yield return new WaitForSeconds(0.4f);
        anim.SetTrigger("switch_on");
    }


    // trigger anim
    void switch_on_light(int id_light){
        if(id_light == 1){
            audio_source.Stop();
        }
        audio_source.PlayOneShot(light_on_sound,1f);
        lights_to_switch[id_light].SetTrigger("power_on");
    }


    // trigger power_switch
    public void switch_is_ready(){
        has_power = true;
        anim.SetTrigger("has_power");
        if(is_generator){
            _generator.start_generator();
        }else{  
            _cylinder_push.active_cylinder();
        }
    }




    void play_fx(AudioClip clip){
        audio_source.clip = clip;
        audio_source.volume = 1f;
        audio_source.Play();
    }


   
       
    


    




    

      




       

}
