using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemies_manager : MonoBehaviour{

    public static enemies_manager inst;

    NavMeshSurface surface;
    GameObject agent; // for instiantiate

    [Header("Edition")]
    public GameObject[] nest_enemy;
    public GameObject[] enemies_prefab;
    public wall_fire[] _wall_fire;
    public wall_air wall_fan;
    public gear_floor gear_floor;
    public Transform[] position_nest;

    [Header("Info")]
    public List<GameObject> enemy_in_game = new List<GameObject>();
    
    int pos_nest = -1;
    int _position_apparition;

    void Awake(){
        inst = this;;
        Random.InitState(42);
    }

    


    public IEnumerator create_enemy_floor(float delai, int id_prefab,bool random, int id_floor){

        yield return new WaitForSeconds(delai);
       
        _position_apparition = random ? Random.Range(0,map_manager.inst.floor_list.Count) : id_floor;
        Transform base_enemy = map_manager.inst.floor_list[_position_apparition].gameObject.transform.GetChild(0);

        base_enemy.GetComponent<falling_floor>().create_enemy(enemies_prefab[id_prefab]);
      
        // base_enemy.GetComponent<Animator>().SetTrigger("alerte_floor");
        // yield return new WaitForSeconds(2f);

        // base_enemy.GetComponent<Animator>().SetTrigger("create_enemy");

        // sound_manager.inst.sound_enemy_nest(); 

        // yield return new WaitForSeconds(1f);
        // create_enemy(base_enemy, id_prefab); 
        // sound_manager.inst.sound_enemy_nest();

        // yield return new WaitForSeconds(1f);
        // agent.transform.parent = null;

        // yield return new WaitForSeconds(0.3f);
        // initialize_enemy_floor(agent);
       
    }


    void create_enemy(Transform base_enemy, int id_prefab){
        Vector3 _position = new Vector3(base_enemy.position.x, base_enemy.position.y + 1.08f,base_enemy.position.z);
        agent = Instantiate(enemies_prefab[id_prefab],_position,base_enemy.transform.rotation);
        enemy_in_game.Add(agent);
        agent.transform.parent = base_enemy.transform;
    }



    void initialize_enemy_floor(GameObject _enemy){
        if(_enemy.GetComponent<navmesh_agent>() != null){
            _enemy.GetComponent<navmesh_agent>().initialize();   
        }
        if(_enemy.GetComponent<bomb_enemy>() != null){
            _enemy.GetComponent<bomb_enemy>().initialize();   
        }  
    }


   

    public void remove_all_enemies(){
        
        StopAllCoroutines();
        print("delete_all_enemies");
        foreach(GameObject enemy in enemy_in_game){
            Destroy(enemy);
        }
        enemy_in_game.Clear();

        GameObject[] bullet_in_game = GameObject.FindGameObjectsWithTag("degat");
        foreach(GameObject _bullet in bullet_in_game){
            Destroy(_bullet);
        }  
    }



   
    


}
