using UnityEngine;
using Scripts.Objects.Base;

public class DynamicDoor : DynamicSceneObject
{

    public bool IsOpen
    {
        get => IsActive;
        set => IsActive = value;
    }

    public bool IsLocked;

    public override void OnActivate()
    {
        IsActive = !IsActive;

        Transform door = transform.Find("Door");
        if (door == null)
        {
            Debug.LogError("DynamicDoor requires a child named Door.", this);
            return;
        }

        door.localRotation = Quaternion.Euler(0f, IsActive ? 165f : 0f, 0f);
        door.localPosition = new Vector3(IsActive ? 0.6f : 0.522f, 1f, 0.15f);
    }

}
