using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller_cube : MonoBehaviour{

    Animator anim;
    Rigidbody rb;
    GameObject temp_cross_hair;

    PlayerControls gameplay;

    [Header("Characteristic")]
    public bool host;
    public int id_avatar;
    public float speed = 5;
    public float head_rotation_speed = 200f;
    public float smooth_rotate_body = 0.2f;
    public float force_shoot;
    public bool type_catapulte;
    public bool type_canon;
    public bool invulnerable;
    public float speed_boost = 0.3f;
    public float reload_time_boost = 2f;
    public float jump_force = 20f;
    public int nbr_rocket = 4;

    [Header("Edit")]
    public Transform origin_shoot;
    public Transform origin_shoot_rocket1;
    public Transform origin_shoot_rocket2;
    public ParticleSystem shoot_effect_1;
    public ParticleSystem shoot_effect_2;
    public ParticleSystem shoot_effect;
    public ParticleSystem intouchable_particule;
    public GameObject bullet;
    public GameObject bullet_rocket;
    public GameObject particule_cross_hair;
    public GameObject origin_cross_hair;
    public GameObject death_effect;
    public Transform head_turn;
    public Transform pivot_canon;
    public Transform body;
    public ParticleSystem trail_turbo;
    public AudioClip death_sound;
   
    public Slider slider_boost;
    public Animator slider_boost_anim;


    [HideInInspector] public int is_dead = 0;
    [HideInInspector] public int is_shooting = 0;
    [HideInInspector] public bool is_moving;
   
    bool shoot_running;

    Vector2 left_stick;
    Vector2 right_stick;
    float value_bumper_right;

    float TurnSmoothVelocity;

    bool cube_has_boost;
    bool start_timer_boost;
    public bool rocket_mode;
    bool rocket_is_ready;
    bool toggle_canon_rocket;
    int double_shoot = 0;
    public List<GameObject> enemy_to_kill_rocket = new List<GameObject>();
    Transform target_rocket;
    Transform shoot_canon_toggle;

    bool toggle_light;
    public float step_angle_canon = 5f;
    public float initiale_angle = -25f;
    Vector3 currentEulerAngles;
 
    

    
    void Awake(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pivot_canon.localEulerAngles = new Vector3(initiale_angle, 0f, 0f);
        
        if(!host){
            invulnerable = true;
        }

        gameplay = new PlayerControls();

        gameplay.game_pad.left_bump.performed += ctx => press_for_shoot();
        gameplay.game_pad.pause.performed += ctx => button_start();
        gameplay.game_pad.actionA.performed += ctx => interactable_manager.inst.press_for_action();
        gameplay.game_pad.buttonB.performed += ctx => activation_mode_rocket();
        gameplay.game_pad.buttonY.performed += ctx => switch_light();
        gameplay.game_pad.cross_up.performed += ctx => up_canon();
        gameplay.game_pad.cross_down.performed += ctx => down_canon();
      
        gameplay.game_pad.right_bump.performed += ctx => value_bumper_right = ctx.ReadValue<float>();
        gameplay.game_pad.right_bump.canceled += ctx => value_bumper_right = 0f;

        gameplay.game_pad.move_cube.performed += ctx => left_stick = ctx.ReadValue<Vector2>();
        gameplay.game_pad.move_cube.canceled += ctx => left_stick = Vector2.zero;

        gameplay.game_pad.rotate_cube.performed += ctx => right_stick = ctx.ReadValue<Vector2>();
        gameplay.game_pad.rotate_cube.canceled += ctx => right_stick = Vector2.zero;

    }

    void OnEnable(){
        gameplay.game_pad.Enable();
    }

    void OnDisable(){
        gameplay.game_pad.Disable();
    }

   

    void Update(){
 
        
        if(host){

            float r = right_stick.x * Time.deltaTime * head_rotation_speed;
            Vector3 rot = new Vector3 (head_turn.rotation.x,r,head_turn.rotation.z);
            head_turn.Rotate(Vector3.up,r); 

            if(r == 0f){
                sound_manager.inst.audio_source_move_canon.Stop();
            }else{
                play_fx_sound_move_canon();
            }

            float horizontal = left_stick.x;
            float vertical = left_stick.y;
            Vector3 direction = new Vector3(horizontal, 0f, vertical);
            Vector3 move = new Vector3(horizontal, 0f, vertical) * Time.deltaTime * speed;

           
            if(direction.magnitude >= 0.1f){
                float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg; 
                float angle = Mathf.SmoothDampAngle(body.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, smooth_rotate_body);
                body.rotation = Quaternion.Euler(head_turn.rotation.x, angle, head_turn.rotation.z);
                this.transform.Translate(move,Space.World);
                play_fx_sound(Mathf.Clamp(direction.magnitude, 0.4f,1.5f));

                if(player_manager.inst.has_boost && value_bumper_right > 0 && !cube_has_boost && !start_timer_boost){
                    cube_has_boost = true;
                    start_timer_boost = true;
                    StartCoroutine(add_turbo_boost(move));
                }
                if(value_bumper_right <= 0.1f){
                    cube_has_boost = false;
                }
            }
            else{
                sound_manager.inst.audio_source_move.Stop();     
            }

            
            // if(Input.GetKeyDown(KeyCode.Space) && type_catapulte){ 
            //     show_particule_cross_hair();
            // }

            // if(Input.GetKeyUp(KeyCode.Space) && type_catapulte){ 
            //     if(shoot_running) 
            //     return;
            //     shoot_running = true;
            //     StartCoroutine(shoot_catapult());
            // }

        }  
    }


    public void press_for_shoot(){
        if(!host)
        return;
        if(shoot_running)
        return;

        if(type_canon && !rocket_mode){
            shoot_running = true;
            StartCoroutine(shoot3());
        }
        else if (!type_catapulte && !rocket_mode && !rocket_is_ready){
            shoot_running = true;
            StartCoroutine(shoot());
        }
        else if(rocket_is_ready){
            shoot_running = true;
            enemy_to_kill_rocket = new List<GameObject>(enemies_manager.inst.enemy_in_game);
            StartCoroutine(shoot_rocket());
        }
        
    }


    public IEnumerator shoot(){
        if(is_shooting != 1){
            is_shooting = 1;
            player_manager.inst.nbr_shoot++;
            anim.SetTrigger("shoot");
            shoot_effect.Play();
            sound_manager.inst.sound_shoot_player();
            GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
            _bullet.GetComponent<bullet>().id_player_shoot = id_avatar;
            Rigidbody rb = _bullet.GetComponent<Rigidbody>();
            rb.velocity = origin_shoot.transform.TransformDirection(Vector3.forward * force_shoot);
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0; // network
            yield return new WaitForSeconds(0.6f);
            shoot_running = false;
        }
    }

    public IEnumerator shoot3(){
        if(is_shooting != 1){
            is_shooting = 1;
            player_manager.inst.nbr_shoot++;
            anim.SetTrigger("shoot_three");
            shoot_effect.Play();
            sound_manager.inst.sound_bullet_death();
            GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
            _bullet.GetComponent<bullet>().id_player_shoot = id_avatar;
            _bullet.GetComponent<Rigidbody>().useGravity = false;
            _bullet.GetComponent<bullet>().canon = true;
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0;
            yield return new WaitForSeconds(1.5f);
            shoot_running = false;
        }
    }

   
    public IEnumerator shoot_catapult(){
        anim.SetTrigger("shoot_two");
        if(temp_cross_hair != null){
            temp_cross_hair.transform.parent = null;
        }
        yield return new WaitForSeconds(0.07f);
        if(is_shooting != 1){
            is_shooting = 1;
            player_manager.inst.nbr_shoot++;
            sound_manager.inst.sound_shoot2_player();
            GameObject _bullet = Instantiate(bullet, origin_shoot.position, origin_shoot.rotation);
            _bullet.GetComponent<bullet>().id_player_shoot = id_avatar;
            _bullet.GetComponent<bullet>().catapult = true;
            Rigidbody rigid = _bullet.GetComponent<Rigidbody>();
            rigid.velocity = origin_shoot.transform.TransformDirection(Vector3.forward * force_shoot);
            yield return new WaitForSeconds(0.02f);
            is_shooting = 0;
        }
        yield return new WaitForSeconds(1f);
        shoot_running = false;
    }


    public void controller_dead(){
        if(dev_script.inst.invincible)
        return;

        if(!invulnerable){
            GameObject _death = Instantiate(death_effect, transform.position, transform.rotation);
            sound_manager.inst.stop_sound_player_death();
            Destroy(_death,6f); 
            is_dead = 1;
            sound_manager.inst.sound_death_player(death_sound);
            if(host){
                player_manager.inst.player_dead();
            }
            Destroy(this.gameObject); 
            print("player destroy");
        }     
    }




    void play_fx_sound(float pitch){
        if(is_dead == 0){
            sound_manager.inst.sound_move(pitch);  
        }   
    }

    void play_fx_sound_move_canon(){
        if(is_dead == 0){
            sound_manager.inst.sound_move_canon();  
        }   
    }





    void show_particule_cross_hair(){
        temp_cross_hair = Instantiate(particule_cross_hair,origin_cross_hair.transform);
        temp_cross_hair.GetComponent<ParticleSystem>().Play();
        Destroy(temp_cross_hair,5f);
    }

    // trigger player_manager
    public IEnumerator player_invulnerable(){
        if(player_manager.inst.life_player < 3){
            intouchable_particule.Play();
            yield return new WaitForSeconds(0.5f);
            intouchable_particule.Stop();
            yield return new WaitForSeconds(1.5f);
            sound_manager.inst.sound_end_intouched();
            yield return new WaitForSeconds(1.5f);
            invulnerable = false;
        }else{
            yield return new WaitForSeconds(1f);
            invulnerable = false;
        }
    }

    // trigger gamepad
    public void button_start(){
        if(!ui_manager.inst.menu_in_game.activeSelf){
            ui_manager.inst.show_menu_paused(); 
            sound_manager.inst.audio_source_move.volume = 0f;
        }else{
            ui_manager.inst.click_button_back_to_game();
        }
    }


    IEnumerator add_turbo_boost(Vector3 _move){

        float elapsed = 0f;
        float duree = 0.2f;

        trail_turbo.Play();
        slider_boost.value = 0f;
        slider_boost_anim.SetBool("boost",true);
        sound_manager.inst.sound_turbo();
        sound_manager.inst.sound_reload_boost();

        while(elapsed < duree ){
            this.transform.Translate(_move + new Vector3(left_stick.x * speed_boost, 0f, left_stick.y * speed_boost),Space.World);
            float boost_bar_value = Mathf.Lerp(1,0, elapsed / duree);
            elapsed += Time.deltaTime;
            yield return null;
        }

        trail_turbo.Stop();

        elapsed = 0f;
        duree = reload_time_boost;

        yield return new WaitForSeconds(0.5f);

        while(elapsed < duree ){
            float boost_bar_value = Mathf.Lerp(0,1, elapsed / duree);
            slider_boost.value = boost_bar_value;
            elapsed += Time.deltaTime;
            yield return null;
        }

        sound_manager.inst.audio_source_player.Stop();
        sound_manager.inst.sound_full_boost();

        slider_boost.value = 1f;
        slider_boost_anim.SetBool("boost",false);
        yield return new WaitForSeconds(0.2f);
        start_timer_boost = false;
    }




    // trigger button b
    void activation_mode_rocket(){
        if(!host)
        return;
        if(!player_manager.inst.has_rocket)
        return;
        if(!rocket_mode){ 
            rocket_mode = true;
            anim.SetBool("mode_rocket",true);
            ui_manager.inst.rocket_nbr.text = nbr_rocket.ToString();
            sound_manager.inst.sound_rocket_mode();
            Invoke("enable_rocket",1.6f);
            return;
        }else{
            rocket_mode = false;
            sound_manager.inst.sound_rocket_mode();
            anim.SetBool("mode_rocket",false);
            ui_manager.inst.rocket_container.SetBool("show",false);
            rocket_is_ready = false; 
        }
    }

    void enable_rocket(){
        rocket_is_ready = true; 
        ui_manager.inst.rocket_container.SetBool("show",true);
    }










    public IEnumerator shoot_rocket(){

        if(nbr_rocket == 0){
            sound_manager.inst.sound_no_rocket();
            ui_manager.inst.rocket_container.SetTrigger("no_rocket");
            yield return new WaitForSeconds(1f);
            shoot_running = false;
            yield break;
        }

        double_shoot = 2;

        while(double_shoot > 0){

            toggle_canon_rocket = !toggle_canon_rocket;
            shoot_canon_toggle = toggle_canon_rocket ? origin_shoot_rocket1 : origin_shoot_rocket2;
            int canon_rocket = toggle_canon_rocket ? 0 : 1;
            ui_manager.inst.set_rocket_img(canon_rocket,0);
            search_enemy();
            create_bullet();
            double_shoot--;
            yield return new WaitForSeconds(0.6f); // cadence double shoot
        }

        if(nbr_rocket > 0){
            StartCoroutine(ui_manager.inst.anim_rocket_img(2f));
        }
        yield return new WaitForSeconds(2f); // cadence de tir
            
        shoot_running = false;
        enemy_to_kill_rocket.Clear();    
    }



    void create_bullet(){
        player_manager.inst.nbr_shoot++;
        if(toggle_canon_rocket){
            shoot_effect_1.Play();
            anim.SetTrigger("shoot_right");
        }else{
            shoot_effect_2.Play();
            anim.SetTrigger("shoot_left");
        }
        sound_manager.inst.sound_rocket();
        GameObject _bullet = Instantiate(bullet_rocket, shoot_canon_toggle.position, shoot_canon_toggle.rotation);
        bullet my_bullet = _bullet.GetComponent<bullet>();
        my_bullet.id_player_shoot = id_avatar;
        my_bullet.rocket_follow_enemy = true;
        my_bullet.target = target_rocket;
        _bullet.GetComponent<Rigidbody>().useGravity = false;
        nbr_rocket --;
        ui_manager.inst.refresh_nbr_rocket(nbr_rocket);
    }


    void search_enemy(){
        if(enemy_to_kill_rocket.Count > 0){
            target_rocket = enemy_to_kill_rocket[Random.Range(0,enemy_to_kill_rocket.Count)].transform;
            enemy_to_kill_rocket.Remove(target_rocket.gameObject); 
        }    
        else{
            target_rocket = null;
        }   
    }

    // anim_rocket
    void mode_rocket_ready(){
        rocket_mode = true;
    }



    
    public void pick_up_box_rocket(int value){
        StartCoroutine(show_rocket_win(value));       
    }


    IEnumerator show_rocket_win(int value){

        if(!rocket_mode){
            nbr_rocket += value;
            yield break;
        }

        ui_manager.inst.rocket_container.SetBool("show",true);
      
        yield return new WaitForSeconds(0.5f);

        int i = value;
        int total = nbr_rocket + value;

        while(nbr_rocket < total){
            ui_manager.inst.set_rocket_img(0,1);
            ui_manager.inst.set_rocket_img(1,1);
            nbr_rocket++;
            ui_manager.inst.refresh_nbr_rocket(nbr_rocket);
            sound_manager.inst.sound_reload_rocket();
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);
        if(!rocket_mode){
            ui_manager.inst.rocket_container.SetBool("show",false);
        }
    }




    void switch_light(){
        if(!host)
        return;
        if(!player_manager.inst.has_light)
        return;
        toggle_light = !toggle_light;
        anim.SetBool("light",toggle_light);
        sound_manager.inst.sound_light_mode();
    }
   

    void up_canon(){
         if(!host)
        return;
        currentEulerAngles -= new Vector3(step_angle_canon, 0, 0);
        currentEulerAngles.x = currentEulerAngles.x < -25f ? -25f : currentEulerAngles.x;
        pivot_canon.localEulerAngles = currentEulerAngles;
    }

    void down_canon(){
         if(!host)
        return;
        currentEulerAngles += new Vector3(step_angle_canon, 0, 0);
        currentEulerAngles.x = currentEulerAngles.x > 0f ? 0f : currentEulerAngles.x;
        pivot_canon.localEulerAngles = currentEulerAngles;
    }

   


    
   



    

    
    

   




}
