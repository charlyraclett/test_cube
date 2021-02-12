using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_manager : MonoBehaviour{

    public static player_manager inst;

    public bool destroy_player_prefs;

    [Header("Info")]
    public controller_cube my_avatar;
    public int myId;
    public int choix_type;
    public int enemies_killed = 0;
    public int enemies_to_kill_per_vague;
    public int nbr_shoot;
    public int time_finish_level;
    public Transform[] bases_pos;
    public Transform[] avatars_pos;
    public int[] point_players = new int[]{0,0,0,0};
    public bool player_active_multi;


    [Header("Edition")]
    public Animator pv_player_ui;
    public int life_player = 3;
    public float distance_great_shot = 12f;
    public GameObject[] avatars_prefab;
    public Material[] color;
   
    public Animator[] bases_anim;


    // Player Stats
    [HideInInspector] public bool start_timer;
    float timer = 0f;
    string minutes;
    string seconds;
    string duration_of_level;

    void Awake(){
        inst = this;
        if(destroy_player_prefs){
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("Timer", 10000);
        }
    }

    // trigger game_manager
    public void player_win(){
        player_manager.inst.life_player = 3;
        start_timer = false;
        my_avatar.host = false;
        if(timer <  PlayerPrefs.GetFloat("Timer")){
            print("new Record !!");
            PlayerPrefs.SetFloat("Timer", timer);
            PlayerPrefs.Save(); 
        } 

        ui_manager.inst.refresh_duration_level(duration_of_level, get_last_duration());   
    }

    // trigger game_manager
    public IEnumerator count_time(){
        start_timer = true;
        while(start_timer){
            timer = Time.deltaTime + timer;
            minutes = Mathf.Floor(timer / 60).ToString("0");
            seconds = (timer % 60).ToString("00");
            duration_of_level = minutes + " min " + seconds + " s"; 
            yield return null;
        }
    }

    string get_last_duration(){
        float last_duration = PlayerPrefs.GetFloat("Timer");
        minutes = Mathf.Floor(last_duration / 60).ToString("0");
        seconds = (last_duration % 60).ToString("00");
        string last_time = minutes + " min " + seconds + " s";
        return last_time;
    }

    



    public void check_if_shoot_perfect(){
        if(nbr_shoot == enemies_to_kill_per_vague){
            StartCoroutine(ui_manager.inst.show_perfect_vague());
        }
        nbr_shoot = 0;
    }






    public IEnumerator create_player(int id,bool ismine, int type, float delay){

        yield return new WaitForSeconds(delay);
       
        Vector3 pos_base = new Vector3(bases_pos[id].position.x,bases_pos[id].position.y - 1f, bases_pos[id].position.z);
        GameObject cube = Instantiate(avatars_prefab[type],pos_base, bases_pos[id].rotation);
        avatars_pos[id] = cube.GetComponent<Transform>();
        controller_cube controller = cube.GetComponent<controller_cube>();

      
        foreach(GameObject color_player in GameObject.FindGameObjectsWithTag("color")){
            color_player.GetComponent<MeshRenderer>().material = color[id];
        }
     
        controller.id_avatar = id;
        sound_manager.inst.sound_friction();
        ui_manager.inst.score_players[id].transform.parent.gameObject.SetActive(true);

        if(ismine){
            myId = id; 
            my_avatar = controller;
            camera_manager.inst.target_cam_player = player_manager.inst.my_avatar.transform;
            StartCoroutine(controller.player_invulnerable());   
        }
       
      
        cube.transform.parent = bases_pos[id].transform;
        bases_pos[id].GetComponent<Animator>().SetBool("turn_player_create",true);
        yield return new WaitForSeconds(1f);
        cube.transform.parent = null;
        bases_pos[id].GetComponent<Animator>().SetBool("turn_player_create",false);

        if(ismine){   
            cube.GetComponent<Rigidbody>().useGravity = true;
            cube.GetComponent<Rigidbody>().isKinematic = false; 
            my_avatar.host = ismine;

            if(player_active_multi){
                StartCoroutine("network_position_send"); 
                print("start : send position to network");
            }
        }
        controller.is_moving = true;
        yield return null;
    }



    //trigger controller_cube
    public void player_dead(){
        life_player--;
        pv_player_ui.SetInteger("pv_player",life_player);
        StartCoroutine(camera_manager.inst.start_shake_cam());
        avatars_pos[myId] = null;

        if(life_player == 1){
            StartCoroutine(sound_manager.inst.sound_end_last_pv());
        }

        if(life_player > 0){
            StartCoroutine(create_player(myId, true, choix_type, 2f));
        }else{
            StartCoroutine(game_manager.inst.game_over());
            start_timer = false;
        }   
    }

    




    public IEnumerator refresh_score(int id_player, float distance_shoot){
        if(distance_shoot > distance_great_shot){
           ui_manager.inst.show_great_shot();
        }  
        point_players[id_player] += 1;
        ui_manager.inst.score_players[id_player].GetComponent<Animator>().SetTrigger("win");
        sound_manager.inst.sound_win_point();
        ui_manager.inst.score_players[myId].text = point_players[id_player].ToString();
        yield return null;
    }
    

    public void reset_all_player(){

        print("reset players");

        //StopAllCoroutines();
        foreach(Transform player in avatars_pos){
            if(player != null){
                Destroy(player.gameObject);
            }
        }

        for (int i = 0; i < point_players.Length; i++){
            point_players[i] = 0;   
        }

        life_player = 3;
        enemies_killed = 0;
        nbr_shoot = 0;
        pv_player_ui.SetInteger("pv_player",life_player);
    }

    
    



    





   




   

   










}
