using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_floor : MonoBehaviour{


   Rigidbody rb;
   Animator anim;


    void Start(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    public IEnumerator falling(){
        sound_manager.inst.sound_rocks();
        anim.SetTrigger("shake");
        yield return new WaitForSeconds(2f);  
        rb.isKinematic = false;
        rb.useGravity = true;
        Destroy(gameObject,5f);
    }





   
}
