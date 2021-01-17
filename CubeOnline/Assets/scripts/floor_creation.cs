using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class floor_creation : MonoBehaviour{

    public class floor_type{
        public int size_line;
        public int size_colonne;
        public int pos_base1;
        public int pos_base2;
        public int pos_base3;
        public int pos_base4;
        public int[] delete_id_floor;

        public floor_type(int _line, int _colonne, int _base1, int _base2, int _base3, int _base4){
            size_line = _line;
            size_colonne = _colonne;
            pos_base1 = _base1;
            pos_base2 = _base2;
            pos_base3 = _base3;
            pos_base4 = _base4;
        }

    }

    public map _map;
    public enum map {
        map1,
        map2,
        map3
    };




    public static floor_creation inst;

    [Header("Edition")]
    public GameObject floor;
    public Material[] my_materiels;
    public float delay_falling = 3f;

    int line;
    int colonne;
    int[] id_floor_base = {0,0,0,0};
    int size = 3;
    int material_base_id;
    int count = 0;

    public List<GameObject> floor_list = new List<GameObject>();
    public List<GameObject> save_list_to_destroy;
    public List<id_floor> list_id_floor = new List<id_floor>();

    floor_type level1 = new floor_type(11,11,64,56,104,16);

    floor_type level2 = new floor_type(11,11,64,56,104,16); 



    void Start(){
        inst = this;

        switch(_map){
            case map.map1 :  map1(); break;
            case map.map2 :  map2(); break;
            case map.map3 :  map3(); break;
        }
        Random.InitState(42); 
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
                GameObject _floor = Instantiate(floor,transform);
                _floor.GetComponent<id_floor>().id = i;
                i++;
                floor_list.Add(_floor);
                float posX = col * size;
                float posY = row * size;
                _floor.transform.position = new Vector3(posX,0,posY); 
            }      
        }  
    }


    public IEnumerator delete_floor(float delay){
        yield return new WaitForSeconds(0.02f);  
        network.inst.id_event = 0;   
        yield return new WaitForSeconds(delay);  
        int rand = Random.Range(0, floor_list.Count);
        StartCoroutine(floor_list[rand].GetComponentInChildren<falling_floor>().falling());
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



  


    void setting_floor(floor_type my_floor){
        line = my_floor.size_line;
        colonne = my_floor.size_colonne;
        id_floor_base[0] = my_floor.pos_base1;
        id_floor_base[1] = my_floor.pos_base2;
        id_floor_base[2] = my_floor.pos_base3; 
        id_floor_base[3] = my_floor.pos_base4;
        create_floor();
    }


    
    void map1(){
        line = 11;
        colonne = 11;
        int[] position_to_erase = {};
        id_floor_base[0] = 64;
        id_floor_base[1] = 56;
        id_floor_base[2] = 104; 
        id_floor_base[3] = 16;
        create_floor();
        create_bases();
        erase_floor_setting(position_to_erase);
    }

    void map2(){
        line = 11;
        colonne = 11;
        int[] position_to_erase = {27,35,38,41,46,47,48,49,50,51,52,57,58,59,61,62,63,68,69,70,71,72,73,74,79,82,85,93};
        id_floor_base[0] = 64;
        id_floor_base[1] = 56;
        id_floor_base[2] = 104; 
        id_floor_base[3] = 16;
        create_floor();
        create_bases();
        erase_floor_setting(position_to_erase);
    }

    void map3(){
        line = 11;
        colonne = 11;
        int[] position_to_erase = {12,13,14,15,17,18,19,20,23,24,25,26,28,29,30,31,34,35,36,37,39,40,41,42,45,46,47,48,50,51,52,53,67,68,69,70,72,73,74,75,78,79,80,81,83,84,85,86,89,90,91,92,94,95,96,97,100,101,102,103,105,106,107,108};
        id_floor_base[0] = 64;
        id_floor_base[1] = 56;
        id_floor_base[2] = 104; 
        id_floor_base[3] = 16;
        create_floor();
        create_bases();
        erase_floor_setting(position_to_erase);
    }












    public void create_bases(){

        for (int i = 0; i < id_floor_base.Length;i++){
            floor_list[id_floor_base[i]].GetComponentInChildren<MeshRenderer>().material = my_materiels[0];
        }

        floor_list[id_floor_base[0]].transform.Rotate(0,-90,0);
        floor_list[id_floor_base[1]].transform.Rotate(0,90,0);
        floor_list[id_floor_base[2]].transform.Rotate(0,180,0);
      
        for (int i = 0; i < network.inst.bases_pos.Length;i++){
            network.inst.bases_pos[i] = floor_list[id_floor_base[i]].transform;
        }

        floor_list.RemoveAt(id_floor_base[2]);  
        floor_list.RemoveAt(id_floor_base[0]);
        floor_list.RemoveAt(id_floor_base[1]);
        floor_list.RemoveAt(id_floor_base[3]);
    }


    void erase_floor_setting(int[] pos){

        foreach(GameObject _id_floor in floor_list){
            list_id_floor.Add(_id_floor.GetComponent<id_floor>());
        }
       
        for (int i = 0; i < pos.Length; i++){
            foreach(id_floor floor in list_id_floor){
                if(floor.id.Equals(pos[i])){
                    floor_list.Remove(floor.gameObject);
                    Destroy(floor.gameObject);
                }
            }
        }
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
        print(id_destroy); 
    }



    IEnumerator delete_all_floor(){
        int i = floor_list.Count-1; 
        while(i >= 0){
          //  Destroy(floor_list[i].gameObject);
            Rigidbody rb = floor_list[i].GetComponentInChildren<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            floor_list.RemoveAt(i);
            i--;
            yield return new WaitForSeconds(0.1f);
        }

        list_id_floor.Clear();
    }



  
}
