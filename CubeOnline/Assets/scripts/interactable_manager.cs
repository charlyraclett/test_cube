using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable_manager : MonoBehaviour{

    public static interactable_manager inst;

    public object object_interactable;
    string text_button_action;

    void Start(){  
        inst = this;
    }



    // trigger interactable object collider
    public void player_is_in_area_interactable(object _interactable){

        object_interactable = _interactable; 
        change_and_show_text_button(object_interactable);
        sound_manager.inst.sound_interaction();
    }


    // trigger controller_cube gamepad Button A
    public void press_for_action(){

        if(object_interactable != null){
           
            if(object_interactable.GetType() == typeof(plateform_move)){
                action_plateform((plateform_move)object_interactable);
            sound_manager.inst.sound_press_button_a();

            }
            if(object_interactable.GetType() == typeof(collider_switch_turret)){
                do_action_switch_turret((collider_switch_turret)object_interactable);   
            sound_manager.inst.sound_press_button_a();

            }

            if(object_interactable.GetType() == typeof(switch_light)){
                do_action_switch_light((switch_light)object_interactable);   
            }
           
            ui_manager.inst.show_button_action(false);
        }  
    }



    // trigger interactable collider exit
    public void quit_aera_interactable(){
        object_interactable = null;
        ui_manager.inst.show_button_action(false);
    }





    void action_plateform(plateform_move plateform){
        plateform.active_plateform();   
    }

    void do_action_switch_turret(collider_switch_turret switch_turret){
        switch_turret.active_switch();
    }

    void do_action_switch_light(switch_light switch_light){
        switch_light.active_light();
    }

    


    void change_and_show_text_button(object _interactable){

        if(_interactable.GetType() == typeof(plateform_move)){
            text_button_action = "Move";  
        }
        if(_interactable.GetType() == typeof(collider_switch_turret)){
            text_button_action = "Deactivate";
        }

        if(_interactable.GetType() == typeof(switch_light)){
            text_button_action = "Switch On";
        }

        ui_manager.inst.button_action_text.text = text_button_action;  
        ui_manager.inst.show_button_action(true);
    }

       
}
