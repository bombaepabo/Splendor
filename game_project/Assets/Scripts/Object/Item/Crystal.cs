using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{  private Player player ;
    [SerializeField] private GameObject visualCue ; 
    public bool isFinished = false ;
    private bool playerInRange ; 
    private Animator _anim ; 
    public static bool isOpen = false ;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
         playerInRange = false ;
        visualCue.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
         if(playerInRange && !isFinished){
                visualCue.SetActive(true);
                if(player.inputhandler.GetPickItemPressed()){
                    Open();
                    isOpen =true ; 
                    // open a cage 
         //isFinished = true ;
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
     public void Open(){
    }
}
