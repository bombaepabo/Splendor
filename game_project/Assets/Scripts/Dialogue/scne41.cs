using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scne41 : MonoBehaviour
{
    private Player player ;
    [SerializeField]private GameObject Enemy ; 
    [Header("Visual Cue")]

    [SerializeField] private GameObject visualCue ; 
    [SerializeField] private GameObject AbelVisual ; 
    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson ; 
    public bool isFinished = false ;
   [SerializeField] private GameObject APathold ;
    [SerializeField] private GameObject APathnew ;
    private bool playerInRange ; 
    private float timer ;

    [SerializeField] private GameObject self ;
   private void Awake(){
        playerInRange = false ;
        visualCue.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }
    private void Update(){
        Debug.Log(player.SpawnPointEnemy);
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){
             if(!isFinished){
                visualCue.SetActive(true);
                if(player.inputhandler.GetPickItemPressed()){
                    DialogueManager.GetInstance().EnterDialogueMode(inkJson);
                    isFinished = true ;
                }
            }
            if(isFinished && !DialogueManager.GetInstance().dialogueIsPlaying){
            AbelVisual.SetActive(false);
            APathold.SetActive(false);
            APathnew.SetActive(true);
            Enemy.SetActive(true);            
            }
        }
        else if(player.DeathState.isDead ){
            Enemy.transform.position = player.SpawnPointEnemy  ;
            
            //Enemy.SetActive(false);
            timer += Time.fixedDeltaTime;
            if(!player.DeathState.isDead && timer >=3){
                
                Enemy.SetActive(true); 
                timer = 0 ; 
            }
            Debug.Log("Enter");            
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
        isFinished = data.HanCampisFinished;
  }
  public void SaveData(GameData data){
    data.HanCampisFinished = isFinished ; 
  }
}
