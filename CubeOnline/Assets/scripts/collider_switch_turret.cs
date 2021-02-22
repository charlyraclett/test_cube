using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_switch_turret : MonoBehaviour{



    public bool switch_is_active;
    Color color;
    public Renderer marqueur;
    public Renderer cable;

    Color orange = new Color32(250, 97, 37, 255);


    
    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player" && !switch_is_active){
            interactable_manager.inst.player_is_in_area_interactable(this);  
            setcolor(orange);
        }
    }

    void OnTriggerExit(Collider col){ 
        if(col.tag == "Player"){   
            interactable_manager.inst.quit_aera_interactable();
            if(switch_is_active)
            return;
            setcolor(Color.red);
        }
    }


    public void active_switch(){
        switch_is_active = true;
        setcolor(Color.black);
    }

    public void reinitilize(){
        switch_is_active = false;
        setcolor(Color.red);
    }



    void setcolor(Color color){
        marqueur.material.SetColor("_Color",color);
        marqueur.material.SetColor("_EmissionColor", color);
        cable.material.SetColor("_Color", color);
        cable.material.SetColor("_EmissionColor", color);   
    }
}
