using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection ;
    private bool IsWallJumping ; 
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void Enter(){
        base.Enter();
        IsWallJumping = true ; 
        player.inputhandler.UseJumpInput();
        player.SetVelocity(playerData.WallJumpVelocity,playerData.wallJumpAngle,wallJumpDirection);
        player.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountofJumpLeft();
    }
    public override void Exit(){
    base.Exit();
    IsWallJumping = false ;
}
    public override void LogicUpdate(){
        base.LogicUpdate();
        player.Anim.SetFloat("yVelocity",player.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity",Mathf.Abs(player.CurrentVelocity.x));
        if(Time.time >= startTime + playerData.wallJumpTime){
            isAbilityDone = true;
        }
    }
    public void DetermineWallJumpDirection(bool isTouchingWall){
        if(isTouchingWall){
            wallJumpDirection = -player.FacingDirection;
        }
        else{
            wallJumpDirection = player.FacingDirection;
        }
    }
    public bool IsitWallJumping(){
        return IsWallJumping ;
    }
}
