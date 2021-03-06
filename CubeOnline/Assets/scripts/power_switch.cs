﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class power_switch : MonoBehaviour{

    AudioSource audio_source;


    [Header("Characteristic")]
    public bool first_switch;
    public string code;
    public bool has_help_sound;
  
    [Header("Edit")]
    public collider_switch_power[] switchs;
    public switch_light switch_light;
    public electric_generator _generator;
    public Animator tooltips;

  
    [Header("Edit Sound")]
    public AudioClip code_wrong_sound;
    public AudioClip code_good_sound;
    public AudioSource audio_source_pitch;
   
    [Header("Info")]
    public string receivecode;

    bool power_is_activate;
    float pitch_sound = 1f;
    int index = 0;

    int count_essai;


    void Start(){
        audio_source = GetComponent<AudioSource>();
    }



    // trigger collider button
    public void data_button(collider_switch_power my_switch){
        check_code(my_switch.id_switch);
    }




    void check_code(string number_code){

        if(power_is_activate)
        return;

        if(receivecode.Length != code.Length){
            receivecode += number_code;
            play_sound_each_code(number_code);
        }

        if(receivecode.Length == code.Length){
            if(receivecode == code){
                StartCoroutine(good_code());
            }else{
                StartCoroutine(reinitialze_switchs());
                if(!first_switch)
                show_tips_code();
            }
        }
    }


    IEnumerator good_code(){

        power_is_activate = true;

        yield return new WaitForSeconds(1f);

        audio_source.PlayOneShot(code_good_sound,1f);
        for(int i = 0; i < switchs.Length; i++){
            switchs[i].confirm_code();  
        }

        yield return new WaitForSeconds(1f);
        switch_light.switch_is_ready();
        
    }



    IEnumerator reinitialze_switchs(){
        yield return new WaitForSeconds(1f);
        audio_source.PlayOneShot(code_wrong_sound,1f);      
        receivecode = "";
        for(int i = 0; i < switchs.Length; i++){
            switchs[i].reinitialize_switch(); 
        }
        pitch_sound = 1f;
        index = 0;
    }


    void play_sound_each_code(string number_code){

        if(has_help_sound){

            char c = number_code[0];
            char d = code[index];

            if(c == d){
                pitch_sound += 0.3f;
                index++;
            }else{
                pitch_sound = 1f;
                index = 0;
            }
        }

        audio_source_pitch.pitch = pitch_sound;
        audio_source_pitch.volume = 0.8f;
        audio_source_pitch.Play();
    }


    void show_tips_code(){
        count_essai++; 
        if(count_essai == 3){ 
            tooltips.SetTrigger("show");
            count_essai = 0;
        } 
    }


}
