using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour{


    public GameObject impact;
    public int id_player_shoot;

   
    void OnTriggerEnter(Collider col){ 

        if(col.tag == "ground"){
            bullet_death();   
        }

        else if(col.tag == "Player"){
            print("touch player");
            col.gameObject.GetComponent<controller_cube>().controller_dead();
            network.inst.add_score_for(id_player_shoot);
            bullet_death();
        }
    }


    void bullet_death(){

        GameObject _impact = Instantiate(impact,transform.position,transform.rotation);
        _impact.GetComponent<ParticleSystem>().Play();
        Destroy(_impact,4f);  
        Destroy(gameObject);
    }




}
