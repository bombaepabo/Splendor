using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
public class Player : MonoBehaviour,IDataPersistent
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
    public CapsuleCollider2D MoveMentCollider{get;private set;}
    public SpriteRenderer sprite{get; private set;}
    public ParticleSystem dust ; 
    public ParticleSystem deathEffect ;
    public Key FollowingKey ; 
    private EventInstance playerFootsteps ;
    [SerializeField] public SceneFader scenefader ;
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
    public Transform Keyfollowpoint ; 
    [SerializeField]
    public Transform ItemCollectorfollowpoint ; 
    [SerializeField]
    public Vector3 SpawnPoint ; 
    public Vector3 SpawnPointTemp;

    #endregion
    #region OtherVariable
    public Vector2 CurrentVelocity {get;private set;}
    public int FacingDirection{get;private set ; }
    private Vector2 workspace;
    public float LastOnGroundTime { get; private set; }
    public bool isOnPlatform ;
    public Color flashColor = new Color(1, 1, 1, 0.5f);
    public float flashDuration = 0.5f;
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
       // LedgeClimbState = new PlayerLedgeClimbState(this,StateMachine,playerData,"ledgeClimbState");
        DashState = new PlayerDashState(this,StateMachine,playerData,"inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this,StateMachine,playerData,"crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this,StateMachine,playerData,"crouchMove");
        DeathState = new PlayerDeathState(this,StateMachine,playerData,"Dead");
    }
    private void Start(){
        //init State machine 
        sprite = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        inputhandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
        FacingDirection = 1 ;
        MoveMentCollider = GetComponent<CapsuleCollider2D>();
        playerFootsteps = AudioManager.instance.CreateInstance(FModEvent.instance.playerFootsteps);
    }
    private void Update(){
        SpawnPoint = SpawnPointTemp;
        CurrentVelocity = RB.velocity; 
        LastOnGroundTime -= Time.deltaTime;
        UpdateSound();
        bool isGrounded = CheckIfGrounded();


        if(playerData.PlayerCurrentClimbStamina <= 30){
                FlashStamina();
            
        }
        else{
            StopAllCoroutines();
            sprite.color =new Color(1,1,1,1f);
        }

        if(playerData.CurrentHealth <=0){
            StateMachine.ChangeState(DeathState);
            }
         if(inputhandler.ExitInput == true && inputhandler.ExitInputStop == false ){
            PauseMenu.IsPaused = true ;
            DisableMovement();

            //inputhandler.DisableInput();
            }
         else if(PauseMenu.IsPaused == false){
            //inputhandler.EnableInput();
         }
         else if(RB.velocity.y <0){
            RB.velocity = new Vector2(RB.velocity.x,Mathf.Max(RB.velocity.y,-50));
         }          
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate(){
        if(DialogueManager.GetInstance().dialogueIsPlaying){
            StateMachine.ChangeState(IdleState);
            DisableMovement();

        }
        else{
            EnableMovement();
        }
        UpdateSound();
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
        LastOnGroundTime = 0 ; 
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
    public void run(float velocity,float lerpAmount){
        //Calculate the direction we want to move in and our desired velocity
		float targetSpeed = velocity;
		//We can reduce are control using Lerp() this smooths changes to are direction and speed
		targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);
		float accelRate;

		#region Calculate AccelRate
		if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.runAccelAmount : playerData.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.runAccelAmount * playerData.accelInAir : playerData.runDeccelAmount * playerData.deccelInAir;
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
		//Convert this to a vector and apply to rigidbody
        RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
    }
    public void Jump(float velocityY){
        Debug.Log(velocityY);
        RB.AddForce(new Vector2(CurrentVelocity.x, velocityY), ForceMode2D.Impulse);

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
        if(xinput !=0 && xinput !=FacingDirection&&!IdleState.isDisabled){
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
        workspace.Set((xdistance)*FacingDirection,0f);
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
        CreateDust();
    }
    private void OnCollisionEnter2D(Collision2D Collision){
        if(Collision.gameObject.tag =="Damagable"){
            playerData.CurrentHealth -= 100 ;
            GameEventsManager.instance.PlayerDeath();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Respawn"){
            //get the extents
            var yHalfExtents = collider.bounds.extents.y;
            //get the center
            var yCenter = collider.bounds.center.y;
            //get the up border
            float yUpper = transform.position.y + (yCenter + yHalfExtents);
            //get the lower border
            float yLower = transform.position.y + (yCenter - yHalfExtents);
            var Ypos = (yLower/2)+0.1f ;
            SpawnPointTemp = new Vector3(transform.position.x,Ypos)  ;

            //Debug.Log("Save Respawn in position"+SpawnPointTemp);

        }
    }
  public void HandleRespawn(float spawndelay){
    MoveMentCollider.enabled = false;
    GetComponent<SpriteRenderer>().enabled = false ;
    Invoke("respawn",spawndelay);
  }
  public void respawn(){
        playerData.CurrentHealth = 100 ; 
        DeathState.isDead = false ;
        MoveMentCollider.enabled = true;
        GetComponent<SpriteRenderer>().enabled = true ;
        transform.position = SpawnPointTemp ;
        StateMachine.ChangeState(IdleState);
        scenefader.FadeSceneIn();
        EnableMovement();

  }
  public void LoadData(GameData data){
        this.transform.position = data.playerPosition ;
  }
  public void SaveData(GameData data){
        data.playerPosition = SpawnPoint; 
  }
  private void UpdateSound(){
    if(RB.velocity.x !=0 && CheckIfGrounded()){
    PLAYBACK_STATE playbackState ; 
    playerFootsteps.getPlaybackState(out playbackState);
    if(playbackState.Equals(PLAYBACK_STATE.STOPPED)){
        playerFootsteps.start();
    }
    }
    else{
        playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
    }
  }
  public void CreateDust(){
    dust.Play();
  }
  public void DeathEffect(){
    deathEffect.Play();
  }
  public void DisableMovement(){
            MoveState.isDisabled = true ;
            JumpState.isDisabled = true ; 
            IdleState.isDisabled = true ; 
            DashState.isDisabled = true ; 
  }
   public void EnableMovement(){
            MoveState.isDisabled = false ;
            JumpState.isDisabled = false ; 
            IdleState.isDisabled = false ; 
            DashState.isDisabled = false ; 
  }
  public void FlashStamina(){
    StopAllCoroutines();
    StartCoroutine(Flash());

  }
  private IEnumerator Flash()
    {
        float elapsedTime = 0f;
        Color originalColor = sprite.color;

        while (elapsedTime < flashDuration)
        {
            sprite.color = flashColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        sprite.color = originalColor;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Flash());
    #endregion
}
}
