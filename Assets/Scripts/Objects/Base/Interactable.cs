using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Objects.Base
{

    /// <summary>
    /// Base Class Object that a player is able to Intertact with using their Interact button.
    /// </summary>
    /// TODO: Refactor out the Dynamic SceneObjectController to simplify this class
    public abstract class Interactable : DynamicSceneObjectController
    {
        public bool IsInteractable;

        public abstract void OnInteract();

    }
}