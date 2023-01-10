using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public bool isDisabled = false;

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
        player.SetVelocityX(0);
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
    if(isDisabled){
        return ;
    }
    else{
    player.CheckIfShouldFlip(xinput);
    player.run(playerData.movementVelocity*xinput,1);
    }
        
        if(!isExitingState){
            if(!player.DeathState.CheckIfisDead()){
        if(xinput == 0 ){
            stateMachine.ChangeState(player.IdleState);
        }
        else if(yinput == -1 ){
            stateMachine.ChangeState(player.CrouchMoveState);
        }
        else if(player.DeathState.CheckIfisDead()){
        stateMachine.ChangeState(player.DeathState);
        }
            }
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
