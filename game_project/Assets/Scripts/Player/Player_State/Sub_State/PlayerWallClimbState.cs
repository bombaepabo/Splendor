using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{    
    private Vector2 holdPosition;
    private Vector2 stopPos ; 
    private Vector2 cornerPos ; 
    private Vector2 startPos ; 

    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
     
 }
  public override void LogicUpdate(){
        base.LogicUpdate();
        if(!isExitingState){
        player.SetVelocityY(playerData.WallClimbVelocity);
        playerData.PlayerCurrentClimbStamina -= playerData.ClimbStaminaDrainRate *Time.deltaTime ; 
       if(yinput != 1){
        stateMachine.ChangeState(player.wallGrabState);
       }
        else if(playerData.PlayerCurrentClimbStamina <= 30){
            stateMachine.ChangeState(player.wallSlideState);
        }
       else if(playerData.PlayerCurrentClimbStamina <= 0){
                Exit();
        }
       else if(player.CheckIfTouchingWall() &&!player.CheckIfTouchingLedge() && !player.CheckIfGrounded()){
              Debug.Log("Enter");
              cornerPos = player.DetermineCornerPosition() ;
              stopPos.Set(cornerPos.x + (player.FacingDirection * playerData.StopOffset.x),cornerPos.y +(playerData.StopOffset.y));
              startPos.Set(cornerPos.x -(player.FacingDirection *playerData.StartOffset.x),cornerPos.y -(playerData.StartOffset.y));

              player.transform.position = stopPos ; 
              //player.transform.position = Vector3.Lerp(player.transform.position,stopPos,playerData.WallClimbVelocity*Time.deltaTime);

                                
        }
        
        }
      
    }
}
