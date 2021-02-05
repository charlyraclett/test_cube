using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gear_floor : MonoBehaviour{

    
    AudioSource audio_source;

    [Header("Characteristic")]
    public float force_pente;
   
    [Header("Edition")]
    public Animator anim_gear;
    public Animator anim_floor;
    public AudioClip turn_floor;
    public AudioClip alert_fan;
    public AudioClip move_gear;
    public Transform floor;

    public List<Transform> sphere_left_list = new List<Transform>();
    public List<Transform> sphere_left_list_pair = new List<Transform>();
    public List<Transform> sphere_left_list_impair = new List<Transform>();

    public GameObject prefab_sphere;
    public Transform container_sphere_left;
   
    Vector3 initial_pos_container_left;
    public GameObject lights;

    Vector3 right = new Vector3(1f,0,0);
    Vector3 left = new Vector3(-1f,0,0);
    Vector3 left_gear = Vector3.up;
    Vector3 right_gear = Vector3.down;
    Vector2 direction;
    Vector3 direction_gear;

    string anim_floor_direction;
    bool active;
    bool is_turning;

    
    

    void Start(){
        audio_source = GetComponent<AudioSource>();
       // initial_pos_container_left = new Vector3(container_sphere_left.position.x,container_sphere_left.position.y,container_sphere_left.position.z);
    }


    void Update(){
        if(Input.GetKeyDown(KeyCode.J)){
            active_gear("left");
        }
        if(Input.GetKeyDown(KeyCode.K)){
            active_gear("right");
        }
    }



    public void active_gear(string sens){
        if(sens == "right"){
            direction = right;
            direction_gear = right_gear;
            anim_floor_direction = "rotate_floor_right";
        }
        if(sens == "left"){
            direction = left;
            direction_gear = left_gear;
            anim_floor_direction = "rotate_floor_left";
        }
        StartCoroutine(start_force_to_player());
        StartCoroutine(move_player());
    }

  

   
    // move_player + anim gear
    IEnumerator move_player(){
        while(true){  
            if(active){

                GameObject[] target_players = GameObject.FindGameObjectsWithTag("Player");
                 
                foreach(GameObject player in target_players){ 
                    if(player_manager.inst.my_avatar.invulnerable == false){
                        player.transform.Translate(direction * force_pente * Time.deltaTime,Space.World);
                    }   
                }
                
                if(is_turning){
                    floor.transform.Rotate(direction_gear , force_pente * Time.deltaTime * 15f);
                }
            }
            yield return null;
        }
    }

    


    IEnumerator start_force_to_player(){

        anim_gear.SetBool("gear_in_game",true); 
        play_fx_move();

        float duration = 4f;
        float currentTime = 0f;
        float currentForce = 0f; 
        float targetValue = 4f;

        yield return new WaitForSeconds(2f);
        play_fx_bip();
        yield return new WaitForSeconds(1f);

        anim_floor.SetBool(anim_floor_direction,true);
        play_fx_sound(turn_floor,0.4f); 
        active = true;
       
        is_turning = true;
        while (currentTime < duration){
            currentTime += Time.deltaTime;
            float newValue = Mathf.Lerp(currentForce, targetValue, currentTime / duration);
            force_pente = newValue;
            yield return null;
        }
        audio_source.Stop();
        is_turning = false;
     
        StartCoroutine(start_sequence());
    }

    IEnumerator remove_initi(){
        float duration = 5f;
        float currentTime = 0f;
        float currentForce = force_pente; 
       
        anim_floor.SetBool(anim_floor_direction,false);
        direction_gear = -direction_gear;

        play_fx_sound(turn_floor,0.4f); 

        is_turning = true;
        while (currentTime < duration){
            currentTime += Time.deltaTime;
            float newValue = Mathf.Lerp(currentForce, 0, currentTime / duration);
            force_pente = newValue;
            yield return null;
        }
        force_pente = 0f;
        audio_source.Stop();
        is_turning = false;

      
        anim_gear.SetBool("gear_in_game",false);
        yield return new WaitForSeconds(0.5f); 
        play_fx_bip();
        yield return new WaitForSeconds(0.1f); 
        play_fx_move();
        level_manager.inst.remove_enemy_in_game(); 
        StopAllCoroutines();
    }




    IEnumerator start_sequence(){
        lights.SetActive(true);
        yield return null; 
        StartCoroutine(falling_sphere_left_pair(0f));
        StartCoroutine(falling_sphere_left_impair(0.6f));
        StartCoroutine(falling_sphere_left_pair(1.2f));
        StartCoroutine(falling_sphere_left_impair(1.8f));
        yield return new WaitForSeconds(4f);
        StartCoroutine(falling_sphere_left_pair(0f));
        StartCoroutine(falling_sphere_left_impair(0.4f));
        StartCoroutine(falling_sphere_left_pair(0.8f));
        StartCoroutine(falling_sphere_left_impair(1.6f));

        StartCoroutine(falling_sphere(4f));
        StartCoroutine(falling_sphere2(6f));
        yield return new WaitForSeconds(8f);
        StartCoroutine(remove_initi());
        lights.SetActive(false);
    }



    IEnumerator falling_sphere(float delai){
        yield return new WaitForSeconds(delai); 
        foreach(Transform pos_sphere in sphere_left_list){
            sphere_enemy sphere = Instantiate(prefab_sphere,pos_sphere.position,pos_sphere.rotation).GetComponent<sphere_enemy>();
            sphere.side_left = true;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }


    IEnumerator falling_sphere2(float delai){
        yield return new WaitForSeconds(delai);
        foreach(Transform pos_sphere in sphere_left_list){
            sphere_enemy sphere = Instantiate(prefab_sphere,pos_sphere.position,pos_sphere.rotation).GetComponent<sphere_enemy>();
            sphere.side_left = true;
        }
        yield return new WaitForSeconds(5f); 
    }




    IEnumerator falling_sphere_left_pair(float delai){
        yield return new WaitForSeconds(delai);
        foreach(Transform pos_sphere in sphere_left_list_pair){
            sphere_enemy sphere = Instantiate(prefab_sphere,pos_sphere.position,pos_sphere.rotation).GetComponent<sphere_enemy>();
            sphere.side_left = true;
        }
        yield return new WaitForSeconds(5f); 
    }


    IEnumerator falling_sphere_left_impair(float delai){
        yield return new WaitForSeconds(delai);
        foreach(Transform pos_sphere in sphere_left_list_impair){
            sphere_enemy sphere = Instantiate(prefab_sphere,pos_sphere.position,pos_sphere.rotation).GetComponent<sphere_enemy>();
            sphere.side_left = true;
        }
        yield return new WaitForSeconds(5f);
    }


    public void stop_wall(){
        force_pente = 0f;
        audio_source.Stop();
        is_turning = false; 
        anim_gear.SetBool("gear_in_game",false);
        anim_floor.SetBool(anim_floor_direction,false);
        level_manager.inst.remove_enemy_in_game(); 
        StopAllCoroutines();
    }







    void play_fx_move(){
        audio_source.clip = move_gear;
        audio_source.volume = 0.8f;
        audio_source.Play();
    }


    void play_fx_bip(){
        audio_source.clip = alert_fan;
        audio_source.volume = 0.8f;
        audio_source.Play();
    }

    void play_fx_turn(){
        audio_source.clip = turn_floor;
        audio_source.volume = 0.8f;
        audio_source.Play();
    }

    void play_fx_stop(){ 
        audio_source.Stop();
    }

    void play_fx_sound(AudioClip clip, float vol){
        audio_source.clip = clip;
        audio_source.volume = vol;
        audio_source.Play();
    }






   

    
}
