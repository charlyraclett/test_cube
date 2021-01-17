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
    public bool type_canon;
    bool shoot_running;
    Rigidbody rb;
    public bool is_moving;

    public bool key_holding;
    public bool is_grounded;

    List<KeyCode> pressedInput = new List<KeyCode>();

    void Start(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        pressedInput.Add(KeyCode.UpArrow);
        pressedInput.Add(KeyCode.DownArrow);
        pressedInput.Add(KeyCode.LeftArrow);
        pressedInput.Add(KeyCode.RightArrow);
    }
    

    void Update(){


        if (rb.velocity.y < -15f){
            controller_dead();
        }

        if(host){

            if(Input.GetKey(KeyCode.UpArrow)){  
                this.transform.Translate(Vector3.forward * Time.deltaTime * speed); 
                key_holding = true;
                sound_manager.inst.sound_move();    
            }  
            
            if(Input.GetKey(KeyCode.DownArrow)){  
                this.transform.Translate(Vector3.back * Time.deltaTime * speed);  
                key_holding = true;
                sound_manager.inst.sound_move();    
            }  
            
            if(Input.GetKey(KeyCode.LeftArrow)){    
                this.transform.Rotate(Vector3.up,-speed_rotation); 
                key_holding = true;
                sound_manager.inst.sound_move();     
            }  
            
            if(Input.GetKey(KeyCode.RightArrow)){  
                this.transform.Rotate(Vector3.up,speed_rotation);
                key_holding = true;
                sound_manager.inst.sound_move();     
            } 
           

            if(!Input.anyKey && key_holding) {
                key_holding = false;
                sound_manager.inst.audio_source_move.Stop();     
            }
 

           
            if (Input.GetKeyDown(KeyCode.Space)){ 

                if(shoot_running)
                return;

                bullet.GetComponent<bullet>().id_player_shoot = id_avatar;
                shoot_running = true;
        
                if(type_catapulte){
                    StartCoroutine(shoot_catapult());
                }
                else if(type_canon){
                    StartCoroutine(shoot3());
                }
                else{
                    StartCoroutine(shoot());
                }
            }
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
            Destroy(_bullet, 5f);
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0; // network
            yield return new WaitForSeconds(0.6f);
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
            _bullet.GetComponent<Rigidbody>().useGravity = false;
            _bullet.GetComponent<bullet>().canon = true;
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0;
            yield return new WaitForSeconds(1.5f);
            shoot_running = false;
        }
    }

    // trigger anim
    public IEnumerator shoot_catapult(){

        anim.SetTrigger("shoot_two");
        yield return new WaitForSeconds(0.07f);
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
        shoot_running = false;
    }


    // trigger bullet
    public void controller_dead(){
        GameObject _death = Instantiate(death_effect, transform.position, transform.rotation);
        Destroy(_death,6f); 
        is_dead = 1;
        sound_manager.inst.sound_death_player();
        if(host){
            network.inst.player_dead();
        }
        Destroy(this.gameObject);      
    }





    void OnTriggerStay(Collider col){
        if(col.tag == "ground"){
            is_grounded = true;
        }
    }

    void OnTriggerExit(Collider col){
        if(col.tag == "ground"){
            sound_manager.inst.audio_source_move.Stop();     
            is_grounded = false;
        }  
    }


}
