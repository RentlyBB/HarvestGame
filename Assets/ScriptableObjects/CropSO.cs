using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CropSO : ScriptableObject {

    [SerializeField] private List<Transform> phasePrefabs;

    [SerializeField] private int harvestablePrefabID;

    [SerializeField] private int overgrownPrefabID;

    [Header("UI")]
    [SerializeField] public Sprite sprite;


    public Transform GetPrefab(int id) {

        if(id < phasePrefabs.Count) { 
            return phasePrefabs[id];
        }

        return phasePrefabs[^1];
    }

    public int GetHarvestablePrefabID() {
        return harvestablePrefabID;
    }

    public int GetOvergrownPrefabID() {
        return overgrownPrefabID;
    }

    public int GetPhaseCount() {
        return phasePrefabs.Count;
    }
}
