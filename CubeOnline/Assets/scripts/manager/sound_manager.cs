using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class sound_manager : MonoBehaviour{

    public static sound_manager inst;

    public AudioSource audio_source_player;
    public AudioSource audio_source_env;
    public AudioSource audio_source_bullet;
    public AudioSource audio_source_move;
    public AudioSource audio_source_zic;
    public AudioSource audio_source_nest;
    public AudioSource audio_source_ui;
    public AudioSource audio_source_move_canon;

    public AudioMixer mixer_in_game;


    [Header("Zic level")]
    public AudioClip[] zic_level;
    

    [Header("Player")]
    public AudioClip move;
    public AudioClip move_canon;
    public AudioClip death_sound;
    public AudioClip death_player;
    public AudioClip shoot_player;
    public AudioClip shoot_2_player;
    public AudioClip death_bullet;
    public AudioClip untouched_sound;
    public AudioClip last_life_sound;
    public AudioClip interactable_sound;
    public AudioClip press_buttonA_sound;
    public AudioClip turbo_sound;
    public AudioClip boost_reload_sound;



    [Header("UI")]
    public AudioClip click;
    public AudioClip click_back;
    public AudioClip change_vague;
    public AudioClip hover;
    public AudioClip zic_menu;
    public AudioClip network_ok;
    public AudioClip error;
    public AudioClip point_win;
    public AudioClip win_level;
    public AudioClip game_found;
    public AudioClip last_vague;
    public AudioClip game_over;
    public AudioClip perfect_vague;
    public AudioClip start_game_anim;
    public AudioClip start_map_anim;
    public AudioClip bip_floor_anim;
    public AudioClip title_start_game;


    [Header("Env")]
    public AudioClip friction;
    public AudioClip rocks;

    [Header("Enemy")]
    public AudioClip enemy_nest;
    public AudioClip move_nest;
    public AudioClip active_nest;
    public AudioClip bip_enemy;
    public AudioClip fire;
   
   
   
    void Awake(){
        inst = this;
    }

    // Ziv level
    public void sound_music_level(int id_level){
        audio_source_zic.loop = true;
        audio_source_zic.volume = 0.3f;
        audio_source_zic.clip = zic_level[id_level];
        audio_source_zic.Play(); 
    }
    

    public void sound_music_menu(){
        audio_source_zic.loop = true;
        audio_source_zic.volume = 0.3f;
        audio_source_zic.clip = zic_menu;
        audio_source_zic.Play(); 
    }

    public void sound_music_game_over(){
        audio_source_zic.volume = 0.6f;
        audio_source_zic.loop = false;
        audio_source_zic.clip = game_over;
        audio_source_zic.PlayOneShot(game_over,0.5f); 
    }

    // UI

    public void sound_bip_floor_anim(){
        audio_source_ui.PlayOneShot(bip_floor_anim,0.6f);
    }

    public void sound_title_start_game(){
        audio_source_ui.PlayOneShot(title_start_game,0.8f);
    }

    public void sound_start_game_anim(){
        audio_source_ui.PlayOneShot(start_game_anim);
        audio_source_zic.Stop();
    }

    public void sound_start_map_anim(){
        audio_source_ui.PlayOneShot(start_map_anim,1f);
    }
    public void sound_change_vague(){
        audio_source_ui.PlayOneShot(change_vague,0.6f);
    }

    public void sound_win_level(){
        audio_source_player.Stop();
        audio_source_ui.PlayOneShot(win_level,0.6f);
    }

    public void sound_game_found(){
        audio_source_ui.PlayOneShot(game_found,1f);
    }

    public void sound_win_point(){
        audio_source_ui.PlayOneShot(point_win,0.8f);
    }

    public void sound_click(){
        audio_source_ui.PlayOneShot(click,1f);
    }

    public void sound_server_ok(){
        audio_source_ui.PlayOneShot(network_ok,1f);
    }

    public void sound_server_error(){
        audio_source_ui.PlayOneShot(error,1f);
    }

    public void sound_click_back(){
        audio_source_ui.PlayOneShot(click_back,1f);
    }

    public void sound_hover(){  
        audio_source_ui.PlayOneShot(hover,0.6f);
    }

    public void sound_last_vague(){  
        audio_source_ui.PlayOneShot(last_vague,0.6f);
    }

    public void sound_perfect_vague(){  
        audio_source_ui.PlayOneShot(perfect_vague,0.8f);
    }


    //player

     public void sound_reload_boost(){
        audio_source_player.PlayOneShot(boost_reload_sound,0.5f);
    }

    public void sound_interaction(){
        audio_source_player.PlayOneShot(interactable_sound,0.5f);
    }

    public void sound_press_button_a(){
        audio_source_player.PlayOneShot(press_buttonA_sound,1f);
    }

     public void sound_turbo(){
        audio_source_player.PlayOneShot(turbo_sound,1f);
    }

    public void sound_death_player(){
        audio_source_player.PlayOneShot(death_player,0.5f);
    }

    public void sound_shoot_player(){ 
        audio_source_bullet.PlayOneShot(shoot_player,1f);
    }

    public void sound_shoot_fire(){ 
        audio_source_bullet.PlayOneShot(fire,0.6f);
    }

    public void sound_shoot2_player(){ 
        audio_source_bullet.PlayOneShot(shoot_2_player,1f);
    }

    public void sound_bullet_death(){  
        audio_source_bullet.PlayOneShot(death_bullet,0.6f);
    }

    public void sound_bomb_death(){  
        audio_source_bullet.PlayOneShot(death_sound,1f);
    }

    public void sound_end_intouched(){  
        audio_source_bullet.PlayOneShot(untouched_sound,1f);
    }

    public void sound_move(float pitch){

        audio_source_move.pitch = pitch;
        if(!audio_source_move.isPlaying){
            audio_source_move.volume = 0.4f;
            audio_source_move.clip = move;
            audio_source_move.Play();
        }
    }

    
    public void sound_move_canon(){

        if(!audio_source_move_canon.isPlaying){
            audio_source_move_canon.volume = 0.1f;
            audio_source_move_canon.clip = move_canon;
            audio_source_move_canon.Play();
        }
    }

    public IEnumerator sound_end_last_pv(){ 
        yield return new WaitForSeconds(0.4f);
        while(player_manager.inst.life_player == 1){
        audio_source_bullet.PlayOneShot(last_life_sound,1f);
        yield return new WaitForSeconds(2f);
        }
    }

   

    // Environnement
    public void sound_friction(){
        audio_source_env.PlayOneShot(friction,1f);
    }

    public void sound_rocks(){
        audio_source_env.PlayOneShot(rocks,0.5f);
    }

 

    // Enemy
    public void sound_nest_move(){
        audio_source_nest.PlayOneShot(move_nest,0.6f);
    }

    public void sound_nest_activate(){
        audio_source_nest.PlayOneShot(active_nest,0.6f);
    }

    public void sound_enemy_activate(){
        audio_source_nest.PlayOneShot(bip_enemy,1f);
    }

    public void sound_enemy_nest(){
        audio_source_env.PlayOneShot(enemy_nest,0.4f);
    }


    public IEnumerator fade_in_nest_move(){ 
        float elapsed = 0.0f;
        float duree = 2f;
        audio_source_nest.volume = 0f;
        float volume = 0f;
        audio_source_nest.clip = move_nest;
        audio_source_nest.Play();

        while( elapsed < duree ){
            volume = Mathf.Lerp(0f,1f,elapsed / duree);
            audio_source_nest.volume = volume;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator fade_out_nest_move(float delay){ 

        audio_source_nest.Play();
        yield return new WaitForSeconds(delay);
        float elapsed = 0.0f;
        float duree = 3f;
        float volume = 1f;
        audio_source_nest.clip = move_nest;
       
        while( elapsed < duree ){
            volume = Mathf.Lerp(1f,0.0f,elapsed / duree);
            audio_source_nest.volume = volume;
            elapsed += Time.deltaTime;
            yield return null;
        }
        audio_source_nest.Stop();
    }


    public void pause_all_sources(){   
        audio_source_player.Pause();
        audio_source_env.Pause();
        audio_source_bullet.Pause();
        audio_source_move.Pause();
        audio_source_zic.Pause();
        audio_source_nest.Pause();
        audio_source_move_canon.Pause();

    }

    public void play_all_sources(){
        audio_source_player.Play();
        audio_source_env.Play();
        audio_source_bullet.Play();
        audio_source_move.Play();
        audio_source_zic.Play();
        audio_source_nest.Play();
    }




    public IEnumerator set_mixer_in_game(float duration, float targetVolume){
        float currentTime = 0f;
        float currentVol = 0f;
        mixer_in_game.GetFloat("volume_in_game", out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        
        while (currentTime < duration){
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            mixer_in_game.SetFloat("volume_in_game", Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }




    public void stop_sound_player_death(){
        audio_source_move.Stop();    
        audio_source_move_canon.Stop();
        audio_source_player.Stop();
    }
}
