using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_follow_player : MonoBehaviour{


    public Transform target_player;
    public Vector3 offset;
   

   
    void Update(){

        if(player_manager.inst.my_avatar != null){
            target_player = player_manager.inst.my_avatar.transform;
            Vector3 pos = Camera.main.WorldToScreenPoint(target_player.position);
            this.gameObject.transform.position = pos + offset;
        }


      

        
    }
}
