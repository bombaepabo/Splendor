using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public bool isDisabled = false;

   public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void DoChecks(){
        base.DoChecks();
    }
    public override void Enter(){
        base.Enter();
        player.SetVelocityX(0f);

    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(!isExitingState)
        {
        if(xinput!=0 && !isDisabled){
            stateMachine.ChangeState(player.MoveState);
        }
        else if(yinput == -1 &&!isDisabled){
            stateMachine.ChangeState(player.CrouchIdleState);
        }

        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
