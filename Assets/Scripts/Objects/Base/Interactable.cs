using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Objects.Base
{

    /// <summary>
    /// Base Class Object that a player is able to Intertact with using their Interact button.
    /// </summary>
    /// TODO: Refactor out the Dynamic SceneObjectController to simplify this class
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