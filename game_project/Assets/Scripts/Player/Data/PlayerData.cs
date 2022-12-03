using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData",menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
 [Header("Player Stat")]
 public int MaxHealth = 100 ;
 public int CurrentHealth ;
 public float ClimbStaminaDrainRate  = 15 ;
 public float PlayerCurrentClimbStamina =100; 
 [Header("Move State")]
 public float movementVelocity = 5f ; 
 [Header("Jump State")]
 public float jumpVelocity = 15f ; 
 public int amountOfJumps = 1 ;

 [Header("InAir State")]
 public float coyoteTime  = 0.2f;
 public float JumpHeightMultiplier = 0.5f;
 [Header("Check Variable")]
 public float GroundCheckRadius = 0.3f;
 public float WallCheckDistance = 0.6f;
 public LayerMask whatisGround;
[Header("WallSlide State")]
public float WallSlideVelocity = -3f ;
[Header("WallClimb State")]
public float WallClimbVelocity = 3f ;
[Header("WallJump State")]
public float WallJumpVelocity = 10f;
public float wallJumpTime = 0.4f ; 
public Vector2 wallJumpAngle = new Vector2(1,2);
[Header("Dash State")]
public float dashCooldown = 0.5f ;
public float maxHoldTime = 1f ;
public float holdTimeScale = 0.25f ;
public float dashTime = 0.2f ;
public float dashVelocity = 30f ;
public float drag  =10f;
public float dashEndYmultiplier = 0.2f ; 
public float DistanceBetweenAfterImage =0.5f ;
[Header("LedgeClimb State")]
public Vector2 StartOffset ; 
public Vector2 StopOffset;
[Header("Crouch State")]
public float CrouchMovementVelocity = 5f ; 
public float CrouchColliderHeight  =0.12f;
public float standColliderHeight = 0.12f *2 ;

[Header("Player Acc/DeAcc")]
public float runMaxSpeed; //Target speed we want the player to reach.
public float runAcceleration; //The speed at which our player accelerates to max speed, can be set to runMaxSpeed for instant acceleration down to 0 for none at all
[HideInInspector] public float runAccelAmount; //The actual force (multiplied with speedDiff) applied to the player.
public float runDecceleration; //The speed at which our player decelerates from their current speed, can be set to runMaxSpeed for instant deceleration down to 0 for none at all
[HideInInspector] public float runDeccelAmount; //Actual force (multiplied with speedDiff) applied to the player .
[Space(5)]
[Range(0f, 1)] public float accelInAir; //Multipliers applied to acceleration rate when airborne.
[Range(0f, 1)] public float deccelInAir;
[Space(5)]
public bool doConserveMomentum = true;

[Header("Player Jump grav")]
public float jumpCutGravityMult; //Multiplier to increase gravity if the player releases thje jump button while still jumping
[Range(0f, 1)] public float jumpHangGravityMult; //Reduces gravity while close to the apex (desired max height) of the jump
public float jumpHangTimeThreshold; //Speeds (close to 0) where the player will experience extra "jump hang". The player's velocity.y is closest to 0 at the jump's apex (think of the gradient of a parabola or quadratic function)
[Space(0.5f)]
public float jumpHangAccelerationMult; 
public float jumpHangMaxSpeedMult; 		
}
