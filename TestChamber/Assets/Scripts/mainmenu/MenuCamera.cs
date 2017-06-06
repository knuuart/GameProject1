using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCamera : MonoBehaviour {

    public Image image;
    public Material walls;
    public float speed;

    public float fadeInSpeed;

    public float Xrot;
    public float Yrot;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        //wall sliding

        var ass = walls.mainTextureOffset;
        walls.mainTextureOffset = new Vector2(0, ass.y - speed * Time.deltaTime);


        //Fadeout

        Color c = image.color;
        if (c.a > 0) {
            c.a = c.a - fadeInSpeed * Time.deltaTime;
            image.color = c;
        } else {
            image.enabled = false;
        }

        //Camera movement

        transform.Rotate(new Vector3(Yrot * Time.deltaTime, Xrot * Time.deltaTime, 0));
	}
}
