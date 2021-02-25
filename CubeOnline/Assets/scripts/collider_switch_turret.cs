using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_switch_turret : MonoBehaviour{


    public Animator animator;
    public bool switch_is_active;
    
   

    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player" && !switch_is_active){
            interactable_manager.inst.player_is_in_area_interactable(this);  
            animator.SetBool("on_enter",true);
        }
    }

    void OnTriggerExit(Collider col){ 
        if(col.tag == "Player"){   
            interactable_manager.inst.quit_aera_interactable();
            if(switch_is_active)
            return;
            animator.SetBool("on_enter",false);
        }
    }


    public void active_switch(){
        switch_is_active = true;
        animator.SetBool("activate",true);
        animator.SetBool("on_enter",false);
    }

    public void reinitilize(){
        switch_is_active = false;
        animator.SetBool("activate",false);
        animator.SetBool("on_enter",false);
    }



  
      
    
}
