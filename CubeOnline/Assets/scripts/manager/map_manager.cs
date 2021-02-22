using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class map_manager : MonoBehaviour{

    public static map_manager inst;

    public bool show_id_number;

    [Header("Edition")]
    public Transform[] bases;
    public Transform floor_container;
    public GameObject particule_falling_floor;
    public Animator alerte_wall;

    [Header("Info")]
    public List<GameObject> floor_list = new List<GameObject>();
   
    float delay_falling = 3f;
    public bool active_falling_floor;
    int count = 0;

    public List<int> my_list_int_random = new List<int>();
   
   
    void Start(){
        Invoke("initialize",0.1f);
    }


    void initialize(){
        inst = this;
        add_floor_to_list();
        add_bases_to_player_manager();
        StartCoroutine(intro_anim_floor(0.5f,0.02f));
    }


    public IEnumerator falling_floor(float delay){

        yield return new WaitForSeconds(delay);  

        float _delay = delay_falling;
        active_falling_floor = true;

        while(floor_list.Count > 60 && active_falling_floor){

            yield return new WaitForSeconds(0.02f);  
            network.inst.id_event = 0;   //for network

            int rand = Random.Range(0, floor_list.Count);
            Vector3 particule_position = new Vector3(floor_list[rand].transform.position.x, 0.6f, floor_list[rand].transform.position.z);
            Destroy(Instantiate(particule_falling_floor,particule_position,floor_list[rand].transform.rotation),5f);
            floor_list[rand].transform.GetChild(1).gameObject.SetActive(true); // active obstacle navmesh
            floor_list[rand].GetComponentInChildren<falling_floor>().falling(true);
            sound_manager.inst.sound_rocks();
            floor_list.RemoveAt(rand);
            count++;
            yield return new WaitForSeconds(_delay);
    
            if(count == 5){
                _delay = 2f;
            }
            if(count == 10){
                _delay = 1f;  
            }  
    
        }  
    }


    public void add_floor_to_list(){
        foreach (Transform _floor_ground in floor_container){
            floor_list.Add(_floor_ground.gameObject);
            _floor_ground.GetComponentInParent<id_floor>().show_id(show_id_number);   
        }
        list_number_random();
        active_animator_for_intro(!dev_script.inst.skip_menu);
    }


    public void active_animator_for_intro(bool value){
        for (int i = 0; i < floor_list.Count; i++){
            floor_list[i].GetComponentInChildren<falling_floor>().standby_for_intro(value);
        } 
        for(int i = 0; i < bases.Length; i++){
            bases[i].GetComponent<Animator>().SetBool("active_anim_intro",value);
        }    
    }




    public void add_bases_to_player_manager(){
        for (int i = 0; i < bases.Length; i++){
            player_manager.inst.bases_pos[i] = bases[i];
        }   
    }



    public void reinitialze_floor(){
        active_falling_floor = false;
        falling_floor[] floor = FindObjectsOfType<falling_floor>();
        foreach (falling_floor _floor_ground in floor){
            _floor_ground.falling(false);
        }  
        floor_list.Clear();
        add_floor_to_list();

    }




    IEnumerator intro_anim_floor(float delay, float cadence){
        yield return new WaitForSeconds(delay);
        for(int i = 0; i < floor_list.Count; i++){  // show anim floor
            floor_list[my_list_int_random[i]].GetComponentInChildren<falling_floor>().anim_initial_position(true);
            yield return new WaitForSeconds(cadence);
        }    
        yield return new WaitForSeconds(1.5f);  //  shox anim bases
        for(int i = 0; i < bases.Length; i++){
            bases[i].GetComponent<Animator>().SetBool("in_game",true);
        }
        yield return new WaitForSeconds(1.5f);
        sound_manager.inst.sound_start_map_anim();
        yield return new WaitForSeconds(1f);

        if(!dev_script.inst.skip_menu){
            StartCoroutine(game_manager.inst.launch_level());
        }

    }



    public IEnumerator alert_floor_airwall(float delai){
        yield return new WaitForSeconds(delai);
        alerte_wall.SetTrigger("alerte_red");
    }



    void list_number_random(){  // creation list random pour anim intro floor
        while(my_list_int_random.Count < floor_list.Count){
            int numToAdd = Random.Range(0,floor_list.Count);   
            while(!my_list_int_random.Contains(numToAdd)){
                my_list_int_random.Add(numToAdd);
                numToAdd = Random.Range(0,floor_list.Count);
               
            }   
        }
    }

   
 
}
