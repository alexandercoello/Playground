using Scripts.Objects.Base;
using UnityEngine;

namespace Scripts.Objects.DynamicObjects
{
    /// <summary>
    /// A test object that can be activated and deactivated.
    /// </summary>
    public class TestBallObject : DynamicSceneObject
    {

        [SerializeField] private Material ActiveMaterial;
        [SerializeField] private Material InactiveMaterial;

        private Material currentMaterial
        {
            get => this.GetComponent<MeshRenderer>().material;
        }


        public override void OnActivate()
        {
            IsActive = !IsActive;
            
            if(IsActive)
            {
                GetComponent<MeshRenderer>().material = ActiveMaterial;
            }
            else
            {
                GetComponent<MeshRenderer>().material = InactiveMaterial;
            }


        }

    }
}