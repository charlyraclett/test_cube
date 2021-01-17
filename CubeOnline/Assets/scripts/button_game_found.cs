using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class button_game_found : MonoBehaviour{

    Button button;
    public int id_button;

    void Start(){
        button = GetComponent<Button>(); 
        button.onClick.AddListener(() => ui_manager.inst.click_button_game_found(id_button)); 
    }
}
