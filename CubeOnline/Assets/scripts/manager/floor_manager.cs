using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using System.Linq;


//[ExecuteInEditMode]
//[ExecuteAlways]
public class floor_manager : MonoBehaviour{

    public static floor_manager inst;

    public bool go_create_floor;

    [TextArea(3,20)]
    public string Edition;

    //[Header("Edition")]
    public GameObject floor;
    public Material[] my_materiels;
    public float delay_falling = 3f;
    public GameObject particule_falling_floor;

    public int line;
    public int colonne;
    int[] id_floor_base = {0,0,0,0};
    [HideInInspector] public float[] angle_bases = {0,0,0,0};
    int size = 3;
    int material_base_id;
    int count = 0;

    public List<GameObject> floor_list = new List<GameObject>();
    

    int[] position_to_erase;

   
    void Start(){
     
        if(colonne == 0 || line == 0 ){
            print("entrez les dimensions d'abord");
        }
        if(go_create_floor)
        create_floor();
    } 

    void Update(){

        if(Input.GetKeyDown(KeyCode.I)){  
            reassign_id_floor();
        }
         if(Input.GetKeyDown(KeyCode.C)){  
            clean_list();
        }
    }
  

    void reassign_id_floor(){
        int i = 0;
        foreach(GameObject _id_floor in floor_list){

            if( _id_floor != null){
                _id_floor.GetComponent<id_floor>().id = i;
                _id_floor.GetComponent<id_floor>().id_text.text = "" + i;
                _id_floor.GetComponent<id_floor>().show_id(true);
                i++;
            }
        }
        print("done");
    }


    public void create_floor(){ // creation base square
        int i = 0;
        
        for(int row = 0; row < line; row++){
            for(int col = 0; col < colonne; col++){
                GameObject _floor = Instantiate(floor,transform);
                _floor.GetComponent<id_floor>().id = i;
                _floor.GetComponent<id_floor>().id_text.text = "" + i;
                floor_list.Add(_floor);
                float posX = col * size;
                float posY = row * size;
                _floor.transform.position = new Vector3(posX,0,posY); 
                i++;
            }      
        } 
    }

    public void clean_list(){

        int i = 0;
        foreach(GameObject _id_floor in floor_list){

            if( _id_floor == null){
                floor_list.RemoveAt(i);
                i++;
            }
        }
        print("done");



    }






  
}
