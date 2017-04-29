using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextuuri : MonoBehaviour {
    public RenderTexture oranssi, sininen;
    int screenWidth, screenHeight;
	// Use this for initialization
	void Start () {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        oranssi.height = screenHeight;
        oranssi.width = screenWidth;
        sininen.height = screenHeight;
        sininen.width = screenWidth;

	}
	
	// Update is called once per frame
	void Update () {

    }
}
