using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmesh_agent : MonoBehaviour{

    NavMeshAgent agent;
    public GameObject death_effect;
    public bool active_agent;
    Vector3 initial_position;
    int is_shooting = 0;
    Animator anim;
    public ParticleSystem shoot_effect;
    public Transform origin_shoot;
    public GameObject bullet;
    public float force_shoot;
    bool shoot_running;
    public Transform target_agent;

    float distance;


    public void intialize(){
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        search_target();
        StartCoroutine(run());
    }

  
    IEnumerator run(){
        while(true){
            if(target_agent != null){
                agent.SetDestination(target_agent.position);
                distance = Vector3.Distance(transform.position, target_agent.position); 
                if(distance > 4f && distance < 7f && !shoot_running){
                    face_target();
                    shoot_running = true;
                    StartCoroutine(shoot());
                }
            }else{
                search_target();
            }
            yield return null;
        }   
    }



    void OnTriggerEnter(Collider col){
        if(col.tag == "degat"){
            Destroy(col.gameObject);
            agent_dead();
        }
    }


     
    public void agent_dead(){

        Destroy(Instantiate(death_effect, transform.position, transform.rotation),6f);
        sound_manager.inst.audio_source_move.Stop();    
        sound_manager.inst.sound_death_player();
        Destroy(this.gameObject);      
    }




    public IEnumerator shoot(){

        if(is_shooting != 1){
            is_shooting = 1;
            anim.SetTrigger("shoot");
            shoot_effect.Play();
            sound_manager.inst.sound_shoot_player();
            GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
            Rigidbody rb = _bullet.GetComponent<Rigidbody>();
            rb.velocity = origin_shoot.transform.TransformDirection(Vector3.forward * force_shoot);
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0; // network
            yield return new WaitForSeconds(2f);
            shoot_running = false;
        }
        
    }


    void face_target(){
        Vector3 direction = (target_agent.position - transform.position).normalized;
        Quaternion  look_rotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = look_rotation;
    }

    void search_target(){
        foreach(Transform _target in network.inst.avatars_pos){
            if(_target != null){
                target_agent = _target;
                break;
            }
        }
    }

}
