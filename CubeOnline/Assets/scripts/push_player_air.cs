using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class push_player_air : MonoBehaviour{


    float timer = 0f;
    AudioSource audio_source;
    public GameObject impact_effect;
    
    void Start(){
        audio_source = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player"){
            StartCoroutine(destroy_player(col));  
        }
    }



    IEnumerator destroy_player(Collider col){

        controller_cube player = col.GetComponent<controller_cube>();
        if(player.invulnerable)
        yield break;
        player.host = false;
        interactable_manager.inst.quit_aera_interactable(); 
        Rigidbody player_rb = col.GetComponent<Rigidbody>();
        audio_source.Play();
        Instantiate(impact_effect,col.transform.position,col.transform.rotation);
        Vector3 dir = (col.transform.position - transform.position);
        dir.Normalize();  
        
        player_rb.constraints = RigidbodyConstraints.None;
        player_rb.velocity += Vector3.up * 20f;
        player_rb.velocity += dir * 20f;
    
        timer = 0f;
        while(timer < 5f){
            player_rb.AddTorque (transform.right * 600f);
            timer += Time.deltaTime * 10f;   
            yield return null;
        }

        player.controller_dead();
       
    }
}
