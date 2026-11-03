using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


namespace Scripts.Utility
{
    public abstract class AudioPlayerBase : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> audioClips;
        [SerializeField] private AudioType audioType;
        //At some point this should read from settings to pull volume based on the AudioType
        [SerializeField] private float volume = 1f;
        [SerializeField] private bool randomizePitch = false;
        [SerializeField] private float lowerPitchRange = 1f;
        [SerializeField] private float upperPitchRange = 1f;

        public void PlaySound(AudioClip clip)
        {
            if(audioSource.IsUnityNull())
            {
                audioSource = GetComponent<AudioSource>();
            }

            if(!clip.IsUnityNull())
            {
                if(randomizePitch)
                {
                    audioSource.pitch = Random.Range(lowerPitchRange, upperPitchRange);
                }
                
                //Play Sound                
                audioSource.PlayOneShot(clip, volume);
            }
        }


        public void PlayRandomSound()
        {
            if(audioSource.IsUnityNull())
            {
                audioSource = GetComponent<AudioSource>();
            }

            if(!audioClips.IsUnityNull() && audioClips.Count != 0)
            {
                if(randomizePitch)
                {
                    audioSource.pitch = Random.Range(lowerPitchRange, upperPitchRange);
                }
                
                //Play Sound                
                audioSource.PlayOneShot(PickRandomAudioClip(audioClips), volume);
            }
        }

        AudioClip PickRandomAudioClip(List<AudioClip> clips)
        {
            return clips[Random.Range(0, clips.Count)];
        }
    }

    public enum AudioType
    {
        Music,
        Ambient,
        SpecialEffects,
        Voicelines,
        UserInterface
    }
}