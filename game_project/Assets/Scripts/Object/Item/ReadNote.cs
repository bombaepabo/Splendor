using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReadNote : MonoBehaviour
{
    private Player player ;
    [SerializeField] private Button ExitButton ;
    [SerializeField] private GameObject NoteUI ;
    public static bool IsPressNoted = false ; 
    public bool IsTouchingNoted = false ;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if(IsPressNoted && IsTouchingNoted){
                NoteUI.SetActive(true); 
                player.inputhandler.DisableInput();

            }
        else{
                player.inputhandler.EnableInput();
                NoteUI.SetActive(false);
            }
        //Debug.Log(" IsPressNoted: "+ IsPressNoted+" IsTouchingNoted: " + IsTouchingNoted);
    }
    void FixedUpdate(){
        
    }
     void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.gameObject.name.Equals("Player")){
            IsTouchingNoted = true ;
        }
    }
    void OnTriggerExit2D(Collider2D collision){
            IsPressNoted = false ;
            IsTouchingNoted = false ;
            NoteUI.SetActive(false);
    }
    public void OnExitButton(){
        NoteUI.SetActive(false); 
        Debug.Log("PressExit");
        IsPressNoted = false ; 

    }
}
