using System.Collections;
using UnityEngine;

namespace HarvestCode.Core {
    public class ResourceTile : MonoBehaviour {

        [SerializeField] private ResourceTypeSO resource;
        public ResourceTypeSO GetResource() {
            return resource;
        }
    }
}

