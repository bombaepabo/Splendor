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
    public PlayerWallClimbState wallClimbState{get;private set;}
    public PlayerWallGrabState wallGrabState{get;private set;}
    public PlayerWallSlideState wallSlideState{get;private set;}
    public PlayerWallJumpState wallJumpState{get;private set;}
    public PlayerLedgeClimbState LedgeClimbState{get;private set;}
    public PlayerDashState DashState{get;private set;}
    public PlayerCrouchIdleState CrouchIdleState{get;private set;}
    public PlayerCrouchMoveState CrouchMoveState{get;private set;}
    
    [SerializeField]
    public PlayerData playerData ; 
    #endregion

    #region Component
    public PlayerInputHandler inputhandler{get ;private set;}
    public Rigidbody2D RB {get; private set ;}
    public Animator Anim{get ;private set;}
    public Transform DashDirectionIndicator {get;private set;}
    public BoxCollider2D MoveMentCollider{get;private set;}
    #endregion
    #region Check Transform
    [SerializeField]
    private Transform GroundCheck ; 
    [SerializeField]
    private Transform WallCheck ; 
    [SerializeField]
    private Transform LedgeCheck;
    [SerializeField]
    private Transform CeilingCheck;
    [SerializeField]
    private Vector3 SpawnPoint ; 

    #endregion
    #region OtherVariable
    public Vector2 CurrentVelocity {get;private set;}
    public int FacingDirection{get;private set ; }
    private Vector2 workspace;
    public HealthBar healthbar ;
    private GameObject obj ; 
    private float velPower ;
   
    #endregion
   
    #region UnityCallBack Func
    private void Awake(){
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this,StateMachine,playerData,"idle");
        MoveState = new PlayerMoveState(this,StateMachine,playerData,"move");
        JumpState = new PlayerJumpState(this,StateMachine,playerData,"inAir");
        InAirState = new PlayerInAirState(this,StateMachine,playerData,"inAir");
        LandState = new PlayerLandState(this,StateMachine,playerData,"land");
        wallSlideState = new PlayerWallSlideState(this,StateMachine,playerData,"wallSlide");
        wallClimbState = new PlayerWallClimbState(this,StateMachine,playerData,"wallClimb");
        wallGrabState  = new PlayerWallGrabState(this,StateMachine,playerData,"wallGrab");
        wallJumpState = new PlayerWallJumpState(this,StateMachine,playerData,"inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this,StateMachine,playerData,"ledgeClimbState");
        DashState = new PlayerDashState(this,StateMachine,playerData,"inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this,StateMachine,playerData,"crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this,StateMachine,playerData,"crouchMove");
    }
    private void Start(){
        //init State machine 
        Anim = GetComponent<Animator>();
        inputhandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
        FacingDirection = 1 ;
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MoveMentCollider = GetComponent<BoxCollider2D>();
        playerData.CurrentHealth = playerData.MaxHealth ;
        obj = GameObject.Find("Player");
        SpawnPoint = transform.position;
    }
    private void Update(){
        CurrentVelocity = RB.velocity; 
        if(playerData.CurrentHealth <=0){
            obj.transform.position = SpawnPoint + new Vector3 (1f,0,0); 

            Debug.Log(obj.transform);

        }
        playerData.CurrentHealth = 100 ; 
        StateMachine.CurrentState.LogicUpdate();
        
    }
    private void FixedUpdate(){
       
       StateMachine.CurrentState.PhysicsUpdate();
        
    }
    #endregion
    
    #region SetFunction
    public void SetVelocityZero(){
        RB.velocity = Vector2.zero ; 
        CurrentVelocity = Vector2.zero ; 
        
    }
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
    public void SetVelocity(float velocity , Vector2 angle ,int direction){
        angle.Normalize();
        workspace.Set(angle.x * velocity *direction,angle.y *velocity);
        RB.velocity = workspace ; 
        CurrentVelocity = workspace ;
    }
    public void SetVelocity(float velocity,Vector2 direction){
        workspace = direction * velocity ;
        RB.velocity = workspace ;
        CurrentVelocity = workspace  ;
        Debug.Log(direction);
    }   
    #endregion
    
    #region CheckFunction
    public bool CheckForCeiling(){
        return Physics2D.OverlapCircle(CeilingCheck.position,playerData.GroundCheckRadius,playerData.whatisGround);
    }
    public bool CheckIfGrounded(){
        return Physics2D.OverlapCircle(GroundCheck.position,playerData.GroundCheckRadius,playerData.whatisGround);
    }
    public void CheckIfShouldFlip(int xinput){
        if(xinput !=0 && xinput !=FacingDirection){
            flip();
        }
    }
    public bool CheckIfTouchingWall(){
        return Physics2D.Raycast(WallCheck.position,Vector2.right*FacingDirection,playerData.WallCheckDistance,playerData.whatisGround);
    }
    public bool CheckIfTouchingWallback(){
        return Physics2D.Raycast(WallCheck.position,Vector2.right*-FacingDirection,playerData.WallCheckDistance,playerData.whatisGround);
    }
    public bool CheckIfTouchingLedge(){
        return Physics2D.Raycast(LedgeCheck.position,Vector2.right*FacingDirection,playerData.WallCheckDistance,playerData.whatisGround);
    }
   

    #endregion

    #region other Function
    public void SetColliderHeight(float height){
        Vector2 center = MoveMentCollider.offset;
        workspace.Set(MoveMentCollider.size.x,height);
        center.y +=(height -MoveMentCollider.size.y) /2 ; 
        MoveMentCollider.size = workspace ;
        MoveMentCollider.offset = center ;
    }
    public Vector2 DetermineCornerPosition(){
        RaycastHit2D xhit =Physics2D.Raycast(WallCheck.position,Vector2.right*FacingDirection,playerData.WallCheckDistance,playerData.whatisGround);
        float xdistance = xhit.distance ;
        workspace.Set((xdistance +0.015f)*FacingDirection,0f);
        RaycastHit2D yhit = Physics2D.Raycast((LedgeCheck.position+(Vector3)(workspace)),Vector2.down,LedgeCheck.position.y - WallCheck.position.y +0.015f,playerData.whatisGround);
        float ydistance = yhit.distance ;
        workspace.Set(WallCheck.position.x + (xdistance *FacingDirection),LedgeCheck.position.y - ydistance);
        return workspace ; 
    }
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
    private void OnCollisionEnter2D(Collision2D Collision){
        if(Collision.gameObject.tag =="Damagable"){
            playerData.CurrentHealth -= 100 ;
            healthbar.SetHealth(playerData.CurrentHealth);

        }
    }
    private void OnTriggerEnter2D(Collider2D Collision){
        if(Collision.tag == "Respawn"){
            SpawnPoint = transform.position ;
        }
    }
    
    #endregion
}
