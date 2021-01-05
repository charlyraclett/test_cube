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



    public void controller_dead(){
        GameObject _death = Instantiate(death_effect, transform.position, transform.rotation);
        Destroy(_death,6f); 
        is_dead = 1;
        Destroy(this.gameObject);      
    }

}
