using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
private int amountOfJumpsLeft ;
public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
    amountOfJumpsLeft = playerData.amountOfJumps ; 
    
}
public override void Enter(){
    base.Enter();
    player.inputhandler.UseJumpInput();
    player.SetVelocityY(playerData.jumpVelocity);
    isAbilityDone = true; 
    amountOfJumpsLeft--;
    player.InAirState.SetIsJumping();
    playerData.CurrentHealth -=20 ;
    player.healthbar.SetHealth(playerData.CurrentHealth);
   

    //Debug.Log(amountOfJumpsLeft);

}

public bool CanJump(){
    if(amountOfJumpsLeft>0){
        return true ;
    }
    else{
        return false ;
    }
}
public void resetAmountOfJumpsLeft(){
    amountOfJumpsLeft = playerData.amountOfJumps;

}
public void DecreaseAmountofJumpLeft(){
    amountOfJumpsLeft--;
}

}
