using System;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Scripts
{
    public class GameState : MonoBehaviour
    {
        public static bool isPaused;
        public KeyCode pauseKeyCode = KeyCode.Escape;
        public Canvas pauseMenuCanvas;
        bool pauseKeyPressed;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            pauseMenuCanvas = GetComponent<Canvas>();
        }

        // Update is called once per frame
        void Update()
        {
            ListenForPause();
        }

        void ListenForPause()
        {
            pauseKeyPressed = Input.GetKeyDown(pauseKeyCode);

            if(pauseKeyPressed && !isPaused)
            {
                PauseGame();               
                return;
            }

            if(pauseKeyPressed && isPaused)
            {
                ResumeGame();
                return;
            }
        }

        public void PauseGame()
        {
            isPaused = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;

            if(pauseMenuCanvas is not null)
            {
                pauseMenuCanvas.enabled = true;
            }
        }

        public void ResumeGame()
        {
            isPaused = false; 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;


            if(pauseMenuCanvas is not null)
            {
                pauseMenuCanvas.enabled = false;
            }
        }
    }
}