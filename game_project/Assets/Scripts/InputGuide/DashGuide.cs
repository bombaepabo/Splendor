using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGuide : MonoBehaviour
{ private Animator GuideAnimator;
    private Animator GuideAnimator2;

    private Player player ;
    private string ControllerType = "";  
    private bool inArea = false ;
    [SerializeField] private GameObject direction ;
    [SerializeField] private GameObject Shift ;
    [SerializeField] private GameObject Text ;




    void Start()
    {
        GuideAnimator = direction.GetComponent<Animator>();
        GuideAnimator2 = Shift.GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    void Update()
    {
        ControllerType = player.inputhandler.GetControlType();
       if(inArea){

       if(ControllerType == "GamePad"){
                GuideAnimator.Play(ControllerType);
                GuideAnimator2.Play(ControllerType);
                Text.SetActive(true);
             }
       if(ControllerType == "Keyboard"){
                GuideAnimator.Play(ControllerType);
                GuideAnimator2.Play(ControllerType);
                Text.SetActive(true);

             }
       }
       else{
                StartCoroutine("Delay",10f);
                GuideAnimator.Play("Idle");
                GuideAnimator2.Play("Idle");
                Text.SetActive(false);



       }
       
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name.Equals("Player")){
            inArea = true ; 
             
        }
    }
     void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.name.Equals("Player")){
            inArea = false ; 

        }
    }
    IEnumerator Delay(float DelayTime){
        yield return new WaitForSeconds(DelayTime);
    }
    
}
