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

    int nbr_press = 3;

    public bool active_detection;
    bool ready;


    void Start(){
        anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>(); 
        ready = true;   
    }


    // trigger level_two_behaviour
    public void start_pressoir(float delay, int nbr){
        StartCoroutine(press_player(delay));
        nbr_press = nbr;
    }


    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player" && active_detection && ready){      
            StartCoroutine(press_player(0f));
            active_detection = false; 
            nbr_press = 1;
        }
    }

    void OnTriggerExit(){
      //  active_detection = true;             
    }


    IEnumerator press_player(float delay){

        ready = false;

        yield return new WaitForSeconds(delay);

        anim.SetBool("in_game",true);
        sound_fx_play(move_wall,1f);
        yield return new WaitForSeconds(0.5f);
        audio_source.Stop();

        yield return new WaitForSeconds(2f);
    
        while(nbr_press > 0){
            anim.SetTrigger("press");
            colldier_press.isTrigger = true;
            StartCoroutine(touch_ground());
            nbr_press--;
            if(nbr_press == 0){
                StartCoroutine(end_wall());
                yield break;
            }
            yield return new WaitForSeconds(6f);// delai si player dead pour esquiver
        }

       
    }

    IEnumerator end_wall(){
        yield return new WaitForSeconds(3f);
        anim.SetBool("in_game",false);
        sound_fx_play(move_wall,1f);
        yield return new WaitForSeconds(1f);
        level_manager.inst.remove_enemy_in_game(); 
        ready = true;
    }


   
 
    IEnumerator touch_ground(){ 
        yield return new WaitForSeconds(1.1f);
        sound_fx_play(touch_ground_fx,1f);
        yield return new WaitForSeconds(0.1f);
        colldier_press.isTrigger = false;
    }

    


    void sound_fx_play(AudioClip _clip,float vol){
        audio_source.clip = _clip;
        audio_source.volume = vol;
        audio_source.Play();
    }

}
