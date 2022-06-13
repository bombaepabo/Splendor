using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xinput ;
    private bool JumpInput;
    private bool isGrounded;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void DoChecks(){
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
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

        if(JumpInput && player.JumpState.CanJump()){
            player.inputhandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if(!isGrounded){
            player.InAirState.StartcoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
