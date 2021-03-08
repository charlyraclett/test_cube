using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_switch_power : MonoBehaviour{


    Animator anim;
    public string id_switch;
    public power_switch _power_switch;
    bool done;
    public bool active;
    

   


    void Start(){
        anim = GetComponent<Animator>();
    }



    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player" && !done && active){
            _power_switch.data_button(this);
            anim.SetTrigger("waiting");
            done = true;
        }
    }

   


    public void confirm_code(){
        anim.SetTrigger("good");
    }

    public void reinitialize_switch(){
        done = false;
        anim.SetTrigger("wrong");
    }

    public void set_active(){
        if(!active)
        StartCoroutine(end_active());
    }

    IEnumerator end_active(){
        active = true;
        anim.SetTrigger("waiting");
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("initialize");

    }



    

    

   
}
