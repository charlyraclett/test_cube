using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class event_manager : MonoBehaviour{

    public static event_manager inst;
    NavMeshSurface surface;
    GameObject agent;


    public GameObject agent_enemy;



       
    void Start(){
        inst = this;;
        Random.InitState(24);
        Invoke("nav",1f); 
    }

   
    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){  
            StartCoroutine(move_up_floor());
        }   
    }


    void nav(){
        surface  = gameObject.AddComponent<NavMeshSurface>();
        surface.BuildNavMesh(); 
    }


    IEnumerator move_up_floor(){

        int rand = Random.Range(0,floor_creation.inst.floor_list.Count); // nest random
        GameObject base_enemy = floor_creation.inst.floor_list[rand];

        float elapsed = 0f;
        float duree = 1f; 
        float height = 0f;

        sound_manager.inst.sound_enemy_nest();
        
        while( elapsed < duree ){ // down
            height = Mathf.Lerp(0f,-7f,elapsed / duree);
            base_enemy.transform.position = new Vector3(base_enemy.transform.position.x, height, base_enemy.transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        height = -7f;

        create_enemy(base_enemy); 
        sound_manager.inst.sound_enemy_nest();

        elapsed = 0f;
        duree = 1f;
       
        while( elapsed < duree ){ //up
            height = Mathf.Lerp(-7,0f,elapsed / duree);
            base_enemy.transform.position = new Vector3(base_enemy.transform.position.x, height, base_enemy.transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        height = 0f;

        agent.transform.parent = null;
        yield return new WaitForSeconds(0.3f);
        agent.GetComponent<navmesh_agent>().intialize();   
    }




    public void create_enemy(GameObject base_enemy){
        Vector3 _position = new Vector3(base_enemy.transform.position.x, base_enemy.transform.position.y + 1.08f,base_enemy.transform.position.z);
        agent = Instantiate(agent_enemy,_position,base_enemy.transform.rotation);
        agent.transform.parent = base_enemy.transform;
    }





}
