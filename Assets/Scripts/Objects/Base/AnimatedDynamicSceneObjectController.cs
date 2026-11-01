using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Scripts.Objects.Base
{

    /// <summary>
    /// Base Class Object that allows controlling of DynamicSceneObjects. Interactables and PlayerDetectors may implement this.
    /// </summary>
    public abstract class AnimatedDynamicSceneObjectController : Interactable 
    {
        [SerializeField] private List<GameObject> LinkedGameObjects;
        [SerializeField] private List<AudioClip> ActivateAudioClips;
        [SerializeField] private List<AudioClip> DeactivateAudioClips;
        public Animator animator;
        AudioSource audioSource;

        void Start()
        {
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public override void OnIsActiveChanged()
        {
            PlaySound();
            ActivateDynamicSceneObjects();     
            animator.SetBool("IsActive", IsActive);       
        }
        
        void ActivateDynamicSceneObjects()
        {
            foreach(GameObject gameObject in LinkedGameObjects)
            {
                DynamicSceneObject dynamicSceneObject = gameObject.GetComponent<DynamicSceneObject>(); 

                dynamicSceneObject.OnActivate();
            }
        }

        void PlaySound()
        {
            if(audioSource.IsUnityNull())
            {
                audioSource = GetComponent<AudioSource>();               
            }

            if(IsActive && !ActivateAudioClips.IsUnityNull() && ActivateAudioClips.Count != 0)
            {
                //Play Activate Sound                
                audioSource.PlayOneShot(PickRandomAudioClip(ActivateAudioClips));
            }
            else if(!IsActive && !DeactivateAudioClips.IsUnityNull() && DeactivateAudioClips.Count != 0)
            {
                //Play DeactivateSound
                audioSource.PlayOneShot(PickRandomAudioClip(DeactivateAudioClips));
            }
        }

        AudioClip PickRandomAudioClip(List<AudioClip> clips)
        {
            return clips[Random.Range(0, clips.Count)];
        }
    }
}