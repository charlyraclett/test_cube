using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest_move : MonoBehaviour{

    public float speed = 10;
    public Transform target_position;
   
    void Start(){
        
    }

   
    void Update(){
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target_position.position, step);
    }














}
