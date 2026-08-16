using System;
using Unity.VisualScripting;
using UnityEngine;
using Scripts.PlayerUI;

namespace Scripts.Character
{
    public abstract class CharacterMovement : MonoBehaviour
    {

        [Header("Movement Modifiers")]
        public float MoveSpeed = 5f;
        public float GroundDrag = 5f;
        public float JumpHeight = 0.5f;
        public float JumpCooldown = 0.5f;
        public float AirMultiplier = 1f;
        public float WalkingBackwardsSpeedMuliplier = 0.5f;
        public float CrouchingSpeedMuliplier = 0.5f;
        public float StrafeMultiplier = 0.5f;
        public float Gravity = -9.81f;
        public float SprintSpeedMultiplier = 1.5f;
        public int JumpStaminaCost = 25;
        public int SprintStaminaCost = 5;

        [Header("Current State")]
        protected bool CanJump;
        protected bool IsWalking;
        protected bool IsWalkingBackwards;
        protected bool IsStrafing;
        protected bool IsSprinting;
        protected bool IsJumping;
        protected bool IsCrouching;
        protected bool CanSprintDecrementStamina;
        protected float HorizontalInputRaw;
        protected float VerticalInputRaw;
        private Vector3 CurrentSpeed;
        private Vector3 moveDirection;
        //Used for vertical velocity and gravity
        private Vector3 velocity;    
        
        [Header("Ground Check")]
        public float PlayerHeight;
        public LayerMask IsGround;
        public bool IsGrounded;

        [Header("Character")]
        public Transform Character;
        public CharacterController CharacterController;
        //Helps control animations
        public CharacterMovementState CharacterMovementState;
        public BaseStaminaManager BaseStaminaManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Start()
        {
            CanJump = true;
            CanSprintDecrementStamina = true;
            SetCharacterMovementBools();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            MovementUpdate(0, 0);
        }

        // Update that is better for physics
        protected virtual void FixedUpdate()
        {
            MoveCharacter(0, 0);
        }

        protected void MovementUpdate(float horizontalInput, float verticalInput)
        {
            IsGrounded = Physics.CheckSphere(transform.position, 0.3f, IsGround);
            ApplyGravity();

            //Only listen to new movement input when on the ground (keeps "momentum" in air)
            if(IsGrounded)
            {
                CharacterMovementState.PlayerInputX = horizontalInput;
                CharacterMovementState.PlayerInputY = verticalInput;

                SetCharacterMovementBools();
            }     
        }

        protected void SetCharacterMovementBools()
        {
            CrouchCheck();
            JumpCheck();
            SprintCheck();
            IsWalking = VerticalInputRaw > 0;
            IsWalkingBackwards = VerticalInputRaw < 0;
            IsStrafing = VerticalInputRaw == 0 && HorizontalInputRaw != 0;
        }

        protected void MoveCharacter(float horizontalInput, float verticalInput)
        {
            moveDirection = Character.forward * verticalInput + Character.right * horizontalInput;
            
            if(IsCrouching)
            {
                CharacterMovementState.SetIsCrouching();
                CharacterController.Move(moveDirection.normalized * MoveSpeed * CrouchingSpeedMuliplier * Time.deltaTime);
            }
            else if (!IsGrounded) 
            {
                CharacterMovementState.SetIsJumping();
                CharacterController.Move(moveDirection.normalized * MoveSpeed * AirMultiplier * Time.deltaTime);
            }
            else if(IsSprinting) 
            {
                CharacterMovementState.SetIsSprinting();
                CharacterController.Move(moveDirection.normalized * MoveSpeed * SprintSpeedMultiplier * Time.deltaTime);
                
                //Decrease Stamina
                if(CanSprintDecrementStamina)
                {
                    CanSprintDecrementStamina = false;
                    BaseStaminaManager.DecreaseStamina(SprintStaminaCost);
                    Invoke("ResetCanSprintDecrementStamina", 1f);
                }
            }
            else if (IsWalking)
            {
                CharacterMovementState.ResetMovementModifiers();
                CharacterController.Move(moveDirection.normalized * MoveSpeed * Time.deltaTime);       
            }
            else if (IsWalkingBackwards) 
            {
                CharacterMovementState.ResetMovementModifiers();
                CharacterController.Move(moveDirection.normalized * MoveSpeed * WalkingBackwardsSpeedMuliplier * Time.deltaTime);
            }
            else if (IsStrafing) 
            {
                CharacterMovementState.ResetMovementModifiers();
                CharacterController.Move(moveDirection.normalized * MoveSpeed * StrafeMultiplier * Time.deltaTime);
            }            
            else
            {
                CharacterMovementState.ResetMovementModifiers();
            }            
        }
        protected abstract void SprintCheck();

        protected abstract void JumpCheck();

        protected void Jump()
        {
            IsJumping = true;
            CanJump = false;
            velocity.y = MathF.Sqrt(JumpHeight * -2f * Gravity);
            
            //Decrease Stamina
            BaseStaminaManager.DecreaseStamina(JumpStaminaCost);
            Invoke(nameof(ResetJump), JumpCooldown);
        }

        protected void ResetJump()
        {
            CanJump = true;
            IsJumping = false;
        }

        protected abstract void CrouchCheck();

        protected void Crouch()
        {
            IsCrouching = true;
            CanJump = false;
        }

        protected void StopCrouching()
        {
            IsCrouching = false;
            ResetJump();
        }

        private void ApplyGravity()
        {
            //Reset Gravity velocity
            if(IsGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += Gravity * Time.deltaTime;
            CharacterController.Move(velocity * Time.deltaTime);
        }

        private void ResetCanSprintDecrementStamina()
        {
            CanSprintDecrementStamina = true;
        }
    }
}