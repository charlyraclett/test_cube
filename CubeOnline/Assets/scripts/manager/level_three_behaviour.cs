using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_three_behaviour : level_manager{


    public elevator _elevator;



    void Start(){
        id_vague = dev_script.inst.start_at;
        player_manager.inst.has_light = true;
    }


   
    public override void vague_0(){ 
       // base.vague_0();
        print("level III vague 0"); 
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


    public override void special_event(){

        StartCoroutine(_elevator.elevator_in_game(8f));
    }


    

   


}
