using System.Collections.Generic;
using UnityEngine;

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
            }
        }

        public abstract void OnIsActiveChanged();

    }
}