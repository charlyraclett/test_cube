using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour{

    public GameObject impact;
    public int id_player_shoot;
    public bool no_death_touch_ground;
    public float speed = 5f;
    bool death;
   

    void OnTriggerEnter(Collider col){ 

        if(col.tag == "ground"){
            bullet_death();   
        }

        else if(col.tag == "Player"){
            print("touch player");
            death = true;
            col.gameObject.GetComponent<controller_cube>().controller_dead();
            network.inst.add_score_for(id_player_shoot);
            bullet_death();
        }
    }


     
    public IEnumerator simple_bullet(){  

        Invoke("destroy",5f);
        while(!death){
            transform.position += transform.forward * speed * Time.deltaTime; // forward
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject, 5f); 
    }


    void bullet_death(){
        death = true;
        float delay = no_death_touch_ground ? 4f : 0f;
        StartCoroutine(death_after(delay));
        Destroy(this.gameObject, 5f);
    }

    IEnumerator death_after(float delay){

        yield return new WaitForSeconds(delay);
        GameObject _impact = Instantiate(impact,transform.position,transform.rotation);
        _impact.GetComponent<ParticleSystem>().Play();
        Destroy(_impact,4f); 
        sound_manager.inst.sound_bullet_death();
        Destroy(this.gameObject);

    }

    void destroy(){
        death = true;
    }




}
