using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour{

    public GameObject impact;
    public int id_player_shoot;
    public bool catapult;
    public float speed = 5f;
    public bool canon;
    public Transform target;
    Rigidbody rb;

    public GameObject particule_fire_ground;

    bool is_touching;
    float distance_shoot_reussi;
    float seuil_nice_shot = 10f;


    void Start(){
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 7f);  
        StartCoroutine(calcul_distance());
    }



    void Update(){  
        if(canon){
            transform.position += transform.forward * speed * Time.deltaTime;
        }  
    }
   


    void OnTriggerEnter(Collider col){ 

        if(!is_touching){

            if(col.tag == "Player"){
                is_touching = true;
                controller_cube cube = col.gameObject.GetComponent<controller_cube>();
                cube.controller_dead();
                if(id_player_shoot != cube.id_avatar){
                   count_shoot();
                }
                death_bullet(); 
            }

            else if(col.tag == "enemy"){
                is_touching = true; 
                navmesh_agent enemy = col.gameObject.GetComponent<navmesh_agent>();
                if(!enemy.has_initialize)
                return;
                enemy.agent_dead();
                count_shoot();
                death_bullet(); 
            }

            else if(col.tag == "bomb"){
                is_touching = true; 
                bomb_enemy bomb = col.gameObject.GetComponent<bomb_enemy>();
                if(!bomb.has_initialize)
                return;
                bomb.agent_dead();
                count_shoot();
                death_bullet(); 
            }  

            else if(col.tag == "ground"){
                is_touching = true; 
                if(catapult){
                    fire_impact();
                    death_bullet();
                }else{
                    death_bullet();
                }
            }
        }
    }


    void count_shoot(){
        StartCoroutine(player_manager.inst.refresh_score(id_player_shoot, distance_shoot_reussi));  
    }



    void death_bullet(){
        if(catapult){
            GameObject _impact = Instantiate(impact,transform.position,transform.rotation);
            _impact.GetComponent<ParticleSystem>().Play();
            Destroy(_impact,4f);
            Destroy(this.gameObject);
            return;
        }

        else{
            sound_manager.inst.sound_bullet_death();
            GameObject _impact = Instantiate(impact,transform.position,transform.rotation);
            _impact.GetComponent<ParticleSystem>().Play();
            Destroy(_impact,4f); 
            Destroy(this.gameObject);
        }
    }

    

    void fire_impact(){  
        sound_manager.inst.sound_shoot_fire();
        Vector3 pos_particule = new Vector3(transform.position.x,0.6f,transform.position.z);
        GameObject fire_ground = Instantiate(particule_fire_ground,pos_particule,Quaternion.identity);
        fire_ground.GetComponent<particule_collider>().id_player_shoot = id_player_shoot;
        Destroy(fire_ground,10f);   
    }


    void raycast_distance(){

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin,direction);

        if(Physics.Raycast(ray, out RaycastHit raycastHit)){
            if(raycastHit.transform.tag == "enemy" || raycastHit.transform.tag == "bomb"){
                float distance_shoot = Vector3.Distance (transform.position, raycastHit.transform.transform.position);
                distance_shoot_reussi = distance_shoot;
            }
        } 
    }

    IEnumerator calcul_distance(){
        while(player_manager.inst.my_avatar != null){
            distance_shoot_reussi = Vector3.Distance (transform.position, player_manager.inst.my_avatar.transform.position);
            yield return new WaitForSeconds(0.02f);
        }
    }










    // bullet suit le player
    public IEnumerator follow_player(){   

        Vector3 test = new Vector3(target.position.x, target.position.y, target.position.z);
        while (true){
            transform.position = Vector3.MoveTowards(transform.position, test, 10f * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator refresh_position(){   

        Vector3 test = new Vector3(target.position.x, target.position.y, target.position.z);
        while (true){
            transform.position = Vector3.MoveTowards(transform.position, test, 10f * Time.deltaTime);
            yield return null;
        }
    }



    //rotate to player
    IEnumerator DoRotationAtTargetDirection(){
       
        Quaternion targetRotation = Quaternion.identity;

        do{
            Vector3 targetDirection = (target.transform.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 70f);
            transform.position += transform.forward * 10f * Time.deltaTime; // forward
            yield return null;
 
        } while(Quaternion.Angle(transform.rotation, targetRotation) > 0.01f);

        StartCoroutine(follow_player());

    }


   


}
