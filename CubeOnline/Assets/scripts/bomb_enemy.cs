using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_enemy : MonoBehaviour{

    public Animator anim;
  
    [Header("Characteristic")]
    public float force_shoot = 5f;
    public float cadence_shoot = 1f;
    public int nbr_shoot;
    public float delay_each_wave;
    public bool is_rotate;
    public float speed_rotation = 90;

    [Header("Edition")]
    public Transform[] origin_shoot;
    public GameObject shoot_effect;
    public GameObject death_effect;
    public GameObject bullet;

    [HideInInspector] public bool has_initialize;

   
    public void initialize(){
        anim.SetTrigger("active");
        GetComponent<BoxCollider>().enabled = true;
        sound_manager.inst.sound_enemy_activate();
        StartCoroutine(rotate());
        StartCoroutine(sequence_shoot(0.1f));
        has_initialize = true;
    }

    
    
    IEnumerator rotate(){
        while(is_rotate){
            transform.Rotate(Vector3.up, speed_rotation * Time.deltaTime);
            yield return null;
        }
    }


    IEnumerator sequence_shoot(float delay){
        yield return new WaitForSeconds(delay);
        float d = delay_each_wave;
        int i = nbr_shoot;
        while(i > 0){
            shoot();
            i--;
            yield return new WaitForSeconds(cadence_shoot);
        }
        while(d > 0){
            d--;
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(sequence_shoot(0f));
    }




    void shoot(){

        sound_manager.inst.sound_bullet_death();
        anim.SetTrigger("shoot");

        for(int i = 0; i < origin_shoot.Length;i++){
            GameObject _bullet = Instantiate(bullet, origin_shoot[i].position, origin_shoot[i].rotation);
            Destroy(Instantiate(shoot_effect, origin_shoot[i].position, origin_shoot[i].rotation),1f);
            bullet_enemy bullet_enemy= _bullet.GetComponent<bullet_enemy>();
            setting_bullet(bullet_enemy);
        }
    }



    void setting_bullet(bullet_enemy _bullet){
        _bullet.GetComponent<Rigidbody>().useGravity = false;
        _bullet.canon = true;
        _bullet.speed = force_shoot;
    }





    public void agent_dead(){
        Destroy(Instantiate(death_effect, transform.position, transform.rotation),6f); 
        sound_manager.inst.sound_bomb_death();  
        enemies_manager.inst.enemy_in_game.Remove(this.gameObject);
        level_manager.inst.remove_enemy_in_game();
        player_manager.inst.enemies_killed++;
        Destroy(this.gameObject);        
    }












}
