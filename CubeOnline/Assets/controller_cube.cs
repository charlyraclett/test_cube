using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller_cube : MonoBehaviour{
    public bool host;
    public float speed = 5;
    public float speed_rotation = 1;
    public Transform origin_shoot;
    public ParticleSystem shoot_effect;
    public GameObject bullet;
    public float force_shoot;
    public GameObject death_effect;


   

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
                shoot();
            }
        }  
    }


    void shoot(){

        shoot_effect.Play();
        GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
        Rigidbody rb = _bullet.GetComponent<Rigidbody>();
        rb.velocity = origin_shoot.transform.TransformDirection(Vector3.forward * force_shoot);
        Destroy(_bullet, 5f);
    }


    void OnTriggerEnter(Collider col){ 

        if(col.tag == "degat"){
            is_dead();
        } 
    }


    void is_dead(){

        if(!network.inst.host){
            network.inst.player_one_is_dead = true;  
        }
        else{
            network.inst.player_two_is_dead = true;
        }
        GameObject _death = Instantiate(death_effect, transform.position, transform.rotation);
        Destroy(_death,6f); 
        Destroy(this.gameObject);      
    }





}
