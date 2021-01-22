﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using System.Linq;

//[ExecuteInEditMode]
public class floor_creation : MonoBehaviour{

    public static floor_creation inst;

    public map _map;
    public enum map {
        map1,
        map2,
        map3,
        map4
    };

    [Header("Edition")]
    public GameObject floor;
    public Material[] my_materiels;
    public float delay_falling = 3f;
    public GameObject particule_falling_floor;

    int line;
    [HideInInspector] public int colonne;
    int[] id_floor_base = {0,0,0,0};
    [HideInInspector] public float[] angle_bases = {0,0,0,0};
    int size = 3;
    int material_base_id;
    int count = 0;

    public List<GameObject> floor_list = new List<GameObject>();
    public List<GameObject> save_list_to_destroy;
    public List<id_floor> list_id_floor = new List<id_floor>();

    int[] position_to_erase;

   
    void Start(){

        inst = this;
        Random.InitState(42); 

        switch(_map){
            case map.map1 : map1(); break;
            case map.map2 : map2(); break;
            case map.map3 : map3(); break;
            case map.map4 : map4(); break;
        }  
    } 



    void Update(){     
        if(Input.GetKeyDown(KeyCode.S)){  
            save_floor();
        } 
        if(Input.GetKeyDown(KeyCode.A)){  
            StartCoroutine(delete_all_floor());
        } 
    }

  

    public void create_floor(){ // creation base square

        int i = 0;
        
        for(int row = 0; row < line; row++){
            for(int col = 0; col < colonne; col++){

                if(!position_to_erase.Contains(i)){
                    GameObject _floor = Instantiate(floor,transform);
                    _floor.GetComponent<id_floor>().id = i;
                    float posX = col * size;
                    float posY = row * size;
                    _floor.transform.position = new Vector3(posX,0,posY); 
                    if(id_floor_base.Contains(i)){
                        create_base(_floor,i);
                    }else{
                        floor_list.Add(_floor);        
                    }
                    i++;
                }
                else{
                    i++;
                }

            }      
        } 

    }

   

    public void create_base(GameObject _floor, int id){

        _floor.GetComponentInChildren<MeshRenderer>().material = my_materiels[0];
        if(id_floor_base[0] == id){
            _floor.transform.Rotate(0,angle_bases[0],0); network.inst.bases_pos[0] = _floor.transform;
        }
        if(id_floor_base[1] == id){
            _floor.transform.Rotate(0,angle_bases[1],0); network.inst.bases_pos[1] = _floor.transform;
        }
        if(id_floor_base[2] == id){
            _floor.transform.Rotate(0,angle_bases[2],0); network.inst.bases_pos[2] = _floor.transform;
        }
        if(id_floor_base[3] == id){
            _floor.transform.Rotate(0,angle_bases[3],0); network.inst.bases_pos[3] = _floor.transform;
        }
    }




    public IEnumerator delete_floor(float delay){

        yield return new WaitForSeconds(0.02f);  
        network.inst.id_event = 0;   
        yield return new WaitForSeconds(delay);  
        int rand = Random.Range(0, floor_list.Count);
        Vector3 particule_position = new Vector3(floor_list[rand].transform.position.x, 0.6f, floor_list[rand].transform.position.z);
        Destroy(Instantiate(particule_falling_floor,particule_position,floor_list[rand].transform.rotation),5f);
        StartCoroutine(floor_list[rand].GetComponentInChildren<falling_floor>().falling());
        floor_list[rand].transform.GetChild(1).gameObject.SetActive(true); // active obstacle navmesh
        floor_list.RemoveAt(rand);
        count++;
        yield return new WaitForSeconds(delay_falling);  
        if(floor_list.Count > 0)
        StartCoroutine(delete_floor(0f)); 
        if(count == 5)
        delay_falling = 2f;
        if(count == 10)
        delay_falling = 1f;      
    }



  
    void map1(){
        line = 11;
        colonne = 11;
        int[] position = {};
        position_to_erase = position;
        angle_bases = new float[]{90f, 270f, 0f, 180f};
        id_floor_base = new int[]{65, 55, 115, 5};
        create_floor();
    }

    void map2(){
        line = 11;
        colonne = 11;
        int[] position = {27,35,38,41,46,47,48,49,50,51,52,57,58,59,61,62,63,68,69,70,71,72,73,74,79,82,85,93};
        position_to_erase = position;
        angle_bases = new float[]{90f, 270f, 0f, 180f};
        id_floor_base = new int[]{65, 55, 115, 5};
        create_floor();
    }

    void map3(){
        line = 11;
        colonne = 11;
        int[] position = {12,13,14,15,17,18,19,20,23,24,25,26,28,29,30,31,34,35,36,37,39,40,41,42,45,46,47,48,50,51,52,53,67,68,69,70,72,73,74,75,78,79,80,81,83,84,85,86,89,90,91,92,94,95,96,97,100,101,102,103,105,106,107,108};
        position_to_erase = position;
        angle_bases = new float[]{90f, 270f, 0f, 180f};
        id_floor_base = new int[]{65, 55, 115,5};
        create_floor();
    }

    void map4(){
        line = 21;
        colonne = 21;
        int[] position = {1,2,3,4,6,7,8,10,11,12,14,15,22,23,24,25,27,28,29,31,32,33,35,36,43,44,45,46,48,49,50,52,53,54,56,57,64,65,66,67,69,70,71,73,74,75,77,78,85,86,87,88,90,91,92,94,95,96,98,99,106,107,108,109,111,112,113,115,116,117,119,120,127,128,129,130,132,133,134,136,137,138,140,141,148,149,150,151,153,154,155,157,158,159,161,162,182,183,190,191,192,193,195,196,197,199,200,201,203,204,224,225,233,234,236,237,239,240,242,243,245,246,263,264,266,274,275,278,279,280,281,287,295,296,299,300,301,302,304,305,306,307,308,325,329,343,344,345,346,364,365,366,367,385,386,387,388};
        position_to_erase = position;
        angle_bases = new float[]{180f, 180f, 180f, 180f};
        id_floor_base = new int[]{0, 5, 9, 13};
        create_floor();
    }




  


    void save_floor(){ // pour edition
        string id_destroy = "" ;
        foreach(GameObject floor in floor_list){ 
            if(floor.transform.childCount == 0){
                save_list_to_destroy.Add(floor);
                id_floor my_floor = floor.GetComponent<id_floor>();
                id_destroy += my_floor.id + ",";    
            }
        }
        print(id_destroy); // a recup dans console
    }



    IEnumerator delete_all_floor(){
        int i = floor_list.Count; 
        while(i >= 1){ 
            Rigidbody rb = floor_list[i-1].GetComponentInChildren<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            Destroy(floor_list[i-1].gameObject,5f);
            i--;
            yield return new WaitForSeconds(0.1f);
        }
        floor_list.Clear();
    }



  
}
