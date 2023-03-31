using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2_4 : MonoBehaviour,IDataPersistent
{
    private Player player ;
    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson ; 
    public bool isFinished = false ;
    [SerializeField] private GameObject Enemy ; 
    private bool playerInRange ;
    [SerializeField] GameObject Apath;

    public bool isDestroyEnemyChapter2_4 = false ;  

    private void Awake(){
        playerInRange = false ;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }
    private void Update(){
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){

            if(playerInRange && !isFinished){
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);
                Enemy.SetActive(false);
                Apath.SetActive(false);
                isDestroyEnemyChapter2_4 = true ;
            }
            isFinished = true ;

        }
        if(isDestroyEnemyChapter2_4){
            Enemy.SetActive(false);
            Apath.SetActive(false);

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
        isFinished = data.sceneChapter2_4isFinished;
        isDestroyEnemyChapter2_4 = data.Chapter2_4isDestroy ;
  }
  public void SaveData(GameData data){
    data.Chapter2_4isDestroy = isDestroyEnemyChapter2_4 ;
    data.sceneChapter2_4isFinished = isFinished ; 
  }
}
