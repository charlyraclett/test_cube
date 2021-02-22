using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turret_enemy : MonoBehaviour{

    AudioSource audio_source;
    Animator anim;

    [Header("Characteristic")]
    public int nbr_salve;
    public float speed_bullet;
    public float speed_rotation;
    public float cadence_canon;
    bool shoot;
    float value_meter = 0f;
   

    [Header("Edition")]
    public Transform head;
    public Transform origin_shoot;
    public GameObject bullet_turret;
    public Transform target;
    public Slider bar_slider;
    public ParticleSystem shoot_effect;
    public Transform lookup;
    public Gradient gradient;
    public Image fill;
    public collider_switch_turret switch1;
    public collider_switch_turret switch2;

    [Header("Sound")]
    public AudioClip reload;
    public AudioClip shoot_sound;
    public AudioClip reload_full;
    public AudioClip move_head;
    public AudioClip move_turret;
    public AudioClip alert;
  
  
    void Start(){
        audio_source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        value_meter = 0f;
        bar_slider.value = value_meter; 
        fill.color = gradient.Evaluate(0f);
    }



    // trigger level_two_behaviour
    public void start_turret(float delay){  
        StartCoroutine(go_start_turret(delay));
    }

    IEnumerator go_start_turret(float delay){
        reinitialize();
        yield return new WaitForSeconds(delay);
        anim.SetBool("in_game",true);
        audio_source.PlayOneShot(move_turret,1f);
        StartCoroutine(reload_turret(3f,4f,100f));  
        StartCoroutine(check_switch());  
    }

    IEnumerator check_switch(){
        bool check = true;

        while(check){
            if(switch1.switch_is_active && switch2.switch_is_active){
               check = false;
            }   
            yield return new WaitForSeconds(0.1f);
        }
        end_turret();    
    }






    IEnumerator look_player(){


        try{Transform target = player_manager.inst.my_avatar.transform;  
        }
        catch{
            target = null;
        }

        while(true){ 
            if(shoot){
                if(target != null){
                    head.LookAt(target);   
                } 
                else{  
                    shoot = false;
                    StopAllCoroutines();
                    StartCoroutine(check_switch());
                    StartCoroutine(DoRotationAtTargetDirection(0f,lookup));
                }   
            }
            yield return null;
        }   
    }


    public void end_turret(){
        StopAllCoroutines();
        audio_source.Stop();
        Invoke("end",1f); 
    }

    void end(){
        anim.SetBool("in_game",false);
        audio_source.PlayOneShot(move_turret,1f);
        level_manager.inst.remove_enemy_in_game(); 
    }



    void reinitialize(){  
        value_meter = 0f;
        bar_slider.value = value_meter; 
        fill.color = gradient.Evaluate(0f);
        switch1.reinitilize();
        switch2.reinitilize();
    }



    IEnumerator is_shooting(){

        while(value_meter > 0 && target != null){   
            anim.SetTrigger("shoot");   
            shoot_effect.Play();
            value_meter = value_meter - 20;
            bar_slider.value = value_meter;
            fill.color = gradient.Evaluate(bar_slider.normalizedValue);
            audio_source.PlayOneShot(shoot_sound,1f);
            GameObject _bullet = Instantiate(bullet_turret, origin_shoot.position, origin_shoot.rotation);
            bullet_enemy bullet_enemy= _bullet.GetComponent<bullet_enemy>();
            setting_bullet(bullet_enemy);
            yield return new WaitForSeconds(cadence_canon);  
        }
        StartCoroutine(DoRotationAtTargetDirection(0f,lookup));
        shoot = false;
    }




    void setting_bullet(bullet_enemy _bullet){
        _bullet.GetComponent<Rigidbody>().useGravity = false;
        _bullet.canon = true;
        _bullet.speed = speed_bullet;
    }



   
    IEnumerator reload_turret(float delay, float duration, float target_value){
        yield return new WaitForSeconds(delay);  

        while(value_meter < 90){
            value_meter = value_meter + 10;
            bar_slider.value = value_meter;
            fill.color = gradient.Evaluate(bar_slider.normalizedValue);
            audio_source.PlayOneShot(reload,0.7f);  
            yield return new WaitForSeconds(0.5f); 
        }
        value_meter = 100;
        bar_slider.value = value_meter;
        fill.color = gradient.Evaluate(bar_slider.normalizedValue);
        audio_source.PlayOneShot(reload_full,1f);

        try{ target = player_manager.inst.my_avatar.transform;

            StartCoroutine(DoRotationAtTargetDirection(0f,target)); 
        }
        catch{
            StartCoroutine(wait_player_alive());
        }
       
    }

    IEnumerator wait_player_alive(){
        StartCoroutine(DoRotationAtTargetDirection(0f,lookup));
        yield return new WaitForSeconds(3f);

    }





    IEnumerator DoRotationAtTargetDirection(float delai, Transform target_look){
        yield return new WaitForSeconds(delai); 
        Quaternion targetRotation = Quaternion.identity;
        audio_source.PlayOneShot(move_head,1f);
     
        do{
            Vector3 targetDirection = (target_look.position - head.transform.position).normalized;
            targetRotation = Quaternion.LookRotation(targetDirection);
            head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, targetRotation, Time.deltaTime * speed_rotation);
            yield return null;
        } while(Quaternion.Angle(head.transform.rotation, targetRotation) > 0.01f && target_look != null);

        audio_source.Stop();

        if(target_look != lookup){
            shoot = true;
            StartCoroutine(look_player());
            StartCoroutine(is_shooting());  
        }else{
            StartCoroutine(reload_turret(0.5f,4f,100f));
        }
    }





    // trigger anim
    void alerte_sound(){
        audio_source.PlayOneShot(alert,0.7f);
    }


    public void stop_turret(){
        StopAllCoroutines();
        anim.SetBool("in_game",false);
    }


}
