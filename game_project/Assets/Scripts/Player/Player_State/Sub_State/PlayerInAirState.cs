using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAbilityState
{
  private int xinput ; 
  private bool JumpInput;
  private bool GrabInput ; 
  private bool JumpInputStop;
  private bool IsGrounded ;
  private bool coyoteTime ;
  private bool isTouchingWallBack;
  private bool isJumping; 
  private bool isTouchingWall ; 
  private bool wallJumpCoyoteTime ;
  private float startWallJumpCoyoteTime ;
  private bool oldIsTouchingWall ; 
  private bool oldIsTouchingWallback; 
  private bool isTouchingLedge ;
  private bool DashInput;
public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
     
 }
public override void DoChecks(){
  base.DoChecks();
  oldIsTouchingWall = isTouchingWall;
  oldIsTouchingWallback = isTouchingWallBack;
  IsGrounded = player.CheckIfGrounded();
  isTouchingWall = player.CheckIfTouchingWall();
  isTouchingWallBack = player.CheckIfTouchingWallback();
  isTouchingLedge = player.CheckIfTouchingLedge();
  if(isTouchingWall && !isTouchingLedge){
    player.LedgeClimbState.SetDetectedPosition(player.transform.position);
  }
  if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallback)){
    StartWallJumpCoyoteTime();
    
  }
}
public override void Enter(){
  base.Enter();
}
public override void Exit(){
  base.Exit();
  oldIsTouchingWall = false;
  oldIsTouchingWallback= false;
  isTouchingWall =false; 
  isTouchingWallBack = false ;
}
public override void LogicUpdate(){
  base.LogicUpdate();
  CheckcoyoteTime();
  CheckWallJumpCoyoteTime();
  xinput = player.inputhandler.NormInputX;
  JumpInput = player.inputhandler.JumpInput;
  JumpInputStop = player.inputhandler.JumpInputStop;
  GrabInput = player.inputhandler.GrabInput;
  DashInput = player.inputhandler.DashInput;
  CheckJumpMultiplier();
  if(IsGrounded && player.CurrentVelocity.y < 0.01f){
    stateMachine.ChangeState(player.LandState);
  }
  //else if(isTouchingWall &&!isTouchingLedge && !IsGrounded){
  //  stateMachine.ChangeState(player.LedgeClimbState);
 // }
  else if(JumpInput&&(isTouchingWall ||isTouchingWallBack || wallJumpCoyoteTime))
  {
    StopWallJumpCoyoteTime();
    isTouchingWall = player.CheckIfTouchingWall();
    player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
    stateMachine.ChangeState(player.wallJumpState);
  }
  else if(JumpInput && player.JumpState.CanJump()){
    player.inputhandler.UseJumpInput();
    stateMachine.ChangeState(player.JumpState);
  }
  else if(isTouchingWall && GrabInput&& isTouchingLedge){
    stateMachine.ChangeState(player.wallGrabState);

    
  }
  else if(isTouchingWall && xinput == player.FacingDirection&&player.CurrentVelocity.y <=0){
    stateMachine.ChangeState(player.wallSlideState);
  }
  else if(DashInput && player.DashState.CheckIfCanDash()){
    stateMachine.ChangeState(player.DashState);
  }
  else if(player.DeathState.CheckIfisDead()){
    stateMachine.ChangeState(player.DeathState);
  }
  
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
private void CheckWallJumpCoyoteTime(){
  if(wallJumpCoyoteTime == true && Time.time == startWallJumpCoyoteTime +playerData.coyoteTime){

  }
}
public void StartcoyoteTime(){
  coyoteTime =true ;
}
public void SetIsJumping(){
  isJumping = true; 
}
public void StartWallJumpCoyoteTime(){
  wallJumpCoyoteTime =  true ;
  startWallJumpCoyoteTime = Time.time ;
}
public void StopWallJumpCoyoteTime(){
  wallJumpCoyoteTime =  false ;
}
}
