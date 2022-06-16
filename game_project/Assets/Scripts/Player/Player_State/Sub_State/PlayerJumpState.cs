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
    //Debug.Log(amountOfJumpsLeft);

}

public bool CanJump(){
    Debug.Log(amountOfJumpsLeft);
    if(amountOfJumpsLeft>0){
        return true ;
    }
    else{
        return false ;
    }
}
public void resetAmountOfJumpsLeft(){
    amountOfJumpsLeft = playerData.amountOfJumps;
    Debug.Log("true");


}
public void DecreaseAmountofJumpLeft(){
    amountOfJumpsLeft--;
}

}
