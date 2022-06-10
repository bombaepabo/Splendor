using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAbilityState
{
  private bool IsGrounded ;
  private int xinput ; 
public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
     
 }
public override void DoChecks(){
  base.DoChecks();
  IsGrounded = player.CheckIfGrounded();
}
public override void Enter(){
  base.Enter();
}
public override void Exit(){
  base.Exit();
}
public override void LogicUpdate(){
  base.LogicUpdate();
  xinput = player.inputhandler.NormInputX;
  if(IsGrounded && player.CurrentVelocity.y < 0.01f){
    stateMachine.ChangeState(player.LandState);
  }
  else{
    player.CheckIfShouldFlip(xinput);
    player.SetVelocityX(playerData.movementVelocity*xinput);
    
    player.Anim.SetFloat("yVelocity",player.CurrentVelocity.y);
    player.Anim.SetFloat("xVelocity",Mathf.Abs(player.CurrentVelocity.x));

  }
      
}
public override void PhysicsUpdate(){
  base.PhysicsUpdate();
}
}
