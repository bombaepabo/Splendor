using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGuide : MonoBehaviour,IDataPersistent
{
    private Animator GuideAnimator;
    private Player player ;
    private string ControllerType = "";  
    private bool inArea = false ;
    [SerializeField] private GameObject Abel ; 
    private bool isDestroy;
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
       if(isDestroy){
           Destroy(Abel);
       }
       
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name.Equals("Player")){
            inArea = true ; 
             Destroy(Abel);
             isDestroy = true ; 
             
             
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
    public void LoadData(GameData data){
        isDestroy = data.isDestroyintroAbel;
  }
  public void SaveData(GameData data){
    data.isDestroyintroAbel = isDestroy ; 
  }
    
}
