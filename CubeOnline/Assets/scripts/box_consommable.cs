using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_consommable : MonoBehaviour{


    public bool is_pv_player;
    public bool is_rocket;
    public int nbr_rocket = 4;
    public int nbr_pv_player = 1;

    [Header("Edition")]
    public GameObject box_wood;
    public GameObject circle;

    Camera cam;
    Animator animator;
    Rigidbody rb;
    bool is_grounded;
   
    void Start(){
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cam = camera_manager.inst.main_camera;
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

            if(is_rocket) 
            add_rocket(col.gameObject.GetComponent<controller_cube>());

            if(is_pv_player)
            add_pv_player();
           
        } 
    }



    void add_rocket(controller_cube player){
        player.pick_up_box_rocket(nbr_rocket);
    }


    void add_pv_player(){ 
        if(player_manager.inst.life_player < 3){
            player_manager.inst.life_player++;
            player_manager.inst.pv_player_ui.SetInteger("pv_player",player_manager.inst.life_player);   
        }
    }

}
