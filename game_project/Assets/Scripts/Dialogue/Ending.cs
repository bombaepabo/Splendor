using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private Animator _anim ; 
    public GameObject virtualCam ; 
        public GameObject virtualCam2 ; 

    private Player player ;
    [Header("Ink Json")]
    [SerializeField] private TextAsset inkJson ; 
    private bool playerInRange ; 
    public static bool reachingEnd = false ;

    public bool isFinished = false ;
    private void Awake(){
        playerInRange = false ;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
         if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying){

            if(playerInRange && !isFinished){
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);   
                isFinished = true ;
                virtualCam.SetActive(true);
            }
            if(isFinished&&!DialogueManager.GetInstance().dialogueIsPlaying){
                virtualCam2.SetActive(false);
               
                _anim.SetBool("Ending",true);
            }
        }

    }
     private void OnTriggerEnter2D(Collider2D other){
         if(other.CompareTag("Player")){
            reachingEnd = true ;
            playerInRange = true ; 

         }

     }
     
}
