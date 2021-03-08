using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class follow_waypoint : MonoBehaviour{

    Vector3 target;
    NavMeshAgent agent;
    ParticleSystem particule;
    AudioSource audio_source;
    public Transform[] destinations;
    int id_walk_point = 0;

    [Header("Characteristic")]
    public float speed = 15f;
    public bool can_move;
    public float life_time;
    public bool active_now;
    public bool has_timer;
    public bool show_code;

    public GameObject death_particle;
    public AudioClip sound_death;


   

    public void initialize_electron(){
        audio_source = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();   
        particule = GetComponentInChildren<ParticleSystem>();
        agent.speed = speed;
        StartCoroutine(start_agent());
        if(has_timer)
        StartCoroutine(life_time_count());
    }


    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            controller_cube player = col.GetComponent<controller_cube>();
            player.death_sound = sound_death;
            player.death_effect = death_particle;
            player.controller_dead();

            if(!show_code)
            destroy_electron();
        }
    }



    IEnumerator start_agent(){


        while(true){

            // try{target = player_manager.inst.my_avatar.transform.position;
            // }
            // catch{
            //     target = destinations[id_walk_point].position;
            // }
            agent.SetDestination(destinations[id_walk_point].position);

            if(Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance){
                id_walk_point++;
                if(id_walk_point == destinations.Length){
                    id_walk_point = 0;
                }
                agent.SetDestination(destinations[id_walk_point].position); 
            }
            yield return null;
        }
    }   


    IEnumerator life_time_count(){

        while(life_time > 0){
            life_time--;
            yield return new WaitForSeconds(1f);
        }
        destroy_electron();
    } 


    // trigger switch light
    public void destroy_electron(){
        Destroy(Instantiate(death_particle,transform.position,transform.rotation),2f);
        sound_manager.inst.sound_electron_death();
        Destroy(this.gameObject);
    }    


    
    

}
