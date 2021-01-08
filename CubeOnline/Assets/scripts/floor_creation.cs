using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class floor_creation : MonoBehaviour{

    public static floor_creation inst;

    public int line;
    public int colonne;
    public int size = 1;
    public GameObject floor;

    int[] id_floor_base = {64,56,104,16};
    public Material material_base;
    
    public float delay_falling = 3f;
    int count = 0;


    public List<GameObject> floor_list = new List<GameObject>();

    
    void Start(){
       inst = this;
       create_floor();
    } 

    void Update(){

          
    }

  

    public void create_floor(){
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
        create_bases();
    }


    public IEnumerator delete_floor(float delay){

        yield return new WaitForSeconds(delay);  
        int rand = Random.Range(0, floor_list.Count);
        StartCoroutine(floor_list[rand].GetComponent<falling_floor>().falling());
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



    public void create_bases(){

        for (int i = 0; i < id_floor_base.Length;i++){
            floor_list[id_floor_base[i]].GetComponent<MeshRenderer>().material = material_base;
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

   

   
}
