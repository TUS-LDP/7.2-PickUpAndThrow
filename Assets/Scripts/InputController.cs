using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class InputController : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public bool destroyCude;

    public bool dropItem;
    public bool rotateItem;
    public bool throwItem;
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (cursorInputForLook)
        {
            LookInput(context.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpInput(context.ReadValueAsButton());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        SprintInput(context.ReadValueAsButton());
    }

    public void OnDestroyCude(InputAction.CallbackContext context)
    {
        Debug.Log("Destroy Cude");
        destroyCude = context.ReadValueAsButton();
    }

    public void OnDropItem(InputAction.CallbackContext context)
    {
        dropItem = context.ReadValueAsButton();
    }

    public void OnThrowItem(InputAction.CallbackContext context)
    {
        throwItem = context.ReadValueAsButton();
    }

    public void OnRotateItem(InputAction.CallbackContext context)
    {
        rotateItem = context.ReadValueAsButton();
        //value.isPressed;
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
