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


    void Start(){
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 7f);    
    }



    void Update(){  

        if(canon){
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.P)){ //test
            rb.useGravity = false; 
            rb.isKinematic = true; 
            StartCoroutine(DoRotationAtTargetDirection());
        }  
    }
   



    void OnTriggerEnter(Collider col){ 

        if(col.tag == "ground"){
            death_bullet();   
        }

        else if(col.tag == "Player"){
            controller_cube cube = col.gameObject.GetComponent<controller_cube>();
            cube.controller_dead();
            if(id_player_shoot != cube.id_avatar){
                ui_manager.inst.add_score_for(id_player_shoot);
            }
            death_bullet();
        }
        else if(col.tag == "enemy"){
            ui_manager.inst.add_score_for(id_player_shoot);
        }
    }

    void death_bullet(){
        GameObject _impact = Instantiate(impact,transform.position,transform.rotation);
        _impact.GetComponent<ParticleSystem>().Play();
        Destroy(_impact,4f); 
        sound_manager.inst.sound_bullet_death();
        Destroy(this.gameObject);
        if(catapult)
        fire_impact();
    }

   


    void fire_impact(){  
        sound_manager.inst.sound_shoot_fire();
        Vector3 pos_particule = new Vector3(transform.position.x,0.6f,transform.position.z);
        GameObject fire_ground = Instantiate(particule_fire_ground,pos_particule,Quaternion.identity);
        fire_ground.GetComponent<particule_collider>().id_player_bullet = id_player_shoot;
        Destroy(fire_ground,10f);   
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
