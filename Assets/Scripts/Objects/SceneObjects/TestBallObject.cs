using Scripts.Objects.Base;
using UnityEngine;

public class TestBallObject : DynamicSceneObject
{

    public Material ActiveMaterial;
    public Material InactiveMaterial;

    private Material currentMaterial
    {
        get => this.GetComponent<MeshRenderer>().material;
    }


    void Awake()
    {
        
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
