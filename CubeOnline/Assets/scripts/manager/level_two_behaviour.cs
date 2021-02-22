using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_two_behaviour : level_manager{

    [Header("Prefab Enemy")]
    public bomb_enemy bomb; // for setting before instantiate
    public navmesh_agent agent_enemy; // for setting before instantiate

    [Header("Edition Enemy")]
  
    public wall_press pressoir_left_face;
    public wall_press pressoir_left_loitain;
    public wall_press pressoir_right_face;
    public wall_press pressoir_right_lointain;
    public turret_enemy turret;
    public turret_laser laser_turret;
    public pressoir _pressoir;
    public plateform_move plateform;
    public plateform_move plateform2;
    public saw_circular saw;
  
    void Start(){
        id_vague = dev_script.inst.start_at;
    }

    

    public override void vague_0(){ 
        print("level 2 vague 0");
        base.vague_0();
        setting_agent(2.5f);
        StartCoroutine(enemies_manager.inst.create_enemy_floor(1f, 2, false, 0));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(2f, 2, false, 7));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(3f, 2, false, 74));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(4f, 2, false, 81));
        level_manager.inst.enemies_in_game = 4;
        player_manager.inst.enemies_to_kill_per_vague = 4;
    }
    

    public override void vague_1(){ 
        base.vague_1();
        turret.start_turret(2f);
        StartCoroutine(call_enemy_floor(2f,3,1,2)); 
        level_manager.inst.enemies_in_game = 4; 
        player_manager.inst.enemies_to_kill_per_vague = 3;
    }

    public override void vague_2(){ 
        base.vague_2();
        StartCoroutine(call_enemy_floor(3f,5,1,5)); 
        _pressoir.start_pressoir(6f);
        player_manager.inst.enemies_to_kill_per_vague = 5;
        level_manager.inst.enemies_in_game = 6;  
    }

    public override void vague_3(){  
        base.vague_3();
        setting_bomb(1, 7, 0.5f, 4f);
        StartCoroutine(enemies_manager.inst.create_enemy_floor(0.2f, 2, false, 34));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(0.4f, 2, false, 35));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(0.6f, 2, false, 46));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(0.8f, 2, false, 47));
        pressoir_left_face.start_pressoir(1f);
        pressoir_right_face.start_pressoir(3f);
        pressoir_left_loitain.start_pressoir(4f);
        pressoir_right_lointain.start_pressoir(4.5f);
        player_manager.inst.enemies_to_kill_per_vague = 4;
        level_manager.inst.enemies_in_game = 8;  
    }

    public override void vague_4(){ 
        base.vague_4(); 
        StartCoroutine(enemies_manager.inst.create_enemy_floor(1f, 2, false, 67));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(2f, 2, false, 41));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(3f, 2, false, 21));
        laser_turret.active_turret(3f);
        level_manager.inst.enemies_in_game = 4; 
        player_manager.inst.enemies_to_kill_per_vague = 3;
    }

    public override void vague_5(){
        base.vague_5();
        saw. start_rotate_saw(1f);
        turret.start_turret(15f);
        pressoir_left_face.active_detection = true;
        pressoir_right_face.active_detection = true;
        pressoir_left_loitain.active_detection = true;
        pressoir_right_lointain.active_detection = true;

        level_manager.inst.enemies_in_game = 2; 
        player_manager.inst.enemies_to_kill_per_vague = 100; // no peferct for now
    }



    public override void stop_all(){
        StopAllCoroutines();
        turret.stop_turret();
        _pressoir.stop_pressoir();
    }


    public override void reset_level(){
        print("reset level");
        base.reset_level();
        turret.end_turret();
        plateform.initiale_position();
        plateform2.initiale_position();
        saw.reset_saw();
    }




    public override void delete_level(){
        base.delete_level();
        Destroy(this.gameObject);
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

    void setting_agent(float speed){
        agent_enemy.agent.speed = speed;
    }


    public override void reinitialize_position_mechanism(){
        plateform.initiale_position();
        plateform2.initiale_position();
    }
    

    

   


}
