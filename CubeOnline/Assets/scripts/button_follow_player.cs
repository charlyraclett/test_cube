using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_follow_player : MonoBehaviour{


    public Transform target_player;
    public Vector3 offset;   
    public Animator animator;
   
    // trigger ui manager
    public void start_follow(){
       StartCoroutine(follow_player());
    }


    // trigger ui manager
    public void stop_follow(){
       StopAllCoroutines();
    }


    IEnumerator follow_player(){

        target_player = player_manager.inst.my_avatar.transform;
        
        while(target_player != null){
            target_player = player_manager.inst.my_avatar.transform;
            Vector3 pos = Camera.main.WorldToScreenPoint(target_player.position);
            this.gameObject.transform.position = pos + offset;
            yield return null;
        }

        animator.SetBool("show_buttonA",false);
    }


    


  
}
