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

  }
  public override void LogicUpdate(){
        base.LogicUpdate();
        if(isDead == true){
           //player.JumpState.DecreaseAmountofJumpLeft();
           //player.RB.bodyType = RigidbodyType2D.Static ;
           //player.RB.velocity = new Vector2(0,0);
           //player.GetComponent<SpriteRenderer>().enabled = false ;
            //player.obj.SetActive(false);

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

