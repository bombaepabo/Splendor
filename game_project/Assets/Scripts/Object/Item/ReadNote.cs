using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadNote : MonoBehaviour
{
    private Player player ;
    [SerializeField] private Button ExitButton ;
    [SerializeField] private GameObject NoteUI ;
    [SerializeField] private string context ; 
    [SerializeField] private TextMeshProUGUI text ;
    [SerializeField] private GameObject visualCue ; 
    public static bool IsPressNoted = false ; 
    public bool IsTouchingNoted = false ;
    public bool isinuse = false ;
    void Start()
    {
        visualCue.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(" IsPressNoted: "+ IsPressNoted+" IsTouchingNoted: " + IsTouchingNoted);
    }
    void FixedUpdate(){
         text.text = context;
        if(IsTouchingNoted){
            visualCue.SetActive(true);

        if(player.inputhandler.GetPickItemPressed()&&!isinuse){
                isinuse = true ;
                NoteUI.SetActive(true);
                if(isinuse){
                //Debug.Log("in use "+isinuse);
                //player.StateMachine.ChangeState(player.IdleState);
                //player.EnableMovement();
                player.inputhandler.DisableInput();
                } 
                
            }
        }
        else{

            visualCue.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider){
        
        if(collider.gameObject.tag == "Player"){
            IsTouchingNoted = true ;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collider){
           if(collider.gameObject.tag == "Player"){
            IsTouchingNoted = false ;
            
        }
    }
    public void OnExitButton(){
        isinuse = false ;
        NoteUI.SetActive(false); 
        Debug.Log("PressExit");
        //player.EnableMovement();
        player.inputhandler.EnableInput();


    }
}
