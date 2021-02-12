using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressoir : MonoBehaviour{


    Animator anim;
    AudioSource audio_source;
    public AudioClip move;
    public AudioClip touch_ground_fx;
    public AudioClip alert;
    public ParticleSystem sparkle_effect;
    public BoxCollider colldier_press;
    public Light light_alert;

    int nbr_press;
    
    
    void Start(){
        anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();     
    }


    public void start_pressoir(float delay, int nbr){
        StartCoroutine(active_pressoir(delay, nbr));
        nbr_press = nbr;
    }



    IEnumerator active_pressoir(float delay, int nbr){
        yield return new WaitForSeconds(delay); 
        while(true){ 
            get_position_player();
            StartCoroutine(press_player()); 
            nbr_press--;
            if(nbr_press == 0){
                Invoke("end_press",3f);
                yield break;
            }
            yield return new WaitForSeconds(5f);
        }
    }

    void end_press(){
        level_manager.inst.remove_enemy_in_game(); 
    }


    void get_new_position(){
        int _position_apparition = Random.Range(0,map_manager.inst.floor_list.Count);
        Transform floor_pos = map_manager.inst.floor_list[_position_apparition].transform;
        Vector3 new_position = new Vector3(floor_pos.position.x, transform.position.y, floor_pos.position.z);
        transform.position = new_position;
    }

    IEnumerator press_player(){
        light_alert.enabled = true;
        audio_source.PlayOneShot(alert,0.7f);
        yield return new WaitForSeconds(0.5f);
        sound_fx_play(move,0.3f);
        sparkle_effect.Play();
        anim.SetTrigger("press_player");
       
        colldier_press.isTrigger = true;
    }


    void sound_fx_play(AudioClip _clip,float vol){
        audio_source.clip = _clip;
        audio_source.volume = vol;
        audio_source.Play();
    }



    void get_position_player(){
        if(player_manager.inst.my_avatar != null){
            Transform floor_pos = player_manager.inst.my_avatar.transform;
            Vector3 new_position = new Vector3(floor_pos.position.x, transform.position.y, floor_pos.position.z);
            transform.position = new_position;
        }else{
            get_new_position();
        }
    }


    // trigger anim
    void touch_ground(){
        sparkle_effect.Stop();
        sound_fx_play(touch_ground_fx,1f);
        colldier_press.isTrigger = false;
        light_alert.enabled = false;

    }





    
}
