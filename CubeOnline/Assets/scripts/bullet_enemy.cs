using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_enemy : MonoBehaviour{

    public GameObject impact;
    public bool catapult;
    public float speed = 5f;
    public float speed_rotation = 50f;
    public bool canon;
   
    public Transform target;
    Rigidbody rb;


    public GameObject trail;


    public Vector3 target_refech_pos;

  
    
    void Start(){
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 7f);  


        // test a delete
       // StartCoroutine(DoRotationAtTargetDirection(1f));
       // canon = true;
      
    }


    void Update(){  
        if(canon){
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
   


    void OnTriggerEnter(Collider col){ 
        if(col.tag == "ground"){
            death_bullet(); 
        }

        else if(col.tag == "Player"){
            controller_cube cube = col.gameObject.GetComponent<controller_cube>();
            cube.controller_dead();
            death_bullet();
        }  
    }


    void death_bullet(){
        GameObject _impact = Instantiate(impact,transform.position,transform.rotation);
        _impact.GetComponent<ParticleSystem>().Play();
        Destroy(_impact,4f); 
        sound_manager.inst.sound_bullet_death();
        if(trail != null){
            trail.transform.parent = null; 
            Destroy(trail,0.5f); 
        }
        Destroy(this.gameObject);
    }

   





    // bullet suit le player
    public IEnumerator follow_player(){   
      
        while (true){

            Vector3 test = new Vector3(target.position.x, target.position.y, target.position.z);
            transform.LookAt(target);

            transform.position = Vector3.MoveTowards(transform.position, test, speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator refresh_position(){    
        while(true){   
            target_refech_pos = target.position;
            yield return new WaitForSeconds(1f);
        }
    }



    //rotate to player
    IEnumerator DoRotationAtTargetDirection(float delai){

        yield return new WaitForSeconds(delai);
       
        Quaternion targetRotation = Quaternion.identity;
       canon = false;

        do{
            Vector3 targetDirection = (target.transform.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * speed_rotation);
            transform.position += transform.forward * speed * Time.deltaTime; // forward
            yield return null;
 
        } while(Quaternion.Angle(transform.rotation, targetRotation) > 0.01f);

        StartCoroutine(follow_player());
      //  StartCoroutine(refresh_position());

    }


   


}
