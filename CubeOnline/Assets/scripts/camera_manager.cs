using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camera_manager : MonoBehaviour{

    public static camera_manager inst;


    public CinemachineVirtualCamera cam_game;
    public CinemachineVirtualCamera cam_move;
    public Transform target_cam_player;




   
    void Start(){
        inst = this;   
    }

   




    public IEnumerator switch_cam_game(){
        yield return new WaitForSeconds(0.5f);
        if(floor_creation.inst.colonne > 11){
            move_cam_with_player();
        }
        else{
            cam_game.Priority = 15;
        }
    }



    public void move_cam_with_player(){
        cam_move.Follow = target_cam_player;
        cam_move.LookAt = target_cam_player;
        cam_move.Priority = 15;
    }





    public IEnumerator start_shake_cam(){ 
        
        CinemachineBasicMultiChannelPerlin perlin = cam_game.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        float elapsed = 0.0f;
        float duree = 0.1f; 
        while( elapsed < duree ){
            perlin.m_AmplitudeGain = Mathf.Lerp(0f,1f,elapsed / duree);
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        elapsed = 0.0f;
        duree = 1f; 
        while( elapsed < duree ){
            perlin.m_AmplitudeGain = Mathf.Lerp(1f,0f,elapsed / duree);
            elapsed += Time.deltaTime;
            yield return null;
        }
        perlin.m_AmplitudeGain = 0f;  
    }
      
    
}
