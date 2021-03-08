using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consommable_manager : MonoBehaviour{

    public static consommable_manager inst;


    
    public GameObject[] box;



    void Start(){
        inst = this;
    }



    public IEnumerator create_box(float delay, int id_box){

        yield return new WaitForSeconds(delay);
        Transform point_map = map_manager.inst.floor_list[Random.Range(0,map_manager.inst.floor_list.Count)].gameObject.transform.GetChild(0);
        Vector3 _position = new Vector3(point_map.position.x, 30f,point_map.position.z);
        GameObject bb = Instantiate(box[id_box], _position, point_map.rotation);
    }




    public IEnumerator random_box(){

        float random = Random.Range(10,20);

        yield return new WaitForSeconds(random);

        int id_box = Random.Range(0,box.Length);

        StartCoroutine(create_box(0.1f,id_box));
    }



    
}
