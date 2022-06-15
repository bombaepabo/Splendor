using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
     
 } 
 public override void AnimationFinishTrigger(){
        base.AnimationFinishTrigger();
    }
    public override void AnimationTrigger(){
        base.AnimationTrigger();
    }
    public override void DoChecks(){
        base.DoChecks();
    }
    public override void Enter(){
        base.Enter();
        holdPosition = player.transform.position;
        HoldPosition();
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        
        HoldPosition();
        if(!isExitingState){
         if(yinput > 0){
            stateMachine.ChangeState(player.wallClimbState);
        }
        else if(yinput <0 ||!GrabInput){
            stateMachine.ChangeState(player.wallSlideState);
        }
        }
    }
    private void HoldPosition(){
        player.transform.position = holdPosition;
        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
}
