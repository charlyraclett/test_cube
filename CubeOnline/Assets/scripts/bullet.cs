using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour{
    private const float V = 3f;
    public GameObject impact;
    public int id_player_shoot;
    public bool no_death_touch_ground;




   
    void OnTriggerEnter(Collider col){ 

        if(col.tag == "ground"){
            bullet_death();   
        }

        else if(col.tag == "Player"){
            print("touch player");
            col.gameObject.GetComponent<controller_cube>().controller_dead();
            network.inst.add_score_for(id_player_shoot);
            bullet_death();
        }
    }


    void bullet_death(){

        float delay = no_death_touch_ground ? 4f : 0f;
        StartCoroutine(death_after(delay));
    }

    IEnumerator death_after(float delay){

        yield return new WaitForSeconds(delay);
        GameObject _impact = Instantiate(impact,transform.position,transform.rotation);
        _impact.GetComponent<ParticleSystem>().Play();
        Destroy(_impact,4f);  
        Destroy(gameObject);
    }




}
