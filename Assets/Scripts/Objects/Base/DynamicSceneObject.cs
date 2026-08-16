using UnityEngine;



namespace Scripts.Objects.Base
{
    public abstract class DynamicSceneObject : MonoBehaviour
    {

        public bool IsActive;


        public abstract void OnActivate();

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
             IsActive = false;
        }

    }
}