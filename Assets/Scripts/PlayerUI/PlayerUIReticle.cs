using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.PlayerUI
{
    public class PlayerUIReticle : MonoBehaviour
    {
        private RectTransform reticleT;

        [Header("Reticle")]
        public float MinimumSize;
        public float MaximumSize;
        public float ChangeSpeed;
        private float currentSize;

        private void Start()
        {
            reticleT = GetComponent<RectTransform>(); 
        }

        public void UpdateReticle(bool playerIsMoving)
        {   
            //Accuracy getting worse
            if(playerIsMoving && currentSize != MaximumSize)
            {
                currentSize = Mathf.Lerp(currentSize, MaximumSize, Time.deltaTime * ChangeSpeed);
            }
            //Accuracy getting better
            else
            {
                currentSize = Mathf.Lerp(currentSize, MinimumSize, Time.deltaTime * ChangeSpeed);
            }
            
            reticleT.sizeDelta = new Vector2(currentSize, currentSize);
        }
        
    }
}