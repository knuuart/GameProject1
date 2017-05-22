using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimit : MonoBehaviour {
    public int fpsLimit = 60;

    // Use this for initialization
    void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fpsLimit;
    }
}
