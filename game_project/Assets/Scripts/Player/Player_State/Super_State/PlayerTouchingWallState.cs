using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool IsGrounded ; 
    protected bool isTouchingWall;
    protected bool GrabInput ;
    protected int xinput ; 
    protected int yinput;
    protected bool jumpinput ;
    protected bool isTouchingLedge ;
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
     
 }

    public override void AnimationFinishTrigger(){
        base.AnimationFinishTrigger();
    }
    public override void AnimationTrigger(){
        base.AnimationTrigger();
    }
    public override void DoChecks(){
        base.DoChecks();
        IsGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge= player.CheckIfTouchingLedge();
       // if(isTouchingWall &&!isTouchingLedge){
         //   player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        //}
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
        yinput = player.inputhandler.NormInputY;
        GrabInput = player.inputhandler.GrabInput;
        jumpinput = player.inputhandler.JumpInput;

        if(jumpinput){
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }

        else if(IsGrounded && !GrabInput){
            stateMachine.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall || (xinput != player.FacingDirection && !GrabInput)){
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall &&! isTouchingLedge){
          //  stateMachine.ChangeState(player.LedgeClimbState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }




}
