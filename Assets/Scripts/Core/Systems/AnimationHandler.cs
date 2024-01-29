using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HarvestCode.Systems {
    public class AnimationHandler : MonoBehaviour {

        public Animator animator;

        private void OnEnable() {
            animator.Play("LandSpawnClip");
        }
    }
}