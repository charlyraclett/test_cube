using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmesh_agent : MonoBehaviour{

    public NavMeshAgent agent;
    Rigidbody rb;
    Animator anim;
    Vector3 initial_position; 
    float distance_with_player;
    int is_shooting = 0;
    bool shoot_running;
  
    [HideInInspector] public Transform target_agent;
    [HideInInspector] public bool active_agent;

    [Header("Characteristic")]
    public float distance_attack;
    public float force_shoot;
    public bool has_canon;
    public float speed_agent = 2f;
    [Range(1.5f,10)]
    public float cadence_canon = 1.5f;
    public float speed_bullet_canon = 6f;

    [Header("Edition")]
    public GameObject death_effect;
    public ParticleSystem shoot_effect;
    public Transform origin_shoot;
    public GameObject bullet;

    [Header("Info")]
    public bool has_initialize;
   
    
  
    public void initialize(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        initial_position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        agent.enabled = true;
        agent.speed = speed_agent;
        rb.useGravity = false;
        rb.isKinematic = true;
        if(has_canon){
            anim.SetTrigger("active");
            sound_manager.inst.sound_enemy_activate();
        }
        search_target();
        StartCoroutine(run());
        has_initialize = true;
    }

  
    IEnumerator run(){

        while(true){
            if(target_agent != null){
                agent.SetDestination(target_agent.position);
                distance_with_player = Vector3.Distance(transform.position, target_agent.position); 
                if(distance_with_player > 4f && distance_with_player < distance_attack && !shoot_running){
                    face_target();
                    shoot_running = true;
                    type_shoot();
                }
            }else{
                search_target();
            }
            yield return null;
        }   
    }



    void face_target(){
        Vector3 direction = (target_agent.position - transform.position).normalized;
        Quaternion  look_rotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = look_rotation;
    }

    void search_target(){
       
        foreach(Transform _target in player_manager.inst.avatars_pos){
            if(_target != null){
                target_agent = _target;
                break;
            }else{
                int rand = Random.Range(0,map_manager.inst.floor_list.Count);
                agent.SetDestination(map_manager.inst.floor_list[rand].transform.position);
                StopAllCoroutines();
                Invoke("restart_go_to_target",5f);
            }
        }
    }


    void restart_go_to_target(){
        StartCoroutine(run());
    }




     
    public void agent_dead(){

        if(has_initialize){
            Destroy(Instantiate(death_effect, transform.position, transform.rotation),6f);
            sound_manager.inst.audio_source_move.Stop();    
            sound_manager.inst.sound_agent_death();
            enemies_manager.inst.enemy_in_game.Remove(this.gameObject);
            level_manager.inst.remove_enemy_in_game();
            player_manager.inst.enemies_killed++;
            Destroy(this.gameObject);   
        }   
    }














    public void type_shoot(){
        if(has_canon){
            StartCoroutine(shoot3());
        }
        else{
            StartCoroutine(shoot());
        }
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



    public IEnumerator shoot3(){
        if(is_shooting != 1){
            is_shooting = 1;
            anim.SetTrigger("shoot_three");
            shoot_effect.Play();
            sound_manager.inst.sound_bullet_death();
            GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
            bullet_enemy bullet_enemy= _bullet.GetComponent<bullet_enemy>();
            setting_bullet(bullet_enemy);
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0;
            yield return new WaitForSeconds(cadence_canon);
            shoot_running = false;
        }
    }


    void setting_bullet(bullet_enemy _bullet){
        _bullet.GetComponent<Rigidbody>().useGravity = false;
        _bullet.canon = true;
        _bullet.speed = speed_bullet_canon;
    }


}
