using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData",menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
 [Header("Player Stat")]
 public int MaxHealth = 100 ;
 public int CurrentHealth ;
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


}
