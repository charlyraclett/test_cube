using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager : MonoBehaviour{

    public static game_manager inst;
   
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private Vector2 cursorPosition;

    public int id_level;

    public GameObject[] levels_prefab;
    Dictionary<int, string> dictionary = new Dictionary<int, string>();




   
    void Start(){
        inst = this; 
        dictionary.Add(0, "I");
        dictionary.Add(1, "II");
        dictionary.Add(2, "III");
        dictionary.Add(3, "IV"); 

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        if(!dev_script.inst.skip_menu){  
            ui_manager.inst.show_menu_mode_game();
            sound_manager.inst.sound_music_menu(); 
            ui_manager.inst.ui_in_game.alpha = 0f;  
        }
  
        Cursor.visible = true;
    }


    // trigger ui manager button level
    public IEnumerator launch_game(){
        sound_manager.inst.sound_start_game_anim();
        yield return new WaitForSeconds(1f);// end anim menu
        Instantiate(levels_prefab[id_level], new Vector3(0, 0, 0), Quaternion.identity); 
        ui_manager.inst.light_container.SetActive(false);
        ui_manager.inst.black_panel_menu.SetBool("black_panel", false);
        ui_manager.inst.refresh_text_level_nbr(dictionary[id_level]);
        yield return new WaitForSeconds(1f); // time to anim floor intro
        StartCoroutine(camera_manager.inst.switch_cam_game());  
    }



    // trigger map manager end anim intro
    public IEnumerator launch_level(){
        sound_manager.inst.sound_title_start_game();
        ui_manager.inst.anim_start_level.SetTrigger("show_start");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(player_manager.inst.create_player(player_manager.inst.myId, true, player_manager.inst.choix_type, 0.1f));
        StartCoroutine(level_manager.inst.change_vague(level_manager.inst.id_vague));
        StartCoroutine(player_manager.inst.count_time());
        StartCoroutine(ui_manager.inst.set_alpha_ui_game(0.5f,1f));
        sound_manager.inst.sound_music_level(id_level);
    }



    // trigger_ui_manager_button_exit
    public void reset_level(){
        Time.timeScale = 1.0f;
        reset_and_back_to_menu();
    }

   // trigger ui manager button back menu pause
    public void reset_and_back_to_menu(){

        ui_manager.inst.light_container.SetActive(true);
        print("quit_game game_manager");
        StopAllCoroutines();
        level_manager.inst.stop_all();
        enemies_manager.inst.remove_all_enemies();
        sound_manager.inst.sound_music_menu();
        ui_manager.inst.show_menu_mode_game();
        player_manager.inst.reset_all_player();
        camera_manager.inst.reset_cameras();
        ui_manager.inst.text_vague_nbr.text = "";
        level_manager.inst.enemies_in_game = 0;
    }


    // trigger_level_vague 6
    public void level_complete(){
        map_manager.inst.active_falling_floor = false;
        camera_manager.inst.show_cam_player_win();
        StartCoroutine(ui_manager.inst.game_finish()); 
        sound_manager.inst.audio_source_zic.Stop();
        sound_manager.inst.sound_win_level();
        player_manager.inst.player_win();
        id_level++;
        PlayerPrefs.SetInt("level_complete", id_level);
        PlayerPrefs.Save(); 
        print("save unlock level "+ id_level);
    }


    // trigger button retry ui manager
    public IEnumerator reset_and_restart(){

        ui_manager.inst.black_menu_gameover.GetComponent<Animator>().SetBool("black_panel",true);
        yield return new WaitForSeconds(1f);
        ui_manager.inst.reset_ui_game();
        enemies_manager.inst.remove_all_enemies();
        player_manager.inst.reset_all_player();
        map_manager.inst.reinitialze_floor();
        camera_manager.inst.reset_cameras();
        level_manager.inst.reset_level();
        StartCoroutine(sound_manager.inst.set_mixer_in_game(1f,1f)); 
        ui_manager.inst.black_menu_gameover.GetComponent<Animator>().SetBool("black_panel",false);
        yield return new WaitForSeconds(1f); 
        StartCoroutine(launch_level());

    }


    // trigger player_manager
    public IEnumerator game_over(){
        yield return new WaitForSeconds(1f);
        print("game over");
        player_manager.inst.start_timer = false;
        sound_manager.inst.sound_music_game_over();
        enemies_manager.inst.StopAllCoroutines();
        level_manager.inst.stop_all();
        StartCoroutine(ui_manager.inst.show_menu_gameover()); 
        camera_manager.inst.show_cam_gameover();
        StartCoroutine(sound_manager.inst.set_mixer_in_game(6f,0f));
    }





    private IEnumerator HideCursor(){
        yield return new WaitForSeconds(3f);
        Cursor.visible = false;
    }
  






    








    
}
