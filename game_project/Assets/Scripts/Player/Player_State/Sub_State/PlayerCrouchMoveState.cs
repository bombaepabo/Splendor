using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
     public override void Enter(){
        base.Enter();
        player.SetColliderHeight(playerData.CrouchColliderHeight);

    }
    public override void Exit(){
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }
    public override void LogicUpdate(){
        base.LogicUpdate();

        if(!isExitingState){

            player.SetVelocityX(playerData.CrouchMovementVelocity*player.FacingDirection);
            player.CheckIfShouldFlip(xinput);
            if(xinput == 0){
                stateMachine.ChangeState(player.CrouchIdleState);

            }
            else if(yinput != -1 && !isTouchingCeiling){
                stateMachine.ChangeState(player.MoveState);
            }
             else if(player.DeathState.CheckIfisDead()){
                stateMachine.ChangeState(player.DeathState);
            }
        }
    }
}
