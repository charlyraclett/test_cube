using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour{


    public GameObject impact;
    public int id_player_shoot;

   
    void OnTriggerEnter(Collider col){ 

        if(col.tag == "ground" || col.tag == "Player"){
            
            GameObject _impact = Instantiate(impact,transform.position,transform.rotation);
            _impact.GetComponent<ParticleSystem>().Play();
            Destroy(_impact,4f);
            Destroy(gameObject);
        } 
    }
}
