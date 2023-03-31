using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerAbilityState
{
    public bool isDead ;
    public float currentTime = 0f; 
    public float DeathTime = 3.5f; 
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
{    
        playerData.CurrentHealth = 100 ; 

}
  public override void Enter(){
    player.isrespawn = false ;
    isDead = true;
    player.DisableMovement();
    player.MoveMentCollider.enabled = false;
    player.GetComponent<SpriteRenderer>().enabled = false ;
    player.scenefader.FadeSceneOut();
  }
  public override void LogicUpdate(){
        base.LogicUpdate();
        currentTime += Time.fixedDeltaTime;       
        if(currentTime >= DeathTime){
          player.respawn();
          currentTime = 0;
        }


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

