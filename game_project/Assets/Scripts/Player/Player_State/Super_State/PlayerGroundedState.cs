using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xinput ;
    private bool JumpInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void DoChecks(){
        base.DoChecks();
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
        JumpInput = player.inputhandler.JumpInput;
        if(JumpInput == true ){
            player.inputhandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
