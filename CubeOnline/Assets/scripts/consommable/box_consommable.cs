using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_consommable : MonoBehaviour{

    public object_box box;
    
    int nbr_rocket;
   
    [Header("Edition")]
    public GameObject box_wood;
    public GameObject circle;
    public SpriteRenderer Sprite_gfx;

    Camera cam;
    Animator animator;
    Rigidbody rb;
    bool is_grounded;
   
    void Start(){
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cam = camera_manager.inst.main_camera;
        Sprite_gfx.sprite = box.gfx;
        nbr_rocket = box.add_rocket;
    }




    void Update(){
        circle.transform.LookAt(cam.transform);
    }


    void OnTriggerEnter(Collider col){ 

        if(col.tag == "ground" && !is_grounded){
            is_grounded = true;
            animator.SetTrigger("touch_ground");
            sound_manager.inst.sound_item_box();
            rb.AddForce(transform.up * 6f, ForceMode.Impulse);
        }

        if(col.tag == "Player"){ 

            animator.SetTrigger("pick_up");
            Destroy(gameObject,0.6f);   
            sound_manager.inst.sound_pick_up(); 

            if(nbr_rocket > 0){
                add_rocket(col.gameObject.GetComponent<controller_cube>());
            }

            if(box.add_pv_player){
                add_pv_player();
            }
           
        } 
    }



    void add_rocket(controller_cube player){
        player.pick_up_box_rocket(box.add_rocket);
    }


    void add_pv_player(){ 
        if(player_manager.inst.life_player < 3){
            player_manager.inst.add_pv_player();  
        }
    }

}
