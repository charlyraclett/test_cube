using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour{

    Animator anim;
    AudioSource audio_source;

    public AudioClip open_gate;
    public AudioClip move;
    public AudioClip stop_move;

    bool finish;




    void Start(){
        anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();
    }


    void Update(){

        if(Input.GetKeyDown(KeyCode.P)){    
            StartCoroutine(elevator_in_game(0.1f));   
        }  
    }
   
    void OnTriggerEnter(Collider col){ 
        if(col.tag == "Player"){
            interactable_manager.inst.player_is_in_area_interactable(this); 
            col.gameObject.transform.parent = this.transform; 
        }
    }

    void OnTriggerExit(Collider col){ 
        if(col.tag == "Player"){   
            interactable_manager.inst.quit_aera_interactable(); 
            col.gameObject.transform.parent = null;  
        }
    }




    public IEnumerator up_elevator(){

        if(finish)
        yield break;

        finish = true;

        yield return new WaitForSeconds(0.5f);

        camera_manager.inst.show_cam_level_finished();
        sound_manager.inst.sound_end_chapter();
        StartCoroutine(sound_manager.inst.set_mixer_in_game(4f,0f));
        player_manager.inst.my_avatar.host = false;

        anim.SetBool("open_gate",false);
        audio_source.PlayOneShot(open_gate,1f);
        yield return new WaitForSeconds(3.1f);
        anim.SetBool("in_game",false);
        audio_source.PlayOneShot(move,1f);
        StartCoroutine(ui_manager.inst.set_alpha_ui_game(2f,0f));

        yield return new WaitForSeconds(12f);
        ui_manager.inst.show_menu_end_chapter(true);

        
    }


    public IEnumerator elevator_in_game(float delai){

        yield return new WaitForSeconds(delai);
        anim.SetBool("in_game",true);
        audio_source.PlayOneShot(move,1f);
        yield return new WaitForSeconds(4f);
        audio_source.Stop();
        audio_source.PlayOneShot(stop_move,1f);
        yield return new WaitForSeconds(1);

        anim.SetBool("open_gate",true);
        audio_source.PlayOneShot(open_gate,1f);
        yield return new WaitForSeconds(3.1f);
        audio_source.Stop();

    }

}
