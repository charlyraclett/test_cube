using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_three_behaviour : level_manager{

    [Header("Prefab Enemy")]
    public bomb_enemy bomb; // for setting before instantiate

    [Header("Edition Enemy")]
    public wall_fire firewall1;
    public wall_fire firewall2;
    public wall_air airwall;
    public gear_floor gear_floor;
    public Transform[] position_nest_start;
    

    public override void vague_0(){ 
        print("vague 0");
        base.vague_0();
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


    

   


}
