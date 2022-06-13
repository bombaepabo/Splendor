using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine{get; private set;}
    public PlayerIdleState IdleState{get;private set;}
    public PlayerMoveState MoveState{get ; private set;}
    public PlayerJumpState JumpState{get;private set;}
    public PlayerInAirState InAirState{get;private set;}
    public PlayerLandState LandState{get;private set;}
    
    [SerializeField]
    private PlayerData playerData ; 
    #endregion

    #region Component
    public PlayerInputHandler inputhandler{get ;private set;}
    public Rigidbody2D RB {get; private set ;}
    public Animator Anim{get ;private set;}
    #endregion
    #region Check Transform
    [SerializeField]
    private Transform GroundCheck ; 
    #endregion
    #region OtherVariable
    public Vector2 CurrentVelocity {get;private set;}
    public int FacingDirection{get;private set ; }
    private Vector2 workspace;
    #endregion
   
    #region UnityCallBack Func
    private void Awake(){
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this,StateMachine,playerData,"idle");
        MoveState = new PlayerMoveState(this,StateMachine,playerData,"move");
        JumpState = new PlayerJumpState(this,StateMachine,playerData,"inAir");
        InAirState = new PlayerInAirState(this,StateMachine,playerData,"inAir");
        LandState = new PlayerLandState(this,StateMachine,playerData,"land");
    }
    private void Start(){
        //init State machine 
        Anim = GetComponent<Animator>();
        inputhandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
        FacingDirection = 1 ;
    }
    private void Update(){
        CurrentVelocity = RB.velocity; 
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate(){
       StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
    
    #region SetFunction
    public void SetVelocityX(float velocity){
        workspace.Set(velocity,CurrentVelocity.y);
        RB.velocity = workspace; 
        CurrentVelocity = workspace ;
    }
    public void SetVelocityY(float velocity){
        workspace.Set(CurrentVelocity.x,velocity);
        RB.velocity = workspace; 
        CurrentVelocity = workspace ;
    }
    #endregion
    
    #region CheckFunction
    public bool CheckIfGrounded(){
        return Physics2D.OverlapCircle(GroundCheck.position,playerData.GroundCheckRadius,playerData.whatisGround);
    }
    public void CheckIfShouldFlip(int xinput){
        if(xinput !=0 && xinput !=FacingDirection){
            flip();
        }
    }
    #endregion

    #region other Function
    private void AnimationTrigger(){
        StateMachine.CurrentState.AnimationTrigger();
    }
    private void AnimationFinishTrigger(){
        StateMachine.CurrentState.AnimationFinishTrigger();
    }
    private void flip(){
        FacingDirection *= -1 ;
        transform.Rotate(0.0f,180f,0.0f);
    }
    #endregion
}
