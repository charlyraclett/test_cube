using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_enemy : MonoBehaviour{

    public float force_pente;
    public bool is_grounded;
    public bool side_left;
    
    AudioSource audio_source;
    public AudioClip falling;
    public AudioClip touch_ground;
    public AudioClip move_sphere;


    Vector3 right = new Vector3(1f,0,0);
    Vector3 left = new Vector3(-1f,0,0);
    Vector2 direction;

    Rigidbody rb;

    public float force_impulse = 6f;

  

   
    void Start(){
        Destroy(this.gameObject,10f);
        audio_source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        play_fx_sound(falling,1f);
       
        if(side_left){
            direction = right;
        }else{
            direction = left;
        }
    }


  

    // Update is called once per frame
    void Update(){

        if(is_grounded){
           transform.Translate(direction * Random.Range(5f, 15f) * Time.deltaTime,Space.World);
        }
    }









    void OnTriggerEnter(Collider col){ 
        if(col.tag == "ground" && !is_grounded){
           is_grounded = true;
            play_fx_sound(touch_ground,1f);
           // rb.AddForce(force_impulse,0f, 0f, ForceMode.Impulse);
        }

        else if(col.tag == "Player"){
            controller_cube cube = col.gameObject.GetComponent<controller_cube>();
            cube.controller_dead();
            Destroy(this.gameObject);
        }  
    }

    void play_fx_sound(AudioClip clip, float vol){
        audio_source.clip = clip;
        audio_source.volume = vol;
        audio_source.Play();
    }

}
