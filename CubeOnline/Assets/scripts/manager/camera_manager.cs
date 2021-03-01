using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camera_manager : MonoBehaviour{

    public static camera_manager inst;

    [Header("Edition")]
    public Camera main_camera;
    public CinemachineVirtualCamera cam_game;
    public CinemachineVirtualCamera cam_move;
    public CinemachineVirtualCamera cam_player_finish;
    public CinemachineVirtualCamera cam_player_gameover;
    public CinemachineDollyCart dolly_cam_gameover;

    [Header("Info")]
    public Transform target_cam_player;

    void Awake(){
        inst = this;   
    }

    public IEnumerator switch_cam_game(){
        yield return new WaitForSeconds(0.5f);
        cam_game.Priority = 15;   
    }

    public void move_cam_with_player(){
        cam_move.Follow = target_cam_player;
        cam_move.LookAt = target_cam_player;
        cam_move.Priority = 15;
    }


    public IEnumerator start_shake_cam(){ 
        yield return new WaitForSeconds(0.1f);
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


    public void show_cam_player_win(){
       cam_player_finish.LookAt = player_manager.inst.my_avatar.transform;
       cam_player_finish.Follow = player_manager.inst.my_avatar.transform;
       cam_player_finish.Priority = 20;
    }

    public void reset_cameras(){
        cam_player_finish.Priority = 0;
        cam_player_gameover.Priority = 0;
        dolly_cam_gameover.m_Position = 0;
        dolly_cam_gameover.m_Speed = 0;
    }


    public void show_cam_gameover(){
        cam_player_gameover.Priority = 20;
        dolly_cam_gameover.m_Speed = 1;
    }





    public IEnumerator start_shake_cam_pressoir(){ 
       
        CinemachineBasicMultiChannelPerlin perlin = cam_game.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        float elapsed = 0.0f;
        float duree = 0.1f; 
      
        perlin.m_AmplitudeGain = 1f;
        
        yield return new WaitForSeconds(0.2f);
        elapsed = 0.0f;
        duree = 0.1f; 
        while( elapsed < duree ){
            perlin.m_AmplitudeGain = Mathf.Lerp(1f,0f,elapsed / duree);
            elapsed += Time.deltaTime;
            yield return null;
        }
        perlin.m_AmplitudeGain = 0f;  
    }
      
    
}
