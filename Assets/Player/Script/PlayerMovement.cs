using System;
using UnityEngine;

namespace Player.Script
{
    public class PlayerMovement : PlayerScript
    {
        public PlayerStats playerStats;
        public PlayerAnimator playerAnimator;
        
        //Input
        private Vector3 movementInput = Vector3.zero;
        private void MovementInput(Vector2 input) => movementInput = new Vector3(input.x, 0f, input.y).normalized;
        
        //Velocity
        private Vector3 velocity => moveVelocity + modifyerVelocity;
        private Vector3 moveVelocity;
        private Vector3 modifyerVelocity;
        
        
        private void OnEnable()
        {
            inputReader.playerInputMap.movementHoldEvent += MovementInput;
            //TODO: Add Dash.
            //inputReader.playerInputMap.sprintEvent += ;
        }

        private void OnDisable()
        {
            inputReader.playerInputMap.movementHoldEvent -= MovementInput;
        }

        private void Update()
        {
            TurnCheck();
            HandleAnimation();
            HandleMovement();
            ApplyMovement();
        }

        private void TurnCheck()
        {
            // Turn Left
            if (moveVelocity.x > 0 && movementInput.x < 0)
            {
                moveVelocity.x -= moveVelocity.x * playerStats.turnCompensation;
            }
            // Turn Right
            else if (moveVelocity.x < 0 && movementInput.x > 0)
            {
                moveVelocity.x -= moveVelocity.x * playerStats.turnCompensation;
            }

            if ((moveVelocity.z < 0 && movementInput.z > 0) || (moveVelocity.z < 0 && movementInput.z > 0))
            {
                moveVelocity.z -= moveVelocity.z * playerStats.turnCompensation;
            }
            
        }
        
        private void ApplyMovement() => rb.linearVelocity = velocity;
        
        private void HandleMovement()
        {
            float targetSpeed;
            float accelRate;

            if (movementInput.sqrMagnitude > 0f)
            {
                targetSpeed = playerStats.walkSpeed;
                accelRate = playerStats.acceleration;
            }
            else
            {
                targetSpeed = 0f;
                accelRate = playerStats.deceleration;
            }
            
            moveVelocity = Vector3.Lerp(
                moveVelocity, 
                movementInput * targetSpeed, 
                accelRate * Time.deltaTime);
        }

        private void HandleAnimation()
        {
            if (movementInput == Vector3.zero)
            {
                playerAnimator.SetAnimation("Idle");
            }
            else if (movementInput.x > 0)
            {
                playerAnimator.SetAnimation("WalkR");
                playerAnimator.spriteRenderer.flipX = false;
            }
            else if (movementInput.x < 0)
            {
                playerAnimator.SetAnimation("WalkL");
                playerAnimator.spriteRenderer.flipX = true;
            }
            else if (movementInput.z != 0)
            {
                if (!playerAnimator.spriteRenderer.flipX)
                {
                    playerAnimator.SetAnimation("WalkR");
                }
                else
                {
                    playerAnimator.SetAnimation("WalkL");
                }
            }
            
        }
    }
}