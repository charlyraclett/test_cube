using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class collider_plane_destroy : MonoBehaviour{
   
    void OnTriggerEnter(Collider other){
        
        if(other.gameObject.tag == "enemy" || other.gameObject.tag == "bomb"){
            Destroy(other.gameObject);
            level_manager.inst.remove_enemy_in_game();
        }
    }

}
