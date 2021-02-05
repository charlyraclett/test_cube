using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flame_collider : MonoBehaviour{



    void OnParticleCollision(GameObject objet){

        if(objet.transform.tag == "Player"){
            objet.GetComponent<controller_cube>().controller_dead();
        }
    }

}
