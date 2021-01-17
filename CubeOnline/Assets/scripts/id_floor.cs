using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class id_floor : MonoBehaviour
{


public int id = 0;
public int id_base;
public bool destroy;

[HideInInspector] public bool destroy_this;


    void Update(){

        destroy_this = destroy;
    }
   
}
