using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_floor : MonoBehaviour{


   
   Animator anim;
   Material mymat;
   public GameObject obstacle_navmesh;
  
  
    void Start(){   
        anim = GetComponent<Animator>();  
    }

    // trigger map_manager
    public void falling(bool value){
        anim.SetBool("falling",value);
        obstacle_navmesh.SetActive(value); // active obstacle navmesh  
    }

    // trigger map_manager
    public void anim_initial_position(bool value){
        anim.SetBool("in_game",value);
    }

    public void alert_floor_red(){
        anim.SetTrigger("alerte_floor");
    }


    public void standby_for_intro(bool value){
        anim.SetBool("active_anim_intro",value) ; 
    }

}
