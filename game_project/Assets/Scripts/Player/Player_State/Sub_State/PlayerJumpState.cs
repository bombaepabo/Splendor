using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
private int amountOfJumpsLeft ;
public bool isJumping ; 
public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){
    amountOfJumpsLeft = playerData.amountOfJumps ; 
    
}
public override void Enter(){
    base.Enter();
    isJumping = true;
    player.inputhandler.UseJumpInput();
    if(!player.DeathState.CheckIfisDead()){
    //player.Jump(playerData.jumpVelocity);
    player.SetVelocityY(playerData.jumpVelocity);

    }
    isAbilityDone = true; 
    amountOfJumpsLeft--;
    player.InAirState.SetIsJumping();
   


}
public override void Exit(){
    base.Exit();
    isJumping = false ;
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
public bool isitJumping(){
    return isJumping;
}

}
