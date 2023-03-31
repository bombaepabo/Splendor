using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFin : MonoBehaviour,IDataPersistent
{
   private Player player ;
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue ; 

    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson ; 
    public bool isFinished = false ;
    public bool isFinished2 = false ;
    private bool playerInRange ; 
    [SerializeField] private Door door ; 
    public bool isDoorOpen = false ; 
    private void Awake(){
        playerInRange = false ;
        visualCue.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }
    private void Update(){
        if(Lever.isOpen){
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){
             if(!isFinished){
                visualCue.SetActive(true);
                if(player.inputhandler.GetPickItemPressed()){
                    DialogueManager.GetInstance().EnterDialogueMode(inkJson);
                    isFinished2 = true ;  
                }
                if(isFinished2){
                    door.Open();
                    isDoorOpen = true ; 
                    isFinished = true ; 
                }
            }
        
        }
       if(isDoorOpen == true)
            door.Open();

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
        isDoorOpen = data.isDoorOpenFin ;
        isFinished = data.FinHelpingFinished;
        isFinished2 = data.FinHelpingFinished2 ; 
  }
  public void SaveData(GameData data){
    data.isDoorOpenFin  = isDoorOpen ;
    data.FinHelpingFinished = isFinished ; 
    data.FinHelpingFinished2 = isFinished2;
  }
  
}
