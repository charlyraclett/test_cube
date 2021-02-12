using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_two_behaviour : level_manager{

    [Header("Prefab Enemy")]
    public bomb_enemy bomb; // for setting before instantiate

    [Header("Edition Enemy")]
  
    public wall_press pressoir_left_face;
    public wall_press pressoir_left_loitain;
    public wall_press pressoir_right_face;
    public wall_press pressoir_right_lointain;
    public turret_enemy turret;
    public turret_laser laser_turret;
    public pressoir _pressoir;
    public gear_floor gear_floor;
    public Transform[] position_nest_start;

    void Start(){
        id_vague = dev_script.inst.start_at;
    }

    

    public override void vague_0(){ 
        print("level 2 vague 0");
        base.vague_0();
        StartCoroutine(call_enemy_floor(2f,3,0,5)); 
        level_manager.inst.enemies_in_game = 4;
        player_manager.inst.enemies_to_kill_per_vague = 3;
        turret.start_turret(2f,1);
    }

    public override void vague_1(){ 
        base.vague_1();
        turret.start_turret(2f,3);
        level_manager.inst.enemies_in_game = 1; 
        player_manager.inst.enemies_to_kill_per_vague = 1; // no perfect pas d'enemy a shooter pour le moment
    }

    public override void vague_2(){ 
        base.vague_2();
        StartCoroutine(map_manager.inst.alert_floor_airwall(1f));
        StartCoroutine(call_enemy_floor(3f,2,2,5)); 
        _pressoir.start_pressoir(6f,4);
        player_manager.inst.enemies_to_kill_per_vague = 2;
        level_manager.inst.enemies_in_game = 3;  
    }

    public override void vague_3(){  
        base.vague_3();
        setting_bomb(1, 7, 0.5f, 4f);
        StartCoroutine(enemies_manager.inst.create_enemy_floor(0f, 2, false, 34));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(1.5f, 2, false, 35));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(3f, 2, false, 46));
        StartCoroutine(enemies_manager.inst.create_enemy_floor(4.5f, 2, false, 47));
        pressoir_left_face.start_pressoir(1f,1);
        pressoir_right_face.start_pressoir(3f,1);
        pressoir_left_loitain.start_pressoir(4f,1);
        pressoir_right_lointain.start_pressoir(4.5f,1);
        player_manager.inst.enemies_to_kill_per_vague = 4;
        level_manager.inst.enemies_in_game = 8;  
    }

    public override void vague_4(){ 
        base.vague_4(); 
        laser_turret.active_turret();
        level_manager.inst.enemies_in_game = 1; 
        player_manager.inst.enemies_to_kill_per_vague = 1; // no perfect pas d'enemy a shooter pour le moment


    }

    public override void vague_5(){
        base.vague_5();
        pressoir_left_face.active_detection = true;
        pressoir_right_face.active_detection = true;
        pressoir_left_loitain.active_detection = true;
        pressoir_right_lointain.active_detection = true;
    }

    public override void stop_all(){
        StopAllCoroutines();
        turret.stop_turret();
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

    

   


}
