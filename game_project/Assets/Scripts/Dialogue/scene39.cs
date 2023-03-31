using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene39 : MonoBehaviour,IDataPersistent
{
     private Player player ;
    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson ; 
    public bool isFinished = false ;
    [SerializeField]private GameObject Enemy ; 
    [SerializeField] private GameObject APathold ;
    public bool isDestroyEnemyscene39  =false ; 
    private bool playerInRange ; 

    private void Awake(){
        playerInRange = false ;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }
    private void Update(){
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){

            if(playerInRange && !isFinished){
                Enemy.SetActive(false);
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);   
                APathold.SetActive(false);
                isDestroyEnemyscene39 = true ; 
            }
            if(isDestroyEnemyscene39){
                Enemy.SetActive(false);

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
        isFinished = data.scene36_isFinished;
        isDestroyEnemyscene39 = data.scene39isDestroy;
  }
  public void SaveData(GameData data){
    data.scene39isDestroy =  isDestroyEnemyscene39;
    data.scene36_isFinished = isFinished ; 
  }
}
