using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class escapeMenu : MonoBehaviour {

    public GameObject panel;
    public bool windowFocused;
    [SerializeField] private bool menuVisible;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            menuVisible = !menuVisible;
        }

        if (menuVisible) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        } else {
            Cursor.visible = false;
        }

        panel.active = menuVisible;

        //What happens when the game window is focused
        if (windowFocused && !menuVisible) {
            Cursor.lockState = CursorLockMode.Locked;
        } else if (windowFocused && menuVisible) {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void QuitButton() {
        Application.Quit();
        Debug.Log("Game is trying to quit");
    }

    private void OnApplicationFocus(bool focus) {
        windowFocused = focus;
    }
}
