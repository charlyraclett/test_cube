using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class turret_laser : MonoBehaviour{


    Animator anim;
    AudioSource audio_source;
    public AudioClip laser_sound;
    public AudioClip move_in_game;
    public AudioClip alerte_sound;
    public AudioClip laser_move;

    public Light _light;

    public bool active_raycast;
    float lenght_laser = 0f;

    public float distance_laser;

    public Transform canon_laser_one;
    public Transform canon_laser_two;

    public VolumetricLines.VolumetricLineBehavior laser1;
    public VolumetricLines.VolumetricLineBehavior laser2;
    public GameObject laser_object1;
    public GameObject laser_object2;

   
    void Start(){
        anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>(); 
    }

    // trigger level2 behaviour
    public void active_turret(){
        StartCoroutine(start_turret());
    }



    IEnumerator start_turret(){

        anim.SetBool("in_game",true);
        sound_fx_play(move_in_game,1f);

        StartCoroutine("check_laser_collison");

        yield return new WaitForSeconds(2f);
        sound_fx_play(alerte_sound,1f);
        _light.enabled = true;

        yield return new WaitForSeconds(1f);
        StartCoroutine(set_beam_laser(6f,distance_laser));  
        yield return new WaitForSeconds(7f);
        anim.SetTrigger("move_laser");
        audio_source.PlayOneShot(laser_move,1f);      
    }

  
    IEnumerator check_laser_collison(){

        while(true){
            Vector3 origin = canon_laser_one.position;
            Vector3 direction = canon_laser_one.forward;
            Ray ray_one = new Ray(origin,direction);

            Vector3 origin2 = canon_laser_two.position;
            Vector3 direction2 = canon_laser_two.forward;
            Ray ray_two = new Ray(origin2,direction2);

            
            if(Physics.Raycast(ray_one, out RaycastHit raycastHit,lenght_laser)){
                if(raycastHit.transform.tag == "Player"){
                    destroy_player(raycastHit.transform.gameObject);
                    Debug.DrawRay(canon_laser_one.position, canon_laser_one.TransformDirection(Vector3.forward) * lenght_laser, Color.green);
                }
            }
            else{
                Debug.DrawRay(canon_laser_one.position, canon_laser_one.TransformDirection(Vector3.forward) * lenght_laser, Color.white);
            }
        

            if(Physics.Raycast(ray_two, out RaycastHit raycastHit2,lenght_laser)){
                if(raycastHit2.transform.tag == "Player"){
                    destroy_player(raycastHit2.transform.gameObject);
                    Debug.DrawRay(canon_laser_two.position, canon_laser_two.TransformDirection(Vector3.forward) * lenght_laser, Color.green);
                }
            }
            else{
                Debug.DrawRay(canon_laser_two.position, canon_laser_two.TransformDirection(Vector3.forward) * lenght_laser, Color.white);
            }

            yield return null;
        }

    }





    void destroy_player(GameObject target){  
        controller_cube player = target.GetComponent<controller_cube>();
        player.controller_dead();
    }



    IEnumerator set_beam_laser(float duration, float target_lenght){

        laser_object1.SetActive(true);
        laser_object2.SetActive(true);

        sound_fx_play(laser_sound,0.6f);

        float currentTime = 0f;
        float value = lenght_laser;
        float targetValue = target_lenght;

        while (currentTime < duration){
            currentTime += Time.deltaTime;
            float new_lenght = Mathf.Lerp(value, targetValue, currentTime / duration);
            lenght_laser = new_lenght;
            laser1.StartPos = new Vector3(0,new_lenght,0);
            laser2.StartPos = new Vector3(0,new_lenght,0);
            yield return null;
        }

        lenght_laser = target_lenght;
        yield break;
    }




    void sound_fx_play(AudioClip _clip,float vol){
        audio_source.clip = _clip;
        audio_source.volume = vol;
        audio_source.Play();
    }

    // trigger anim
    void end_move(){
        StartCoroutine(end_laser());  
    }


    IEnumerator end_laser(){
        StopCoroutine("check_laser_collison");
        StartCoroutine(set_beam_laser(0.5f,0f));  //1f
        yield return new WaitForSeconds(0.5f);
        laser_object1.SetActive(false);
        laser_object2.SetActive(false);
        audio_source.Stop();
        yield return new WaitForSeconds(1f);
        sound_fx_play(alerte_sound,1f);
        _light.enabled = false;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("in_game",false);
        sound_fx_play(move_in_game,1f);
        level_manager.inst.remove_enemy_in_game(); 
    }
}
