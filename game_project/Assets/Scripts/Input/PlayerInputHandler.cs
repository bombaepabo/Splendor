using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera Cam ;
    public Vector2 RawMovementInput{get ;private set ;}
    public Vector2 RawDashDirectionInput{get;private set;}
    public Vector2Int DashDirectionInput{get;private set; }
    public int NormInputX {get;private set; }
    public int NormInputY {get;private set ;}
    public bool JumpInput{get; private set;}
    public bool JumpInputStop{get;private set ;}
    public bool DashInput{get;private set;}
    public bool DashInputStop{get;private set;}
    public bool ExitInput{get;private set;}
    public bool ExitInputStop{get;private set;}
    public bool PickItemInput{get;private set;}
    public bool PickItemInputStop{get;private set;}
    [SerializeField]
    private float inputHoldTime = 0.2f ; 
    private float JumpInputStartTime ;
    public bool GrabInput{get;private set ;}
    private float dashInputStartTime ; 
    private void Start(){
        playerInput = GetComponent<PlayerInput>();
        Cam = Camera.main; 
    }
    private void Update(){
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context){
        RawMovementInput = context.ReadValue<Vector2>();
        if(Mathf.Abs(RawMovementInput.x)>0.5f){
            NormInputX = (int)(RawMovementInput *Vector2.right).normalized.x ;
        }
        else{
            NormInputX = 0 ;
        }
        if(Mathf.Abs(RawMovementInput.y)>0.5f){
            NormInputY = (int)(RawMovementInput *Vector2.up).normalized.y;
        }
        else{
            NormInputY = 0 ;
        }

    }
    public void OnJumpInput(InputAction.CallbackContext context){
        if(context.started){
            JumpInput = true ;
            JumpInputStop = false ;
            JumpInputStartTime = Time.time ;
        }
        if(context.canceled){
            JumpInputStop = true ;
        }
    }
    public void OnGrabInput(InputAction.CallbackContext context){
        if(context.started){
            GrabInput = true ; 
            
        }
        if(context.canceled){
            GrabInput = false ;
        }

    }
    public void OnDashDirectionInput(InputAction.CallbackContext context){
        RawDashDirectionInput = context.ReadValue<Vector2>();
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);


    }
    public void OnDashInput(InputAction.CallbackContext context){
        if(context.started){
            DashInput =true ; 
            DashInputStop = false ;
            dashInputStartTime = Time.time ;

        }
        else if(context.canceled){
            DashInputStop = true ; 
        }
    }
    public void OnExitInput(InputAction.CallbackContext context){
          if(context.performed){
            ExitInput = true ;
            ExitInputStop = false ;
        }
        else if(context.canceled){
            ExitInput = false  ;
            ExitInputStop = true ; 
            
        }
    }
    public void OnPickItemInput(InputAction.CallbackContext context){
        if(context.performed){
            PickItemInput = true ;
            PickItemInputStop = false ; 
        }
        else if(context.canceled){
            PickItemInput = false ;
            PickItemInputStop = true ; 
        }
    }
    public bool GetPickItemPressed(){
        bool result = PickItemInput ; 
        PickItemInput = false ;
        return  result ; 
    }
    public bool GetDashInput(){
        bool result = DashInput ; 
        DashInput = false ;
        return  result ; 
    }
    public bool GetSubmitPressed(){
        bool result = JumpInput ; 
        JumpInput = false ;
        return result ; 
    }
    public void registerSubmitPressed(){
        JumpInput = false ;
    }
    public void registerpcikitemPressed(){
        PickItemInput = false ;
    }
    public string GetControlType(){
        return playerInput.currentControlScheme ; 
    }
    public bool GetExitPressed(){
        bool result = ExitInput ; 
        ExitInput = false ;
        return  result ; 
    }
    public void UseJumpInput(){
        JumpInput = false;
    }
    public void UseDashInput(){
        DashInput = false ;
    }
    private void CheckDashInputHoldTime(){
        if(Time.time >= dashInputStartTime+inputHoldTime){
            DashInput = false ;
        }
    }
    private void CheckJumpInputHoldTime(){
        if(Time.time >= JumpInputStartTime + inputHoldTime){
            JumpInput = false ;
        }
    }
    public void SwitchActionMap(string actionname){
        playerInput.SwitchCurrentActionMap(actionname);
    }
    public void DisableInput(){
       playerInput.actions.FindActionMap("Gameplay").Disable();
       playerInput.actions.FindActionMap("UI").Enable();

    }
    public void EnableInput(){
        playerInput.actions.FindActionMap("UI").Disable();
        playerInput.actions.FindActionMap("Gameplay").Enable();

    }
    

    
    
}
