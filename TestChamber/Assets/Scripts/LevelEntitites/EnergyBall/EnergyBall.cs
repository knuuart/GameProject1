using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {
    Rigidbody rb;
    public float lifeTime, timer;
    GameObject spawner;
    BallSpawner bs;
    Renderer rend;
    public Color pink, green;
    float duration = 4f, t = 0;
    Color lerpedColor;
    bool flag;
    public Material ballMat;
    public float tScrollSpeed;
    public ParticleSystem ballTrail;

	// Use this for initialization
	void Start () {
        spawner = GameObject.Find("BallSpawner");
        bs = spawner.GetComponent<BallSpawner>();
        rb = gameObject.GetComponent<Rigidbody>();
        lifeTime = bs.lifeTime;
        rb.AddForce(spawner.transform.forward, ForceMode.Impulse);
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        
        if (timer > lifeTime) {
            DestroyBall();
        }
        lerpedColor = Color.Lerp(green, pink, t);
        rend.material.color = lerpedColor;
        

        var tOffset = rend.material.mainTextureOffset;
        rend.material.mainTextureOffset = new Vector2(tOffset.x - tScrollSpeed * Time.deltaTime, 
                                                      tOffset.y - tScrollSpeed * Time.deltaTime);

        if (flag) {
            t -= Time.deltaTime / duration;
            if(t < 0.1f) {
                flag = false;
            } 
        } else {
            t += Time.deltaTime / duration;
            if (t > 0.9f) {
                flag = true;
            }
        }

        var boffset = ballMat.mainTextureOffset;
        boffset.x = tScrollSpeed * Time.deltaTime;
        ballMat.mainTextureOffset = boffset;

    }
    public void DestroyBall() {
        Destroy(gameObject);
    }

}
