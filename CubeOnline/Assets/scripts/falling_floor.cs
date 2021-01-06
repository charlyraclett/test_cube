using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_floor : MonoBehaviour{


   Rigidbody rb;
   ParticleSystem effect_particule;
   Animator anim;



   
    void Start(){

        rb = GetComponent<Rigidbody>();
        effect_particule = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
    }


    public IEnumerator falling(){

        sound_manager.inst.sound_rocks();
        anim.SetTrigger("shake");
        effect_particule.Play();
        yield return new WaitForSeconds(2f);  
        rb.isKinematic = false;
        rb.useGravity = true;
        Destroy(gameObject,3f);
    }





   
}
