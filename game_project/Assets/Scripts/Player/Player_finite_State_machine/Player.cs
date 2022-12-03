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
    public PlayerDeathState DeathState{get; private set;}
    
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
    public Vector3 SpawnPoint ; 
    public Vector3 SpawnPointTemp;

    #endregion
    #region OtherVariable
    public Vector2 CurrentVelocity {get;private set;}
    public int FacingDirection{get;private set ; }
    private Vector2 workspace;
    public HealthBar healthbar ;
    public GameObject obj ; 
    public float LastOnGroundTime { get; private set; }

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
        DeathState = new PlayerDeathState(this,StateMachine,playerData,"Dead");
    }
    private void Start(){
        //init State machine 
        SpawnPoint = transform.position;
        Anim = GetComponent<Animator>();
        inputhandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
        FacingDirection = 1 ;
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MoveMentCollider = GetComponent<BoxCollider2D>();
        playerData.CurrentHealth = playerData.MaxHealth ;
        obj = GameObject.Find("Player");
        obj.transform.position = SpawnPoint;

    }
    private void Update(){
        SpawnPoint = SpawnPointTemp;
        CurrentVelocity = RB.velocity; 
        LastOnGroundTime -= Time.deltaTime;

        if(DeathState.CheckIfisDead()){
            GameManager.PlayerDied();
            }
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
    }   
    public void run(float velocity){
        //Calculate the direction we want to move in and our desired velocity
		float targetSpeed = velocity;
        float lerpAmount = 1 ;
		//We can reduce are control using Lerp() this smooths changes to are direction and speed
		targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);
		float accelRate;

		#region Calculate AccelRate
		if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.runAccelAmount : playerData.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.runAccelAmount * playerData.accelInAir : playerData.runDeccelAmount * playerData.deccelInAir;
		#endregion

		#region Add Bonus Jump Apex Acceleration
		if ((JumpState.isitJumping() || wallJumpState.IsitWallJumping() || RB.velocity.y<0) && Mathf.Abs(RB.velocity.y) < playerData.jumpHangTimeThreshold)
		{
			accelRate *= playerData.jumpHangAccelerationMult;
			targetSpeed *= playerData.jumpHangMaxSpeedMult;
		}

		#endregion


		#region Conserve Momentum
		if(playerData.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
		{
			//Prevent any deceleration from happening, or in other words conserve are current momentum
			//You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
			accelRate = 0; 
		}

		#endregion

		//Calculate difference between current velocity and desired velocity
		float speedDif = targetSpeed - RB.velocity.x;
		//Calculate force along x-axis to apply to thr player

		float movement = speedDif * accelRate;
        Debug.Log("Rb velocity x  = "+RB.velocity.x+ " speed dif"+speedDif+ " accelRate"+accelRate+" RB velocity y"+RB.velocity.y);
		//Convert this to a vector and apply to rigidbody
        RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);

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
            SpawnPointTemp = transform.position ;
            Debug.Log(SpawnPointTemp);
        }
        else if(Collision.tag == "DashReset")
        {
            DashState.ResetCanDash();
            Destroy(Collision.gameObject);

        }
    }
    public IEnumerator respawn(float spawndelay){
        yield return new WaitForSeconds(spawndelay);
        playerData.CurrentHealth = 100 ; 
        DeathState.isDead = false ;
        obj.SetActive(true);
        obj.transform.position = SpawnPoint + new Vector3 (1f,0,0); 
        Debug.Log("ssss");
    
  }
    #endregion
}
