using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateform_move : MonoBehaviour{


    public Animator anim;
    public bool is_moving;
    public Material material;
    bool toggle;


    void Start(){
        anim = GetComponentInParent<Animator>();
        StartCoroutine(show_plateform());
    }



    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player"){  
            interactable_manager.inst.player_is_in_area_interactable(this);
            col.gameObject.transform.parent = this.transform; 
        }
       
    }


    
    void OnTriggerExit(Collider col){ 
        if(col.tag == "Player"){   
            col.gameObject.transform.parent = null;  
            interactable_manager.inst.quit_aera_interactable();
        }
    }


  
    // trigger gamepad_manager
    public void active_plateform(){
        if(is_moving)
        return;
        toggle = !toggle;
        anim.SetBool("move",toggle);
        is_moving = true;
        Invoke("ready_to_move",2f);
        GetComponent<BoxCollider>().enabled = false;

    }

    void ready_to_move(){
        is_moving = false;
        GetComponent<BoxCollider>().enabled = true;
    }

    // trigger level_manager
    public void initiale_position(){
        anim.SetBool("move",false);
        toggle = false;
    }


    IEnumerator show_plateform(){

        yield return new WaitForSeconds(3f);
        GetComponent<MeshRenderer>().enabled = true;
        float elapsed = 0.0f;
        float duree = 2f;
        float alpha = 0f;

        Color color = material.color;
       
        while( elapsed < duree ){
            alpha = Mathf.Lerp(0f,1f,elapsed / duree);
            color.a = alpha;
            material.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }
        
    }
 


    

}
