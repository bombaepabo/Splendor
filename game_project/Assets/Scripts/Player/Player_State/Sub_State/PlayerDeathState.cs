using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerAbilityState
{
    public bool isDead ; 
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
{    
        playerData.CurrentHealth = 100 ; 

}
  public override void Enter(){
    isDead = true;
    Debug.Log("Death State");
  }
  public override void LogicUpdate(){
        base.LogicUpdate();
        if(isDead == true){
            player.RB.bodyType = RigidbodyType2D.Static ;
            player.obj.GetComponent<SpriteRenderer>().enabled = false ;

        }
        stateMachine.ChangeState(player.IdleState);

        }
  public bool CheckIfisDead(){
    if(playerData.CurrentHealth <=0){
            isDead = true ;
            return true ;    
        }
    else{
        isDead = false ;
        return false ;
    }
  }
    }

