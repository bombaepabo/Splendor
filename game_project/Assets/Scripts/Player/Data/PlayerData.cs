using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData",menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
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
 public LayerMask whatisGround;

}
