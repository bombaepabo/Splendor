using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xinput ;
    private bool JumpInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool GrabInput ; 
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void DoChecks(){
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
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
        xinput = player.inputhandler.NormInputX;
        JumpInput = player.inputhandler.JumpInput;
        GrabInput = player.inputhandler.GrabInput;
        if(JumpInput && player.JumpState.CanJump()){
            stateMachine.ChangeState(player.JumpState);
        }
        else if(!isGrounded){
            player.InAirState.StartcoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && GrabInput){
            stateMachine.ChangeState(player.wallGrabState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
