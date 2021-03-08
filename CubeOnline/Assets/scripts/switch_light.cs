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
    public Light light_background;
    public cylinder_push _cylinder_push;
    public electric_generator _generator;
    public collider_switch_power[] switchs_to_activate;
    public bool has_power;
    public cylinder_push cylinder;
    bool has_finished;
   
    
    void Start(){
        audio_source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.speed = Random.Range(0.5f,1.8f);
    }



    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player" && !has_finished){
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

        if(is_generator){
            for(int i = 0; i < switchs_to_activate.Length; i++){ // activation du second switch
                switchs_to_activate[i].set_active(); 
            }
        }

        if(!is_generator){
            StartCoroutine(cylinder.stop());
            StartCoroutine(destroy_electron());
            print("stop cyliner and killed electrons");
        }
    }



    IEnumerator power_on(){ 
        play_fx(switch_sound);
        yield return new WaitForSeconds(0.4f);
        anim.speed = 1f;
        anim.SetTrigger("switch_on");
        has_finished = true;
    }


    // trigger anim
    void switch_on_light(int id_light){  
        if(id_light == 1){
            audio_source.Stop();
            if(!is_generator){                                      // end mechanism light
                StartCoroutine(dimmer_light_background());
                level_manager.inst.special_event();
            }
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



    IEnumerator destroy_electron(){

        yield return new WaitForSeconds(0.5f);

        GameObject[] electrons = GameObject.FindGameObjectsWithTag("electron_enemy");
        print("search electrons");

        foreach (GameObject electron in electrons){
            electron.GetComponent<follow_waypoint>().destroy_electron();
            yield return new WaitForSeconds(0.3f);
        }
    }



    IEnumerator dimmer_light_background(){

        float elapsed = 0.0f;
        float duree = 4f;

        while( elapsed < duree ){
            float new_value  = Mathf.Lerp(0,4, elapsed / duree);
            light_background.intensity = new_value;
            elapsed += Time.deltaTime;
            yield return null;
        }

        
    }

    


   
       
    


    




    

      




       

}
