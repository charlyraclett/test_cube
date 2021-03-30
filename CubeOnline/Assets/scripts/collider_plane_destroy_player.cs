using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class collider_plane_destroy_player : MonoBehaviour{
   
    void OnTriggerEnter(Collider col){
        
        if(col.gameObject.tag == "Player"){
           controller_cube player = col.GetComponent<controller_cube>();
           dev_script.inst.invincible = false;
           player.invulnerable = false;
           player.controller_dead();
        }
    }

}
