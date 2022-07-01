using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash{get; private set; }
    private float LastDashTime; 
    private bool isHolding ; 
    private Vector2 dashDirection;
    private Vector2 dashDirectionInput ;
    private bool dashInputStop ;
    private Vector2 lastAfterimagePos ;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void Enter(){
    base.Enter();
    CanDash = false ;
    player.inputhandler.UseDashInput();

    isHolding = true ;
    dashDirection = Vector2.right * player.FacingDirection; 

    Time.timeScale = playerData.holdTimeScale ; 
    startTime = Time.unscaledTime ;
    player.DashDirectionIndicator.gameObject.SetActive(true);
    }
    public override void Exit(){
    base.Exit();
    if(player.CurrentVelocity.y > 0 ){
        player.SetVelocityY(player.CurrentVelocity.y * playerData.dashEndYmultiplier);
    }
    }
    public override void LogicUpdate(){
    base.LogicUpdate();
    if(!isExitingState){
        player.Anim.SetFloat("yVelocity",player.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity",Mathf.Abs(player.CurrentVelocity.x));
            if(isHolding){
                dashDirectionInput = player.inputhandler.DashDirectionInput;
                dashInputStop = player.inputhandler.DashInputStop;

                if(dashDirectionInput !=Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right,dashDirection);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f,0f,angle - 45f);

                if(dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false ;
                    Time.timeScale = 1f; 
                    startTime = Time.time ;
                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = playerData.drag;
                    player.SetVelocity(playerData.dashVelocity,dashDirection);
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
                }
                else
                 {
                    player.SetVelocity(playerData.dashVelocity,dashDirection);
                    CheckIfShouldPlaceAfterImage();
                    if(Time.time >= startTime +playerData.dashTime)
                    {
                        player.RB.drag = 0f ;
                        isAbilityDone = true ;
                        LastDashTime = Time.time ;
                    }
            } 
        }
    }
    public bool CheckIfCanDash(){
        return CanDash && Time.time >= LastDashTime + playerData.dashCooldown;
    }
    public void ResetCanDash(){
        CanDash = true ;
    }
    private void PlaceAfterImage(){
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAfterimagePos = player.transform.position;
    }
    private void CheckIfShouldPlaceAfterImage(){
        if(Vector2.Distance(player.transform.position,lastAfterimagePos) >= playerData.DistanceBetweenAfterImage){
            PlaceAfterImage();
        }
    }

    
}
