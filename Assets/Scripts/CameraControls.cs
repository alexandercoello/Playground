using Unity.Mathematics;
using UnityEngine;

namespace Scripts
{
    public class CameraControls : MonoBehaviour
    {
        public float SensX;
        public float SensY;
        float xRotation;
        float yRotation;

        public Transform player;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            player.rotation = Quaternion.Euler(0, yRotation, 0);
                    
        }

    }
}