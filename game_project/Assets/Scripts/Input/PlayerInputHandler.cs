using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 movementinput ; 
    public void OnMoveInput(InputAction.CallbackContext context){
        movementinput = context.ReadValue<Vector2>();
        Debug.Log(movementinput);

    }
    public void OnJumpInput(InputAction.CallbackContext context){
        if(context.started){
            Debug.Log("Jump button push down now");

        }
        if(context.performed){
            Debug.Log("Jump is being held down");

        }
        if(context.canceled){
            Debug.Log("Jump button has been released");
        }

    }
}
