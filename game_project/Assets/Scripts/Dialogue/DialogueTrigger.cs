using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour,IDataPersistent
{
    private Player player ;
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue ; 

    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson ; 
    [SerializeField] private TextAsset AfterTalkInkJson ; 
    public bool isFinished = false ;

    private bool playerInRange ; 

    private void Awake(){
        playerInRange = false ;
        visualCue.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }
    private void Update(){
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){

            if(playerInRange && !isFinished){
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);
                
            }
            if(isFinished){
                visualCue.SetActive(true);
                if(player.inputhandler.GetPickItemPressed()){
                    DialogueManager.GetInstance().EnterDialogueMode(AfterTalkInkJson);

                }

            }
            isFinished = true ;
        }
        else{
            visualCue.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            playerInRange = true ; 
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player"){
            playerInRange = false ; 
        }
    }
     public void LoadData(GameData data){
        isFinished = data.isFinishedAbelintro;
  }
  public void SaveData(GameData data){
    data.isFinishedAbelintro = isFinished ; 
  }
}
