using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saw_circular : MonoBehaviour{

    AudioSource audio_source;

    [Header("Characteristic")]
    public float rotation_speed = 150f;
    public float speed_attack = 3f;
    public float speed_follow = 4f;
    public float position_y_start = -4f;
    public float position_back = 35f;

    [Header("Edition")]
    public Transform saw_rotation;
    public ParticleSystem[] spark;
    public AudioClip saw_sound;
    public AudioClip move_wall;
    public AudioClip sound_alert;
    public TrailRenderer trail;
    public AudioSource audio_source_motor;
    public AudioSource audio_source_move_head;
    public GameObject[] marqueur_alerte;

    float speed_rotation;
   
    bool position_start;
    bool toggle_ground;
    public bool desactive;
    Vector3 initiale_position;


   
    void Start(){
        audio_source = GetComponent<AudioSource>();
        position_start = true;
        initiale_position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }

    
    // trigger level_two_behaviour
    public void start_rotate_saw(float delay){
        StartCoroutine(in_game(position_y_start, delay));
    }

   
   
    void OnTriggerEnter(Collider col){ 
       
        if(col.tag == "Player"){      
            controller_cube cube = col.gameObject.GetComponent<controller_cube>();
            cube.controller_dead();      
        }

        if(col.tag == "ground" && !toggle_ground){  
            foreach(ParticleSystem _spark in spark){
                _spark.Play();
            }    
            trail.enabled = true;
            toggle_ground = true;
            play_fx_sound(saw_sound,0.2f);
            StopCoroutine("stop_collider");
        }
    }




    void OnTriggerExit(Collider col){
        if(col.tag == "ground" && toggle_ground){      
           StartCoroutine("stop_collider");
        }
    }

    IEnumerator stop_collider(){
        yield return new WaitForSeconds(0.5f);
        foreach(ParticleSystem _spark in spark){
            _spark.Stop();
        }
        audio_source.Stop();
        toggle_ground = false;
        yield return new WaitForSeconds(0.5f);
        trail.enabled = false;
    }


    IEnumerator check_number_enemies(){
        while(true){
            if(level_manager.inst.enemies_in_game == 1){
                desactive = true;
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    //rotation animation
    IEnumerator rotate_fan_anim(){
        while(true){
            saw_rotation.transform.Rotate(Vector3.down, speed_rotation * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator start_rotate(float delay){ 
      
        yield return new WaitForSeconds(delay);

        StartCoroutine(rotate_fan_anim());
        float elapsed = 0.0f;
        float duree = 2f;
        float start_speed = 0f; 
        float speed_target = rotation_speed;
        speed_rotation = 0f;

        audio_source_motor.Play();
        
        while( elapsed < duree ){
            start_speed = Mathf.Lerp(0f, speed_target, elapsed / duree);
            speed_rotation = start_speed; 
            elapsed += Time.deltaTime;
            yield return null;
        }

       StartCoroutine(follow_player(2f));
    }

   

    IEnumerator follow_player(float delay){

        yield return new WaitForSeconds(delay);

        float distance_x = 10f;
        float destination = position_start ? destination = position_back : destination = position_y_start;

        Transform target;

        try{ 
            Transform _target = player_manager.inst.my_avatar.transform;
            target = _target;
        }
        catch{
            target = transform; 
        }

        audio_source_move_head.Play();

        while(target != null && (distance_x > 0.5f || distance_x < -0.5f)){
            distance_x = transform.position.x - target.transform.position.x;
            float target_axe = Mathf.Lerp(transform.position.x, target.position.x, speed_follow * Time.deltaTime);
            transform.position = new Vector3(target_axe,transform.position.y, transform.position.z);
            yield return null;
        } 

        audio_source_move_head.Stop();
        StartCoroutine(move_attack_saw(destination));  
    }




    IEnumerator move_attack_saw(float destination_position_z){

        float z = transform.position.z;
        float elapsed = 0.0f;
        float duree = speed_attack;
     
        while(elapsed < duree ){
            float new_z = Mathf.Lerp(z,destination_position_z, elapsed / duree);
            transform.position = new Vector3(transform.position.x,transform.position.y, new_z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        if(destination_position_z == position_back){
            position_start = false;     
        }else{
            position_start = true;

            if(desactive){
                StartCoroutine(end_game());
                yield break;
            }
        }

        StartCoroutine(follow_player(1f));
    }


    IEnumerator end_game(){
        float elapsed = 0f;
        float duree = 2f;
        yield return new WaitForSeconds(1f);           
        float x = transform.position.x;
        audio_source_move_head.Play();
        while(elapsed < duree ){
            float new_x = Mathf.Lerp(x,-0.38f, elapsed / duree);
            transform.position = new Vector3(new_x,transform.position.y, transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        audio_source_move_head.Stop();
        yield return new WaitForSeconds(0.5f);
        foreach(GameObject marqueur in marqueur_alerte){
            marqueur.SetActive(false);
        }
        play_fx_sound(sound_alert,1f);
        elapsed = 0f;
        duree = 1f;

        while( elapsed < duree ){
            float speed  = Mathf.Lerp(150, 0f, elapsed / duree);
            speed_rotation = speed; 
            elapsed += Time.deltaTime;
            yield return null;
        }

        speed_rotation = 0f;
        audio_source_motor.Stop();

        yield return new WaitForSeconds(0.5f);      

        play_fx_sound(move_wall,1f);

        elapsed = 0f;
        float z = transform.position.z;

        while(elapsed < duree ){
            float new_z = Mathf.Lerp(z,-10f, elapsed / duree);
            transform.position = new Vector3(transform.position.x,transform.position.y, new_z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        level_manager.inst.remove_enemy_in_game(); 
        StopAllCoroutines();
    }



    IEnumerator in_game(float destination_position_z, float delay){

        yield return new WaitForSeconds(delay);

        float z = transform.position.z;
        float elapsed = 0.0f;
        float duree = 1.5f;
        play_fx_sound(move_wall,1f);

        while(elapsed < duree ){
            float new_z = Mathf.Lerp(z,destination_position_z, elapsed / duree);
            transform.position = new Vector3(transform.position.x,transform.position.y, new_z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        foreach(GameObject marqueur in marqueur_alerte){
            marqueur.SetActive(true);
        }
        play_fx_sound(sound_alert,1f);
        StartCoroutine(start_rotate(1f));
        StartCoroutine(check_number_enemies());
    }




    void play_fx_sound(AudioClip clip, float vol){
        if(audio_source.isPlaying)
        return;
        audio_source.clip = clip;
        audio_source.volume = vol;
        audio_source.Play();
    }


    // trigger level_two_behaviour
    public void reset_saw(){
        StopAllCoroutines();
        transform.position = initiale_position;
        position_start = true;
        toggle_ground = false;
        desactive = false; 
        audio_source_motor.Stop();  
    }



    
}
