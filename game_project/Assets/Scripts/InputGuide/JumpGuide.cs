using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGuide : MonoBehaviour
{
    private Animator GuideAnimator;
    private Player player ;
    private string ControllerType = "";  
    private bool inArea = false ;

    void Start()
    {
        GuideAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    void Update()
    {
       ControllerType = player.inputhandler.GetControlType();
       if(inArea){

       if(ControllerType == "GamePad"){
                GuideAnimator.Play(ControllerType);
             }
       if(ControllerType == "Keyboard"){
                GuideAnimator.Play(ControllerType);
             }
       }
       else{
                StartCoroutine("Delay",10f);
                GuideAnimator.Play("Idle");


       }
       
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name.Equals("Player")){
            inArea = true ; 
             Debug.Log(ControllerType);
             
        }
    }
     void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.name.Equals("Player")){
            inArea = false ; 

            Debug.Log(ControllerType);
        }
    }
    IEnumerator Delay(float DelayTime){
        yield return new WaitForSeconds(DelayTime);
    }
    
}
