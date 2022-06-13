using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAbilityState
{
  private bool IsGrounded ;
  private int xinput ; 
  private bool JumpInput;
  private bool coyoteTime ;
  private bool isJumping; 
  private bool JumpInputStop;
public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
     
 }
public override void DoChecks(){
  base.DoChecks();
  IsGrounded = player.CheckIfGrounded();
}
public override void Enter(){
  base.Enter();
  player.JumpState.resetAmountOfJumpsLeft();
}
public override void Exit(){
  base.Exit();
}
public override void LogicUpdate(){
  base.LogicUpdate();
  CheckcoyoteTime();
  xinput = player.inputhandler.NormInputX;
  JumpInput = player.inputhandler.JumpInput;
  JumpInputStop = player.inputhandler.JumpInputStop;
  CheckJumpMultiplier();
 
  if(IsGrounded && player.CurrentVelocity.y < 0.01f){
    stateMachine.ChangeState(player.LandState);
  }
 // else if(JumpInput && player.JumpState.CanJump()){
  //  stateMachine.ChangeState(player.JumpState);
  //}
  else{
    player.CheckIfShouldFlip(xinput);
    player.SetVelocityX(playerData.movementVelocity*xinput);
    
    player.Anim.SetFloat("yVelocity",player.CurrentVelocity.y);
    player.Anim.SetFloat("xVelocity",Mathf.Abs(player.CurrentVelocity.x));

  }
      
}
private void CheckJumpMultiplier(){
 if(isJumping){
    if(JumpInputStop){
      player.SetVelocityY(player.CurrentVelocity.y *playerData.JumpHeightMultiplier);
      isJumping = false ;
    }
    else if(player.CurrentVelocity.y <=0f){
        isJumping = false ;
    }
  }
}
public override void PhysicsUpdate(){
  base.PhysicsUpdate();
}
private void CheckcoyoteTime(){
  if(coyoteTime && Time.time > startTime + playerData.coyoteTime){
    coyoteTime = false ;
    player.JumpState.DecreaseAmountofJumpLeft();
  }
}
public void StartcoyoteTime(){
  coyoteTime =true ;
}
public void SetIsJumping(){
  isJumping = true; 
}
}
