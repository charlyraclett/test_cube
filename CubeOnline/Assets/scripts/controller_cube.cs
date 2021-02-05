using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller_cube : MonoBehaviour{

    Animator anim;
    Rigidbody rb;
    GameObject temp_cross_hair;

    //PlayerControls gameplay;

    [Header("Characteristic")]
    public bool host;
    public int id_avatar;
    public float speed = 5;
    public float speed_rotation = 1;
    public float force_shoot;
    public bool type_catapulte;
    public bool type_canon;
    public bool invulnerable;

    [Header("Edit")]
    public Transform origin_shoot;
    public ParticleSystem shoot_effect;
    public ParticleSystem intouchable_particule;
    public GameObject bullet;
    public GameObject particule_cross_hair;
    public GameObject origin_cross_hair;
    public GameObject death_effect;

    [HideInInspector]public int is_dead = 0;
    [HideInInspector] public int is_shooting = 0;
    [HideInInspector]public bool is_moving;
    [HideInInspector]public bool key_holding;
    bool shoot_running;

    Vector2 left_stick;
    Vector2 right_stick;


    
    void Awake(){
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        if(!host){
            invulnerable = true;
        }

        // gameplay = new PlayerControls();
        // gameplay.game_pad.shoot.performed += ctx => press_for_shoot();
        // gameplay.game_pad.pause.performed += ctx => button_start();

        // gameplay.game_pad.move_cube.performed += ctx => left_stick = ctx.ReadValue<Vector2>();
        // gameplay.game_pad.move_cube.canceled += ctx => left_stick = Vector2.zero;

        // gameplay.game_pad.rotate_cube.performed += ctx => right_stick = ctx.ReadValue<Vector2>();
        // gameplay.game_pad.rotate_cube.canceled += ctx => right_stick = Vector2.zero;

    }

    // void OnEnable(){
    //     gameplay.game_pad.Enable();
    // }

    // void OnDisable(){
    //     gameplay.game_pad.Disable();
    // }
    

    void Update(){
 
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin,direction);



        if (Physics.Raycast(ray, out RaycastHit raycastHit)){
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.green);
            float distance = Vector3.Distance (transform.position, raycastHit.transform.transform.position);
          //  Debug.Log("Did Hit "+ raycastHit.transform.tag+" distance : "+distance);

        }
        else{
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.white);
        }


        if (rb.velocity.y < -15f){
            invulnerable = false;
            controller_dead();
        }

        if(host){

            // float x = left_stick.x * Time.deltaTime * speed;
            // float y = left_stick.y * Time.deltaTime * speed;
            // transform.Translate(x, 0, y);
            // float r = right_stick.x * Time.deltaTime * speed_rotation * 100f;
            // Vector3 rot = new Vector3 (transform.rotation.x,r,transform.rotation.z)
          

           

            if(Input.GetKey(KeyCode.UpArrow)){  
                this.transform.Translate(Vector3.forward * Time.deltaTime * speed); 
                play_fx_sound();
            }  
            
            if(Input.GetKey(KeyCode.DownArrow)){  
                this.transform.Translate(Vector3.back * Time.deltaTime * speed);  
                play_fx_sound();
            }  
            
            if(Input.GetKey(KeyCode.LeftArrow)){    
                this.transform.Rotate(Vector3.up,-speed_rotation); 
                play_fx_sound();
 
            }  
            
            if(Input.GetKey(KeyCode.RightArrow)){  
                this.transform.Rotate(Vector3.up,speed_rotation);
                play_fx_sound();  
            } 
           

            if(!Input.anyKey && key_holding) {
                key_holding = false;
                sound_manager.inst.audio_source_move.Stop();     
            }

            if(Input.GetKeyDown(KeyCode.Space) && type_catapulte){ 
                show_particule_cross_hair();
            }

            if(Input.GetKeyUp(KeyCode.Space) && type_catapulte){ 
                if(shoot_running) 
                return;
                shoot_running = true;
                StartCoroutine(shoot_catapult());
            }

            if (Input.GetKeyDown(KeyCode.Space)){ 

                press_for_shoot();
            }
        }  
    }


    public void press_for_shoot(){
        if(shoot_running)
        return;
        if(type_canon){
            shoot_running = true;
            StartCoroutine(shoot3());
        }
        else if (!type_catapulte){
            shoot_running = true;
            StartCoroutine(shoot());
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

            _bullet.GetComponent<bullet>().target = network.inst.avatars_pos[3]; // test bullet  tete chercheuse

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
            rb.velocity = transform.TransformDirection(Vector3.back * 3f);
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


    // trigger bullet
    public void controller_dead(){
        if(!invulnerable){
            GameObject _death = Instantiate(death_effect, transform.position, transform.rotation);
            sound_manager.inst.audio_source_move.Stop();    
            Destroy(_death,6f); 
            is_dead = 1;
            sound_manager.inst.sound_death_player();
            if(host){
                player_manager.inst.player_dead();
            }
            Destroy(this.gameObject); 
        }     
    }




    void play_fx_sound(){
        if(is_dead == 0){
            key_holding = true;
            sound_manager.inst.sound_move();  
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
        }else{
            ui_manager.inst.click_button_back_to_game();
        }
    }

       
    

   




}
