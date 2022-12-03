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
    
    
}
