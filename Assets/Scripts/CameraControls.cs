using Unity.Mathematics;
using UnityEngine;

namespace Scripts
{
    public class CameraControls : MonoBehaviour
    {
        public float SensX;
        public float SensY;
        public bool IsCameraLocked = false;
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
            if(!IsCameraLocked)
            {
                GetUserInput();
            }                            
        }

        void GetUserInput()
        {
            // This method can be used to update the camera's position or other properties if needed.
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);     
        }

        public void LockCameraAndShowCursor()
        {
            IsCameraLocked = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void UnlockCameraAndHideCursor()
        {
            IsCameraLocked = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void LateUpdate()
        {
            // This is where you would handle any physics-based camera movement if needed.
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            player.rotation = Quaternion.Euler(0, yRotation, 0);
            
        }

    }
}