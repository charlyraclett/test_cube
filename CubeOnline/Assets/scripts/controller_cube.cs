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
    public bool type_catapulte;
    bool shoot_running;
    Rigidbody rb;
    bool is_moving;

    void Start(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        _bullet = bullet.GetComponent<bullet>();
        _bullet.id_player_shoot = id_avatar;
        
    }
    

    void Update(){


        if (rb.velocity.y < -15f){
            controller_dead();
        }

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
                if(type_catapulte){
                    anim.SetTrigger("shoot_two");
                }else{
                    if(shoot_running)
                    return;
                    StartCoroutine(shoot());
                }
            }
        }  
    }


    public IEnumerator shoot(){
        if(is_shooting != 1){
            is_shooting = 1;
            shoot_running = true;
            anim.SetTrigger("shoot");
            shoot_effect.Play();
            sound_manager.inst.sound_shoot_player();
            GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
            Rigidbody rb = _bullet.GetComponent<Rigidbody>();
            rb.velocity = origin_shoot.transform.TransformDirection(Vector3.forward * force_shoot);
            Destroy(_bullet, 5f);
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0;
            yield return new WaitForSeconds(1f);
            shoot_running = false;
        }
    }

    // trigger anim
    public IEnumerator shoot_catapult(){
        if(is_shooting != 1){
            is_shooting = 1;
            sound_manager.inst.sound_shoot2_player();
            GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
            _bullet.GetComponent<bullet>().no_death_touch_ground = true;
            Rigidbody rb = _bullet.GetComponent<Rigidbody>();
            rb.velocity = origin_shoot.transform.TransformDirection(Vector3.forward * force_shoot);
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0;
        }
        yield return new WaitForSeconds(1f);
    }



    public void controller_dead(){
        GameObject _death = Instantiate(death_effect, transform.position, transform.rotation);
        Destroy(_death,6f); 
        is_dead = 1;
        sound_manager.inst.sound_death_player();
        Destroy(this.gameObject);      
    }

}
