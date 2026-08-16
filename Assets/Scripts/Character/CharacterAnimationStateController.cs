using System.Linq;
using UnityEngine;
using UnityEditor.Animations;
using Unity.VisualScripting;

namespace Scripts.Character
{
    public class CharacterAnimationStateController : MonoBehaviour
    {
        public Animator animator;
        int isSprintingHash;
        int isCrouchingHash;
        int isJumpingHash;
        int inputXHash;
        int inputYHash;

        
        void Awake()
        {
            DeclareStringHashes();
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }


        public void SetPlayerInputVariables(CharacterMovementState CharacterAnimationState)
        {
            if(!animator.IsUnityNull())
            {
                animator.SetFloat(inputXHash, CharacterAnimationState.PlayerInputX);
                animator.SetFloat(inputYHash, CharacterAnimationState.PlayerInputY);
                animator.SetBool(isSprintingHash, CharacterAnimationState.CheckIsSprinting());
                animator.SetBool(isJumpingHash, CharacterAnimationState.CheckIsJumping());
                animator.SetBool(isCrouchingHash, CharacterAnimationState.CheckIsCrouching());
            }
        }

        private void DeclareStringHashes()
        {
            isSprintingHash = Animator.StringToHash("isSprinting");
            isJumpingHash = Animator.StringToHash("isJumping");
            isCrouchingHash = Animator.StringToHash("isCrouching"); 

            inputXHash = Animator.StringToHash("InputX");
            inputYHash = Animator.StringToHash("InputY");
        }

    }
}