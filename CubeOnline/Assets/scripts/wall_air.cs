using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_air : MonoBehaviour{

    Animator anim;
    AudioSource audio_source;

    [Header("Characteristic")]
    public bool is_rotate;
    public float speed_rotation;
    public float force;
    public int duration;

    [Header("Edition")]
    public AudioClip move_wall;
    public AudioClip alert_fan;
    public AudioClip fan_turn;

    public Transform[] fan;
    public ParticleSystem[] effect_fan;
    

    void Start(){
        audio_source = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>(); 
    }

    public void lauch_fan_wall(){
        search_player();
        StartCoroutine(start_rotate());
        StartCoroutine(rotate_fan_anim());
    }


    void search_player(){
        GameObject[] target_players = GameObject.FindGameObjectsWithTag("Player");;
        StartCoroutine(move_player(target_players)); 
    }

    
    // move_player
    IEnumerator move_player(GameObject[] players){
        while(true){  
            foreach(GameObject player in players){ 
                if(player != null){
                    Vector3 move = new Vector3(0f, 0f, -1f);
                    player.transform.Translate(move * force * Time.deltaTime,Space.World);
                }
            }
            yield return null;
        }
    }


    // rotation animation pale
    IEnumerator rotate_fan_anim(){
        while(is_rotate){
            fan[0].transform.Rotate(Vector3.right, speed_rotation * Time.deltaTime);
            fan[1].transform.Rotate(Vector3.right, speed_rotation * Time.deltaTime);
            yield return null;
        }
    }





    IEnumerator start_rotate(){ 

        float elapsed = 0.0f;
        float duree = 5f;
        float start_speed = 0f;
        float start_force = 0f;
        float speed_target = 700f; // en dur pour le moment
        float force_target = 7f;
        speed_rotation = 0f;
        force = 0f;

        anim.SetBool("wall_start",true); 
        play_fx_sound(move_wall,1f);

        yield return new WaitForSeconds(2f);
        play_fx_sound(alert_fan,0.4f);

        yield return new WaitForSeconds(1f);
        effect_fan[0].Play();
        effect_fan[1].Play();
        play_fx_sound(fan_turn,1f);

        while( elapsed < duree ){
            start_speed = Mathf.Lerp(0f, speed_target, elapsed / duree);
            start_force = Mathf.Lerp(0f, force_target, elapsed / duree);
            speed_rotation = start_speed;
            force = start_force;
            elapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(timer());
        effect_fan[0].Stop(); // temporaire 
        effect_fan[1].Stop();
    }




    IEnumerator timer(){
        int i = 0;
        while(i < duration){
            i++;
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(end_rotate());
    }


    IEnumerator end_rotate(){ 
        float elapsed = 0.0f;
        float duree = 0.6f;
        float speed = speed_rotation;
        float force_target = force;

        while( elapsed < duree ){
            speed_rotation = Mathf.Lerp(speed,0.0f,elapsed / duree);
            force = Mathf.Lerp(force_target,0.0f,elapsed / duree);    
            elapsed += Time.deltaTime;
            yield return null;
        }
        force = 0f;
        speed_rotation = 0f;

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("wall_start",false);
        play_fx_sound(alert_fan,0.5f);
        yield return new WaitForSeconds(0.5f);
        play_fx_sound(move_wall,1f);
        StopAllCoroutines();
    }


    public void stop_wall(){
        anim.SetBool("wall_start",false);
        StopAllCoroutines();
    }





    void play_fx_sound(AudioClip clip, float vol){
        audio_source.clip = clip;
        audio_source.volume = vol;
        audio_source.Play();
    }

}
