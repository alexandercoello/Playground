using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Objects.Base
{
    public abstract class Interactable : MonoBehaviour //DynamicSceneObjectController
    {
        public List<GameObject> LinkedGameObjects;
        public bool IsInteractable;
        private bool isActive;                
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;

                OnIsActiveChanged();
            }
        }


        public abstract void OnInteract();

        public abstract void OnIsActiveChanged();

    }
}