using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class escapeMenu : MonoBehaviour {

    [Header("References")]
    public GameObject escMenu;
    public GameObject optionsMenu;
    public Dropdown QualitySettingsDrop;
    public Slider mouseSensitivitySlider;
    public Toggle mouseInvert;
    [Space(10)]
    [Header("Player")]
    public GameObject player;
    [Space(10)]
    [Header("Menu Visibility")]
    public bool windowFocused;
    public bool menusOpen;
    public bool menuVisible;
    public bool optVisible;

    NewPlayerBehaviour sNPB;

    private void Start() {

        if (player != null) {
            sNPB = player.GetComponent<NewPlayerBehaviour>();
        }

        if (player = null) {
            Debug.LogError("Player reference missing in Escape Menu Handler");
        }




        QualitySettingsDrop.value = QualitySettings.GetQualityLevel();

        mouseInvert.isOn = sNPB.mInvert;

        Cursor.lockState = CursorLockMode.Locked;

        optVisible = false;
        menusOpen = false;
    }

    private void Update() {
        sNPB.mouseSensitivity = mouseSensitivitySlider.value;

        if (Input.GetButtonDown("Cancel") && optVisible == false) {
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
        if(!menuVisible && !optVisible) {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void QuitButton() {

        SceneManager.LoadScene(0);
    }
    public void Resume() {
        if (menuVisible) {
            menuVisible = false;
        }
    }
    public void ChangeVolume(float newValue) {
        float newVol = AudioListener.volume;
        newVol = newValue;
        AudioListener.volume = newVol;
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

    public void MouseSensChanged() {

        var value = mouseSensitivitySlider.value;
        
    }

    public void MouseInvert() {
        sNPB.MouseInvert();
    }

    public void QualiySettings() {

        var qValue = QualitySettingsDrop.value;
        QualitySettings.SetQualityLevel(qValue);
    }
   
    private void OnApplicationFocus(bool focus) {

        windowFocused = focus;
    }
}
