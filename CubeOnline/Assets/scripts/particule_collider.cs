using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particule_collider : MonoBehaviour{

    public int id_player_bullet;
    SphereCollider col;


    void Start(){
        col = GetComponent<SphereCollider>();
        StartCoroutine(size_collider());
    }

    

    void OnTriggerEnter(Collider col){

        if(col.tag == "Player"){
            controller_cube cube = col.gameObject.GetComponent<controller_cube>();
            cube.controller_dead();
            if(id_player_bullet != cube.id_avatar){
                ui_manager.inst.add_score_for(id_player_bullet);
            }
            StopAllCoroutines();
            Destroy(this);
        }
        else if(col.tag == "enemy"){
            col.gameObject.GetComponent<navmesh_agent>().agent_dead();
            ui_manager.inst.add_score_for(id_player_bullet);
            StopAllCoroutines();
            Destroy(this);
        }
    }



    IEnumerator size_collider(){

        float elapsed = 0.0f;
        float duree = 1f; 
       
        while( elapsed < duree ){
            col.radius = Mathf.Lerp(0f,40f,elapsed / duree);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;
        duree = 4f; 

        while( elapsed < duree ){
            col.radius = Mathf.Lerp(40f,0f,elapsed / duree);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(this);


    }
}
