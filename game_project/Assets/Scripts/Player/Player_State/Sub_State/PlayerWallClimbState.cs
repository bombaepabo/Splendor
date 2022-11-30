using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
     
 }
  public override void LogicUpdate(){
        base.LogicUpdate();
        if(!isExitingState){
        player.SetVelocityY(playerData.WallClimbVelocity);
        playerData.PlayerCurrentClimbStamina -= 15 *Time.deltaTime ; 
        Debug.Log("wall climb state");
        Debug.Log("stamina" + playerData.PlayerCurrentClimbStamina);

       if(yinput != 1){
        stateMachine.ChangeState(player.wallGrabState);
       }
       else if(playerData.PlayerCurrentClimbStamina <= 0){
                Exit();
        }
        }
      
    }
}
