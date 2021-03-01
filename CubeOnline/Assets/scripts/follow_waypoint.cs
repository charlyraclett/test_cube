using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class follow_waypoint : MonoBehaviour{

    Transform target;
    NavMeshAgent agent;
    public Transform[] destinations;
    int id_walk_point = 0;

    [Header("Characteristic")]
    public float speed = 15f;
    public bool can_move;

    
   
    public void initialize_electron(){
        agent = GetComponent<NavMeshAgent>();   
        agent.speed = speed;
        StartCoroutine(start_agent());
    }


    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player" ){
           controller_cube player = col.GetComponent<controller_cube>();
           player.invulnerable = false;
           player.controller_dead();
        }
    }



    IEnumerator start_agent(){

        agent.SetDestination(destinations[id_walk_point].position);

        while(true){

            if(Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance){
                id_walk_point++;
                if(id_walk_point == destinations.Length){
                    id_walk_point = 0;
                }
                agent.SetDestination(destinations[id_walk_point].position); 
            }
            yield return new WaitForSeconds(0.1f);
        }
    }        


    
    

}
