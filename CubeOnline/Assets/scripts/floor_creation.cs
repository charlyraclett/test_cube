using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class floor_creation : MonoBehaviour{


    public int line;
    public int colonne;
    public int size = 1;
    public GameObject floor;

    int id_base1 = 64;
    int id_base2 = 56;
    int id_base3 = 104;
    int id_base4 = 16;

    public float delay_falling = 3f;
    int count = 0;


    public List<GameObject> floor_list = new List<GameObject>();

    
    void Start(){
       create_floor();
    } 

  

    void create_floor(){
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



    void create_bases(){

       
        floor_list[id_base1].GetComponent<MeshRenderer>().material = network.inst.color[4];
        floor_list[id_base2].GetComponent<MeshRenderer>().material = network.inst.color[4];
        floor_list[id_base3].GetComponent<MeshRenderer>().material = network.inst.color[4];
        floor_list[id_base4].GetComponent<MeshRenderer>().material = network.inst.color[4];

        floor_list[id_base1].transform.Rotate(0,-90,0);
        floor_list[id_base2].transform.Rotate(0,90,0);
        floor_list[id_base3].transform.Rotate(0,180,0);

        network.inst.bases_pos[0] = floor_list[id_base1].transform;
        network.inst.bases_pos[1] = floor_list[id_base2].transform;
        network.inst.bases_pos[2] = floor_list[id_base3].transform;
        network.inst.bases_pos[3] = floor_list[id_base4].transform;

        floor_list.RemoveAt(id_base3);  
        floor_list.RemoveAt(id_base1);
        floor_list.RemoveAt(id_base2);
        floor_list.RemoveAt(id_base4);
 
    }

   

   
}
