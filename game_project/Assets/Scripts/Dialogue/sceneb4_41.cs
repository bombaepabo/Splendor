using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneb4_41 : MonoBehaviour
{
     private Player player ;
    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson ; 
    public bool isFinished = false ;

    private bool playerInRange ; 

    private void Awake(){
        playerInRange = false ;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }
    private void Update(){
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){

            if(playerInRange && !isFinished){
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);   
            }
            isFinished = true ;
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
        isFinished = data.isFinishedsceneb4_41;
  }
  public void SaveData(GameData data){
    data.isFinishedsceneb4_41 = isFinished ; 
  }
}
