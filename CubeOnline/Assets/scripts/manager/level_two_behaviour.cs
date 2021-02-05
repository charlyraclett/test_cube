using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_two_behaviour : level_manager{

    [Header("Prefab Enemy")]
    public bomb_enemy bomb; // for setting before instantiate

    [Header("Edition Enemy")]
    public wall_fire firewall1;
    public wall_fire firewall2;
    public wall_air airwall;
    public gear_floor gear_floor;
    public Transform[] position_nest_start;
    

    public override void vague_0(){ 
        print("level 2 vague 0");
        base.vague_0();
        StartCoroutine(call_enemy_floor(2f,3,0,5)); 
        level_manager.inst.enemies_in_game = 3;
        player_manager.inst.enemies_to_kill_per_vague = 3;
    }

    public override void vague_1(){ 
        base.vague_1();
    }

    public override void vague_2(){ 
        base.vague_2();
    }

    public override void vague_3(){  
        base.vague_3();
    }

    public override void vague_4(){ 
        base.vague_4(); 
    }

    public override void vague_5(){
        base.vague_5();
    }

    public override void stop_all(){
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

    

   


}
