using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xinput ;
    protected int yinput ; 
    private bool JumpInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool GrabInput ; 
    private bool isTouchingLedge;
    private bool DashInput ;
    protected bool isTouchingCeiling;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void DoChecks(){
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge();
        isTouchingCeiling = player.CheckForCeiling();
    }
    public override void Enter(){
        base.Enter();
        player.SetVelocityX(0);
        player.JumpState.resetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
        playerData.PlayerCurrentClimbStamina =100 ; 

    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        xinput = player.inputhandler.NormInputX;
        yinput = player.inputhandler.NormInputY;
        JumpInput = player.inputhandler.JumpInput;
        GrabInput = player.inputhandler.GrabInput;
        DashInput = player.inputhandler.DashInput;
        if(JumpInput && player.JumpState.CanJump()&&!isTouchingCeiling&&!player.IdleState.isDisabled){
            stateMachine.ChangeState(player.JumpState);
        }
        else if(!isGrounded){
            player.InAirState.StartcoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && GrabInput&&isTouchingLedge){
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if(DashInput && player.DashState.CheckIfCanDash()&&!isTouchingCeiling&&!player.DashState.isDisabled){
        stateMachine.ChangeState(player.DashState);}
  
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
