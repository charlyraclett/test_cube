using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatar_network : MonoBehaviour{


    public float speed = 5;
    public float speed_rotation = 1;
    public Transform origin_shoot;
    public GameObject bullet;
    public float force_shoot;
   
   

    void shoot(){

        GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
        Rigidbody rb = _bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * force_shoot);
        Destroy(_bullet, 5f);
    }



}
