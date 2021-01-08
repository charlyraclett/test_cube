using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_anim : MonoBehaviour
{
    Animator anim;
    public int id;
    Quaternion init;



    void Start(){

        init = gameObject.transform.rotation;
        anim = GetComponent<Animator>();
      
    }

    public void change_color(int value){
        gameObject.GetComponent<MeshRenderer>().material = network.inst.color[value];
    }


   
    void play(){

        switch(id){
            case 0 :  anim.SetTrigger("shoot"); break;
            case 1 :  anim.SetTrigger("shoot_two"); break;
            case 2 :  anim.SetTrigger("shoot_three"); break;
        }
    }



    public IEnumerator turn_objet(){

        InvokeRepeating("play", 0.5f, 2f);
        while(true){
            transform.Rotate(Vector3.up, 80 * Time.deltaTime);
            yield return null;
        } 
    }

    public void turn(){
        StartCoroutine(turn_objet());
    }

    public void stop_turn(){
        CancelInvoke();
        StopAllCoroutines();
        gameObject.transform.rotation = init;
    }

}
