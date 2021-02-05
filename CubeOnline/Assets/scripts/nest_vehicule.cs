using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nest_vehicule : MonoBehaviour{

    [Header("Edition")]
    public GameObject[] prefab_enemy;
    public Transform[] location_creation;
    public GameObject[] leds;
    [Header("Edition")]
    public GameObject[] enemies_create;
    int id;
   


    void Start(){
      //  intialize_nest_container();
    }

    // trigger enemies_manager
    public void active_container_enemies(){
        StartCoroutine(active_enemy_in_nest());
    }




    IEnumerator active_enemy_in_nest(){

        for(id = 0; id < enemies_create.Length;id++){

            if(enemies_create[id].GetComponent<navmesh_agent>() != null){
                quit_nest(enemies_create[id],id);
                yield return new WaitForSeconds(0.7f); // time_falling
                enemies_create[id].GetComponent<navmesh_agent>().initialize();     
            }

            if(enemies_create[id].GetComponent<bomb_enemy>() != null){
                quit_nest(enemies_create[id],id);
                yield return new WaitForSeconds(0.7f);
                enemies_create[id].GetComponent<bomb_enemy>().initialize();     
            }  
            yield return new WaitForSeconds(1f);
        }          
    }


    void quit_nest(GameObject _enemy, int id){
        Material mymat = leds[id].GetComponent<Renderer>().material;
        mymat.SetColor("_EmissionColor", Color.green);
        sound_manager.inst.sound_nest_activate();
        Rigidbody rb_enemy =  _enemy.GetComponent<Rigidbody>();
        _enemy.transform.parent = null;
        rb_enemy.useGravity = true;
        rb_enemy.isKinematic = false;
        enemies_manager.inst.enemy_in_game.Add(_enemy.gameObject);
        id++;
    }


   
    public void intialize_nest_container(GameObject one, GameObject two, GameObject three){
        GameObject[] prefabs_enemy = {one, two ,three};
        for(int i = 0; i < prefabs_enemy.Length; i++){ 
            enemies_create[i] = Instantiate(prefabs_enemy[i],location_creation[i].position,location_creation[i].rotation);
            enemies_create[i].transform.parent = location_creation[i];   
        }
    }

    























}
