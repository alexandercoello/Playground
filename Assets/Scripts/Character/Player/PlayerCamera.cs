using Unity.Mathematics;
using UnityEngine;

namespace Scripts
{
    public class PlayerCamera : MonoBehaviour
    {
        public Transform playerCameraPosition;
        public Vector3 offset;

        // Update is called once per frame
        void Update()
        {
            transform.position = playerCameraPosition.position + offset;
        }
    }
}