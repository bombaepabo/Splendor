using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer4 : MonoBehaviour
{
   private Player player ; 
    [SerializeField] private Door door;
    private bool answerright = false ;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        
        string Choices = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetVariableState("NormanQuiz4")).value ; 
       
        switch(Choices)
        {
            case "":
            
                break ; 
            case "1" :
                answerright = true;
                break ;
            case "2" :
                //player.playerData.CurrentHealth -=100 ;
                Debug.Log("player got killed");
                Choices = "";
                break ; 
            case "3" :
                //player.playerData.CurrentHealth -=100 ;
                Debug.Log("player got killed");
                Choices = "";

                break ; 
            case "4" :
                //player.playerData.CurrentHealth -=100 ;
                Debug.Log("player got killed");
                Choices = "";

                break ; 
            default:
                Debug.LogWarning("Choices name not handled by switch statement: " + Choices);
                break;
        }
         if(!DialogueManager.GetInstance().dialogueIsPlaying && answerright){
            door.Open();
         }
        }
}
