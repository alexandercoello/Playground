using UnityEngine;
using Scripts.Objects.Base;
using System.Collections;

namespace Scripts.Objects.Interactables
{
    /// <summary>
    /// A player openable door
    /// </summary>
    public class Door : Interactable
    {
        Animator animator;

        void Start()
        {
            IsInteractable = true;
            animator = GetComponent<Animator>();
        }

        public override void OnIsActiveChanged()
        {
            animator.SetBool("IsActive", IsActive);
        }

    }
}