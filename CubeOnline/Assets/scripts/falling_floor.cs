using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_floor : MonoBehaviour{


   
   Animator anim;
   Material mymat;
   public GameObject obstacle_navmesh;

   public GameObject spwan_objet;
  
  
    void Start(){   
        anim = GetComponent<Animator>();  
    }

    // trigger map_manager
    public void falling(bool value){
        anim.SetBool("falling",value);
        obstacle_navmesh.SetActive(value); // active obstacle navmesh  
    }

    // trigger map_manager
    public void anim_initial_position(bool value){
        anim.SetBool("in_game",value);
    }

    public void alert_floor_red(){
        anim.SetTrigger("alerte_floor");
    }


    public void standby_for_intro(bool value){
        anim.SetBool("active_anim_intro",value) ; 
    }


    // trigger enemies manager
    public void create_enemy(GameObject enemy){
        StartCoroutine(spawn_enemy(enemy));
    }

    IEnumerator spawn_enemy(GameObject enemy){

        anim.SetTrigger("alerte_floor");
        yield return new WaitForSeconds(2f);

        anim.SetTrigger("create_enemy");
        sound_manager.inst.sound_enemy_nest(); 

        yield return new WaitForSeconds(1f);
        
        Vector3 _position = new Vector3(transform.position.x, transform.position.y + 1.08f,transform.position.z);
        GameObject enemyagent = Instantiate(enemy,_position,transform.transform.rotation);
        enemies_manager.inst.enemy_in_game.Add(enemyagent);
        enemyagent.transform.parent = transform;
        sound_manager.inst.sound_enemy_nest();

        yield return new WaitForSeconds(1f);
        enemyagent.transform.parent = null;

        yield return new WaitForSeconds(0.3f);
        initialize_enemy_floor(enemyagent);

    }

    void initialize_enemy_floor(GameObject enemyagent){
        if(enemyagent.GetComponent<navmesh_agent>() != null){
            enemyagent.GetComponent<navmesh_agent>().initialize();   
        }
        if(enemyagent.GetComponent<bomb_enemy>() != null){
            enemyagent.GetComponent<bomb_enemy>().initialize();   
        }  
    }


   

}
