using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
   public bool IsWallSlide = false ;
   private bool isGrounded ;
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){

 }
  public override void Enter(){
        base.Enter();
        if(player.isOnPlatform){
        IsWallSlide = true ;
        }
    }
    public override void Exit(){
        base.Exit();
        IsWallSlide = false ;

    }
 public override void LogicUpdate(){
    base.LogicUpdate();
   isGrounded = player.CheckIfGrounded();
    if(!isExitingState){
       player.SetVelocityY(playerData.WallSlideVelocity);
        if(GrabInput && yinput ==0&& playerData.PlayerCurrentClimbStamina >30){
            stateMachine.ChangeState(player.wallGrabState);
    }
     if(isGrounded){
      Debug.Log("Enter");
      Exit();
    }
    }
   
 }
}
