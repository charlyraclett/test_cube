using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_one_behaviour : level_manager{

    [Header("Prefab Enemy")]
    public bomb_enemy bomb; // for setting before instantiate

    [Header("Edition Enemy")]
    public wall_fire firewall1;
    public wall_fire firewall2;
    public wall_air airwall;
    public gear_floor gear_floor;
    public Transform[] position_nest_start;
    int pos_nest = -1;

    int pv_player_temp;

    void Start(){
        id_vague = dev_script.inst.start_at;
        player_manager.inst.has_boost = true;
        player_manager.inst.has_rocket = false;
        player_manager.inst.has_light = false;
    }


    public override void vague_0(){ 
        print("vague 0");
        base.vague_0();
        StartCoroutine(call_enemy_floor(2f,3,0,5)); 
        level_manager.inst.enemies_in_game = 3;
        player_manager.inst.enemies_to_kill_per_vague = 3;
    }

    public override void vague_1(){ 
        base.vague_1();
        StartCoroutine(start_gear_floor(1f,"right"));
        level_manager.inst.enemies_in_game = 1;
        player_manager.inst.enemies_to_kill_per_vague = 1;
    }

    public override void vague_2(){ 
        base.vague_2();
        StartCoroutine(call_enemy_floor(2f,3,0,5));
        StartCoroutine(create_nest(8f,1,1,1,0));
        level_manager.inst.enemies_in_game = 6; 
        player_manager.inst.enemies_to_kill_per_vague = 6;
        StartCoroutine(consommable_manager.inst.create_box(20,0));   
    }

    public override void vague_3(){  
        base.vague_3();
        StartCoroutine(map_manager.inst.alert_floor_airwall(1f));
        StartCoroutine(start_wall_fan(4f));
        StartCoroutine(create_nest(6f,2,2,2,0));
        level_manager.inst.enemies_in_game = 3;  
        player_manager.inst.enemies_to_kill_per_vague = 3;
    }

    public override void vague_4(){ 
        base.vague_4(); 
        StartCoroutine(start_wall_fire(2f));
        setting_bomb(1, 7, 1, 1f);
        StartCoroutine(call_enemy_floor(6f,5,2, 1.5f));
        level_manager.inst.enemies_in_game = 7;  
        player_manager.inst.enemies_to_kill_per_vague = 5;

    }

    public override void vague_5(){
        base.vague_5();
        
        setting_bomb(1, 7, 0.5f, 4f);
        StartCoroutine(enemies_manager.inst.create_enemy_floor(0f, 2, false, 23));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(1.5f, 2, false, 29));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(3f, 2, false, 87));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(4.5f, 2, false, 93));

        StartCoroutine(create_nest(7f,2,1,2,1));

        StartCoroutine(map_manager.inst.falling_floor(5f));

        StartCoroutine(start_wall_fire(15f));
        level_manager.inst.enemies_in_game = 9;
        player_manager.inst.enemies_to_kill_per_vague = 7;
    }

    public override void stop_all(){
        firewall1.stop_wall();
        firewall2.stop_wall();
        gear_floor.stop_wall();
        airwall.stop_wall();
        StopAllCoroutines();
    }


    

    public IEnumerator call_enemy_floor(float delay, int nbr, int id_prefab, float cadence){
        yield return new WaitForSeconds(delay);
        while(nbr > 0){
            StartCoroutine(enemies_manager.inst.create_enemy_floor(0,id_prefab,true,0));
            nbr--;
            yield return new WaitForSeconds(cadence);  
        }
    }
    

   

    void setting_bomb(int nbr, float speed, float cadence, float delay_wave){
        bomb.nbr_shoot = nbr;
        bomb.force_shoot = speed;
        bomb.cadence_shoot = cadence;
        bomb.delay_each_wave = delay_wave;
    }

   

   
    // Wall Fire
    public IEnumerator start_wall_fire(float delay){
        yield return new WaitForSeconds(delay);
        firewall1.lauch_fire_wall();
        yield return new WaitForSeconds(0.5f);
        firewall2.lauch_fire_wall();
    }
    // Wall Air
    public IEnumerator start_wall_fan(float delay){
        yield return new WaitForSeconds(delay);
        airwall.lauch_fan_wall();
    }
    // Floor Rotation Sphere
    public IEnumerator start_gear_floor(float delay, string direction){
        yield return new WaitForSeconds(delay);
        gear_floor.active_gear(direction);
    }



    // Creation nest air vehicule
    public IEnumerator create_nest(float delay, int one, int two, int three, int position_id){

        yield return new WaitForSeconds(delay);
      //  pos_nest = (pos_nest + 1) % position_nest_start.Length;
        GameObject nest = Instantiate(enemies_manager.inst.nest_enemy[0],position_nest_start[position_id].position, position_nest_start[position_id].rotation);
        nest.GetComponent<nest_vehicule>().intialize_nest_container(enemies_manager.inst.enemies_prefab[one], enemies_manager.inst.enemies_prefab[two],enemies_manager.inst. enemies_prefab[three]);
        //enemies_manager.inst.enemy_in_game.Add(nest);
       
        nest.GetComponent<Animator>().SetTrigger("go_to_game");
        StartCoroutine(sound_manager.inst.fade_in_nest_move());

        yield return new WaitForSeconds(3f);
        sound_manager.inst.audio_source_nest.Stop();

        yield return new WaitForSeconds(2f);
        nest.GetComponent<nest_vehicule>().active_container_enemies();
       
        yield return new WaitForSeconds(3.5f);
        nest.GetComponent<Animator>().SetTrigger("quit_game");
        StartCoroutine(sound_manager.inst.fade_out_nest_move(2f));

        yield return new WaitForSeconds(5f);
        Destroy(nest);
        sound_manager.inst.audio_source_nest.Stop();
    }


    public override void delete_level(){
        base.delete_level();
        Destroy(this.gameObject);
    }




    //  public IEnumerator create_enemy_floor(int id_prefab){
    //     int rand = Random.Range(0,floor_list.Count); // nest random
    //     GameObject base_enemy = floor_list[rand];
    //     sound_manager.inst.sound_enemy_nest();  

    //     float elapsed = 0f;
    //     float duree = 1f; 
    //     float height = 0f;
    //     while( elapsed < duree ){ // down
    //         height = Mathf.Lerp(0f,-7f,elapsed / duree);
    //         base_enemy.transform.position = new Vector3(base_enemy.transform.position.x, height, base_enemy.transform.position.z);
    //         elapsed += Time.deltaTime;
    //         yield return null;
    //     }
    //     height = -7f;

    //     create_enemy(base_enemy, id_prefab); 
    //     sound_manager.inst.sound_enemy_nest();

    //     elapsed = 0f;
    //     duree = 1f;
    //     while( elapsed < duree ){ //up
    //         height = Mathf.Lerp(-7,0f,elapsed / duree);
    //         base_enemy.transform.position = new Vector3(base_enemy.transform.position.x, height, base_enemy.transform.position.z);
    //         elapsed += Time.deltaTime;
    //         yield return null;
    //     }
    //     base_enemy.transform.position = new Vector3(base_enemy.transform.position.x, 0f, base_enemy.transform.position.z);

    //     agent.transform.parent = null;
    //     yield return new WaitForSeconds(0.3f);
    //     initialize_enemy_floor(agent);
        
    // }


    // void create_enemy(GameObject base_enemy, int id_prefab){
    //     Vector3 _position = new Vector3(base_enemy.transform.position.x, base_enemy.transform.position.y + 1.08f,base_enemy.transform.position.z);
    //     agent = Instantiate(enemies_prefab[id_prefab],_position,base_enemy.transform.rotation);
    //     print(agent);
    //     enemy_in_game.Add(agent);
    //     agent.transform.parent = base_enemy.transform;
    // }


}
