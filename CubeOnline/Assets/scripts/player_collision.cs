using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_collision : MonoBehaviour{

    void OnCollisionEnter(Collision col){


        print(col.collider.tag);
    }
}
