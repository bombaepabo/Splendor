using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormanQuiz2 : MonoBehaviour,IDataPersistent
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
        string Choices = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("NormanQuiz2")).value ; 

        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){

            if(!isFinished){
                visualCue.SetActive(true);
                if(player.inputhandler.GetPickItemPressed()){
                    DialogueManager.GetInstance().EnterDialogueMode(inkJson);

                }
                if(Choices == "1"){
                    isFinished = true ;
                   
                }
        }

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
        isFinished = data.NormanQuiz2isFinished;
  }
  public void SaveData(GameData data){
    data.NormanQuiz2isFinished = isFinished ; 
}
}
