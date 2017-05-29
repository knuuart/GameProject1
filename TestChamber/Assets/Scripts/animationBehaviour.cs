using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationBehaviour : MonoBehaviour {

    public Animation anim;

    public void AnimationPlay() {
        anim.Play();
    }

    public void AnimationRewind() {
        anim.Rewind("doorOpen");
    }
}
