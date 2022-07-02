using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        player.CheckIfShouldFlip(xinput);
        player.SetVelocityX(playerData.movementVelocity*xinput);
        
        if(!isExitingState){
        if(xinput == 0 ){
            stateMachine.ChangeState(player.IdleState);
        }
        else if(yinput == -1 ){
            stateMachine.ChangeState(player.CrouchMoveState);
        }

        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
