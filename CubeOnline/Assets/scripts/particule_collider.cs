using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particule_collider : MonoBehaviour{

    public int id_player_shoot;
    SphereCollider col;


    void Start(){
        col = GetComponent<SphereCollider>();
        StartCoroutine(size_collider());
    }

    

    void OnTriggerEnter(Collider col){

        if(col.tag == "Player"){
            controller_cube cube = col.gameObject.GetComponent<controller_cube>();
            cube.controller_dead();
            if(id_player_shoot != cube.id_avatar){ 
                StartCoroutine(player_manager.inst.refresh_score(id_player_shoot,0));
            }
            StopAllCoroutines();
            Destroy(this);
        }

        else if(col.tag == "enemy"){ 
            StartCoroutine(player_manager.inst.refresh_score(id_player_shoot,0));
            navmesh_agent enemy = col.gameObject.GetComponent<navmesh_agent>();
            enemy.agent_dead();
            StopAllCoroutines();
            Destroy(this);
        }

        else if(col.tag == "bomb"){  
            StartCoroutine(player_manager.inst.refresh_score(id_player_shoot,0));
            bomb_enemy bomb = col.gameObject.GetComponent<bomb_enemy>();
            bomb.agent_dead();
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
        col.radius = 0f;

        Destroy(this);


    }
}
