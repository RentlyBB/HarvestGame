using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HarvestCode.Core {
    public class Walkable : MonoBehaviour {

        [SerializeField] private bool isWalkable = true;

        public bool IsWalkable() {
            return isWalkable;
        }

        public void SetWalkable(bool isWalkable) {
            this.isWalkable = isWalkable;
        }

    }
}