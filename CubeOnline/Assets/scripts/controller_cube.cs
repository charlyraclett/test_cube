using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller_cube : MonoBehaviour{

    Animator anim;
    public bool host;
    public int id_avatar;
    public float speed = 5;
    public float speed_rotation = 1;
    public Transform origin_shoot;
    public ParticleSystem shoot_effect;
    public GameObject bullet;
    public float force_shoot;
    public GameObject death_effect;
    public int is_dead = 0;
    public int is_shooting = 0;
    bullet _bullet;


    void Start(){

        anim = GetComponent<Animator>();
        _bullet = bullet.GetComponent<bullet>();
        _bullet.id_player_shoot = id_avatar;
    }
    


   

    void Update(){

        if(host){

            if (Input.GetKey(KeyCode.UpArrow)){  
                this.transform.Translate(Vector3.forward * Time.deltaTime * speed);  
            }  
            
            if (Input.GetKey(KeyCode.DownArrow)){  
                this.transform.Translate(Vector3.back * Time.deltaTime * speed);  
            }  
            
            if (Input.GetKey(KeyCode.LeftArrow)){  
                this.transform.Rotate(Vector3.up,-speed_rotation);  
            }  
            
            if (Input.GetKey(KeyCode.RightArrow)){  
                this.transform.Rotate(Vector3.up,speed_rotation);  
            } 

            if (Input.GetKeyDown(KeyCode.Space)){  
                StartCoroutine(shoot());
            }

             if (Input.GetKeyDown(KeyCode.D)){  
                controller_dead();
            }
        }  
    }


    public IEnumerator shoot(){

        if(is_shooting != 1){
            is_shooting = 1;
            anim.SetTrigger("shoot");
            shoot_effect.Play();
            GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
            Rigidbody rb = _bullet.GetComponent<Rigidbody>();
            rb.velocity = origin_shoot.transform.TransformDirection(Vector3.forward * force_shoot);
            Destroy(_bullet, 5f);
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0;
        }
       
    }


    void OnTriggerEnter(Collider col){ 

        if(col.tag == "degat"){

            network.inst.add_score_for(col.gameObject.GetComponent<bullet>().id_player_shoot);
            controller_dead();
        } 
    }


    public void controller_dead(){
        GameObject _death = Instantiate(death_effect, transform.position, transform.rotation);
        Destroy(_death,6f); 
        is_dead = 1;
        Destroy(this.gameObject);      
    }


      // IEnumerator player_one_to_base(){
    //     Vector3 set_position_start = new Vector3(base_player_one.position.x,0.78f,base_player_one.position.z);
    //     while(Vector3.Distance(player_one_network.position, set_position_start) > 0.01f){
    //         player_one_network.position = Vector3.MoveTowards(player_one_network.position, set_position_start, 30 * Time.deltaTime);
    //         player_one_network.rotation = Quaternion.Slerp( player_one_network.rotation,base_player_one.rotation, 10 * Time.deltaTime);
    //         yield return new WaitForSeconds(0.02f);
    //     }
    // }


     // IEnumerator check_player_one(){
        
    //     while(!player_one_is_dead){
    //         yield return new WaitForSeconds(0.02f);
    //     }
    //     yield return new WaitForSeconds(1f);
    //     add_score_two();
    //     StartCoroutine(player_two_to_base());
       
    //     yield return new WaitForSeconds(2f);
    //     Vector3 set_position_start = new Vector3(base_player_one.position.x,0.78f,base_player_one.position.z);
    //     GameObject one = Instantiate(player_one,set_position_start,base_player_one.rotation);
    //     player_one_network = one.GetComponent<Transform>();
    //     if(host){
    //         one.GetComponent<controller_cube>().host = true;
    //     }else{
    //         one.GetComponent<controller_cube>().host = false;
    //     }
    //     player_one_is_dead = false;
    //     StartCoroutine(check_player_one());
    // }







}
