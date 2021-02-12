using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateform_move : MonoBehaviour{





    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player"){   
            col.gameObject.transform.parent = this.transform;   
        }
    }


    void OnTriggerExit(Collider col){ 
        if(col.tag == "Player"){   
            col.gameObject.transform.parent = null;   
        }
    }
}
