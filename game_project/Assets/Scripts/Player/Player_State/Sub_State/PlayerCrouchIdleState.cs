using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
     public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void Enter(){
        base.Enter();
        player.SetVelocityZero();
        player.SetColliderHeight(playerData.CrouchColliderHeight);

    }
    public override void Exit(){
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(!isExitingState){
            if(xinput != 0 ){
                stateMachine.ChangeState(player.CrouchMoveState);

            }
            else if(yinput != -1 &&!isTouchingCeiling){
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
