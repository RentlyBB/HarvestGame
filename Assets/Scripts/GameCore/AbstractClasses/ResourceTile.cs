using System.Collections;
using UnityEngine;

public class ResourceTile : MonoBehaviour {

    [SerializeField] private ResourceTypeSO resource;

    public ResourceTypeSO GetResource() {
        return resource;
    }
    
}
