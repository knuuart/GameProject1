using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class escapeMenu : MonoBehaviour {

    public GameObject escMenu;
    public GameObject optionsMenu;
    public bool windowFocused;
    public bool menusOpen;
    public bool menuVisible;
    public bool optVisible;

    private void Start() {

        Cursor.lockState = CursorLockMode.Locked;

        optVisible = false;
        menusOpen = false;
    }

    private void Update() {

        if (Input.GetButtonDown("Cancel")) {
            menuVisible = !menuVisible;
        }

        if (menuVisible || optVisible) {
            menusOpen = true;
        } else { menusOpen = false; }

        if (menusOpen) {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        } else {
            Cursor.visible = false;
        }

        escMenu.active = menuVisible;
        
        optionsMenu.active = optVisible;

        //What happens when the game window is focused
        if (windowFocused && !menusOpen) {

            Cursor.lockState = CursorLockMode.Locked;
        } else if (windowFocused && menusOpen) {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void QuitButton() {

        Application.Quit();
        Debug.Log("Game is trying to quit");
    }

    public void OptionsButton(bool close) {

        if (close) {

            optVisible = true;
            menuVisible = false;
        }
        if (!close) {

            optVisible = false;
            menuVisible = true;
        }
    }

    private void OnApplicationFocus(bool focus) {

        windowFocused = focus;
    }
}
