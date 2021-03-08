using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electric_generator : MonoBehaviour{

    Animator anim;
    AudioSource audio_source;

    public bool active_now;

    [Header("Characteristic")]
    public bool show_code;
    public int nbr_electron;
    public float cadence;
    public int[] point_path1;
    public int[] point_path2;
    public int[] point_path3;
    public int[] point_path4;

    Transform[] path1;
    Transform[] path2;
    Transform[] path3;
    Transform[] path4;


    [Header("Edit")]
    public GameObject particule_electron;
    public ParticleSystem effect_arc_start;
    public AudioClip flash_elec;
    public AudioClip move;
    public AudioClip move_head;
    public Transform origin_arc;

    bool toggle_move;

    int path_id;
    Transform[] path_for_electron;
    float speed = 48f;


    void Start(){
        path1 = new Transform[point_path1.Length];
        path2 = new Transform[point_path2.Length];
        path3 = new Transform[point_path3.Length];
        path4 = new Transform[point_path4.Length];
        anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();  
        Invoke("set_walk_path",1f);
    }


    // trigger_switch_buttons
    public void start_generator(){
        StartCoroutine(start_shock());
    }



    IEnumerator start_shock(){

        int nbr = nbr_electron;

        anim.SetBool("in_game",true);
        audio_source.PlayOneShot(move,1f);
        yield return new WaitForSeconds(1f);
        audio_source.Stop();

        yield return new WaitForSeconds(1f);

        anim.SetBool("ready_shock",true);
        yield return new WaitForSeconds(3f);

        while(nbr > 0){
            stop_sound_servo();
            audio_source.PlayOneShot(flash_elec,1f);
            effect_arc_start.Play();
            yield return new WaitForSeconds(0.4f);
            follow_waypoint electron = Instantiate(particule_electron,origin_arc.position,origin_arc.rotation).GetComponent<follow_waypoint>();
            select_path();
            electron.destinations = path_for_electron;
            electron.speed = speed;
            electron.show_code = show_code;
            electron.initialize_electron();
            nbr--;
            toggle_move =!toggle_move;
            yield return new WaitForSeconds(0.25f);
            anim.SetBool("move_toggle",toggle_move);
            play_sound_servo();
            yield return new WaitForSeconds(0.25f);
            yield return new WaitForSeconds(cadence);
        }
        yield return new WaitForSeconds(1f);
        anim.SetBool("ready_shock",false);

        yield return new WaitForSeconds(1f);
        anim.SetBool("in_game",false);
        audio_source.PlayOneShot(move,1f);
        path_id = 0;
    }



    // trigger anim
    void play_sound_servo(){
        audio_source.clip = move_head;
        audio_source.Play();
    }

    // trigger anim
    void stop_sound_servo(){
        audio_source.Stop();  
    }


    
    void set_walk_path(){

        for (int i = 0; i < point_path1.Length; i++){
            path1[i] = map_manager.inst.floor_list[point_path1[i]].gameObject.transform.GetChild(0);   
        } 

        for (int i = 0; i < point_path2.Length; i++){
            path2[i] = map_manager.inst.floor_list[point_path2[i]].gameObject.transform.GetChild(0);   
        }  

        for (int i = 0; i < point_path3.Length; i++){
            path3[i] = map_manager.inst.floor_list[point_path3[i]].gameObject.transform.GetChild(0);   
        }  

        for (int i = 0; i < point_path4.Length; i++){
            path4[i] = map_manager.inst.floor_list[point_path4[i]].gameObject.transform.GetChild(0);   
        }   
       
        if(active_now)
        start_generator();
    }




    void select_path(){

        if(path_id > 3){
            path_id = 0;
        }

        switch(path_id){
            case 0 :  path_for_electron = path1 ; speed = 35f; break;
            case 1 :  path_for_electron = path2 ; speed = 3f; break;
            case 2 :  path_for_electron = path3 ; speed = 20f; break;
            case 3 :  path_for_electron = path4 ; speed = 10f; break;
        }

        path_id++;
    }


    





    
}
