using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consommable_manager : MonoBehaviour{

    public static consommable_manager inst;


    
    public GameObject[] box;



    void Start(){
        inst = this;
    }



    public void create_box(int id_box){
        Transform point_map = map_manager.inst.floor_list[Random.Range(0,map_manager.inst.floor_list.Count)].gameObject.transform.GetChild(0);
        Vector3 _position = new Vector3(point_map.position.x, 30f,point_map.position.z);
        GameObject bb = Instantiate(box[id_box], _position, point_map.rotation);
    }



    
}
