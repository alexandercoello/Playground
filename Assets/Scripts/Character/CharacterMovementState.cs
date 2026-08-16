using UnityEngine;
using Scripts;

namespace Scripts.Character
{
    public class CharacterMovementState : MonoBehaviour
    {
        public CharacterAnimationStateController animationStateController;

        public CharacterMovementState()
        {
            PlayerInputX = 0;
            PlayerInputY = 0;
        }

        public CharacterMovementState(float playerInputX, float playerInputY, bool isSprinting, bool isJumping, bool isCrouching)
        {
            PlayerInputX = playerInputX;
            PlayerInputY = playerInputY;
            IsSprinting = isSprinting;
            IsJumping = isJumping;
            IsCrouching = isCrouching;
        }

        public float PlayerInputX;
        public float PlayerInputY;

        bool IsSprinting;
        bool IsJumping;
        bool IsCrouching;


        public void SetIsSprinting()
        {
            if(!CheckIsSprinting())
            {
                setState(isSprinting:true);
            }
        }

        public bool CheckIsSprinting()
        {
            return IsSprinting;
        }

        public void SetIsJumping()
        {
            if(!CheckIsJumping())
            {
                setState(isJumping:true);
            }
        }

        public bool CheckIsJumping()
        {
            return IsJumping;
        }

        public void SetIsCrouching()
        {
            if(!CheckIsCrouching())
            {
                setState(isCrouching:true);
            }
        }

        public bool CheckIsCrouching()
        {
            return IsCrouching;
        }

        public void ResetMovementModifiers()
        {
            //Setting state with no params should set all to false and update x/y
            setState();
        }

        private void setState(bool isSprinting = false, bool isJumping = false, bool isCrouching = false)
        {
            IsSprinting = isSprinting;
            IsJumping = isJumping;
            IsCrouching = isCrouching; 

            animationStateController.SetPlayerInputVariables(this);
        }

    }

}