using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {

    public Animator animator;

    private void OnEnable() {
        animator.Play("LandSpawnClip");
    }
}
