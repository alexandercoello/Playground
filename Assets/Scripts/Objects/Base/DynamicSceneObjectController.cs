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
    public abstract class DynamicSceneObjectController : MonoBehaviour 
    {
        public List<GameObject> LinkedGameObjects;
        private bool isActive;                
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                OnIsActiveChanged();

                PlaySound();
            }
        }

        public List<AudioClip> ActivateAudioClips;
        public List<AudioClip> DeactivateAudioClips;
        AudioSource audioSource;        

        public abstract void OnIsActiveChanged();

        void PlaySound()
        {
            if(audioSource.IsUnityNull())
            {
                audioSource = GetComponent<AudioSource>();               
            }

            if(isActive && !ActivateAudioClips.IsUnityNull() && ActivateAudioClips.Count != 0)
            {
                //Play Activate Sound                
                audioSource.PlayOneShot(PickRandomAudioClip(ActivateAudioClips));
            }
            else if(!isActive && !DeactivateAudioClips.IsUnityNull() && DeactivateAudioClips.Count != 0)
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