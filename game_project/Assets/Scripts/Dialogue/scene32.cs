using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene32 : MonoBehaviour,IDataPersistent
{
   private Player player ;
    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson ; 
    public bool isFinished = false ;
    [SerializeField]private GameObject Enemy ; 
    private Vector2 initspawnEnemy ; 
    private float timer ;
    private bool playerInRange ; 
    [SerializeField] private GameObject APathold ;
    [SerializeField] private GameObject APathnew ;

    private void Awake(){
        playerInRange = false ;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        initspawnEnemy =  Enemy.transform.position ; 

    }
    private void Update(){
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){

            if(playerInRange && !isFinished){
            DialogueManager.GetInstance().EnterDialogueMode(inkJson);   
            }
            isFinished = true ;
        }
        if(isFinished && !DialogueManager.GetInstance().dialogueIsPlaying){
            APathold.SetActive(false);
            APathnew.SetActive(true);
            Enemy.SetActive(true);
            }
        if(player.playerData.CurrentHealth <= 0 ){
            Enemy.SetActive(false);
            Enemy.transform.position = player.SpawnPointEnemy  ;
            
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
        isFinished = data.scene32isFinished;
  }
  public void SaveData(GameData data){
    data.scene32isFinished = isFinished ; 
  }
    }

