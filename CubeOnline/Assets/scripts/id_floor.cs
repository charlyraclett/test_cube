using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class id_floor : MonoBehaviour{


public int id = 0;
public TextMesh id_text;

    void Start(){
       id_text.text = id.ToString();
        show_id(false);
    }


    public void show_id(bool value){
        id_text.gameObject.SetActive(value);
    }

    

}
