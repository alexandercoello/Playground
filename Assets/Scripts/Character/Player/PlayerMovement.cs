using System;
using Unity.VisualScripting;
using UnityEngine;
using Scripts.PlayerUI;

namespace Scripts.Character.Player
{
    public class PlayerMovement : CharacterMovement
    {

        [Header("Player")]
        //Player UI Reticle
        [SerializeField] private PlayerUIReticle reticle;

        [Header("Player Input Keys")]
        [SerializeField] private KeyCode JumpKey = KeyCode.Space;
        [SerializeField] private KeyCode SprintKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode CrouchKey = KeyCode.LeftControl;

        [Header("Player Input Bools")]
        bool jumpPressed;
        bool crouchPressed;
        bool sprintPressed;

        [Header("Player Audio")]
        [SerializeField] private PlayerFootstepAudioPlayer footstepAudioPlayer;
        private float nextFootstepTime;
        [SerializeField] private float walkingFootstepAudioInterval;
        [SerializeField] private float sprintingFootstepAudioInterval;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            CanJump = true;
            CanSprintDecrementStamina = true;
            SetCharacterMovementBools();
        }

        // Update is called once per frame
        protected override void Update()
        {
            PlayerInputUpdate();
        }

        // Update that is better for physics
        protected override void FixedUpdate()
        {
            MoveCharacter(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        private void PlayerInputUpdate()
        {
            HorizontalInputRaw = Input.GetAxisRaw("Horizontal");
            VerticalInputRaw = Input.GetAxisRaw("Vertical");
            crouchPressed = Input.GetKey(CrouchKey);
            sprintPressed = Input.GetKey(SprintKey);
            jumpPressed = Input.GetKeyDown(JumpKey);
            
            MovementUpdate(HorizontalInputRaw, VerticalInputRaw);

            bool playerIsMoving = HorizontalInputRaw != 0 || VerticalInputRaw != 0 || !CanJump;

            if(playerIsMoving)            
                PlayFootstepAudio();
            
            reticle.UpdateReticle(playerIsMoving);               
        }

        private void PlayFootstepAudio()
        {
            if(footstepAudioPlayer != null && Time.time >= nextFootstepTime)
            {
                footstepAudioPlayer.PlayRandomSound();
                nextFootstepTime = Time.time + UnityEngine.Random.Range(0f, 0.05f) + (IsSprinting ? sprintingFootstepAudioInterval : walkingFootstepAudioInterval);
            }
        }

        protected override void JumpCheck()
        {
            if(jumpPressed && CanJump && !IsJumping && BaseStaminaManager.Current >= JumpStaminaCost)
            {
                Jump();
            }
        }

        protected override void CrouchCheck()
        {
            if(crouchPressed)
            {
                Crouch();
            }
            else
            {
                StopCrouching();
            }   
        }

        protected override void SprintCheck()
        {
            IsSprinting =  sprintPressed && VerticalInputRaw > 0 && BaseStaminaManager.Current >= SprintStaminaCost;
        }
    }
}