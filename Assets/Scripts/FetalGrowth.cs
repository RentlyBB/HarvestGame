using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetalGrowth : MonoBehaviour, ILandHandler {

    [SerializeField] private CropSO crop;

    public bool canSpawnCrop = false;

    private int currentPhase;

    private void OnEnable() {
        Field.onGridInit += SpawnCrop;
        GridMovement.onMovementEnds += GrowthUp;
    }

    private void OnDisable() {
        Field.onGridInit -= SpawnCrop;
        GridMovement.onMovementEnds -= GrowthUp;
    }

    public void SpawnCrop() {

        if(!canSpawnCrop) return;

        if(transform.childCount != 0) {
            Destroy(transform.GetChild(0).gameObject);
        }

        var currentCropPrefab = Instantiate(crop.phasePrefabs[currentPhase], transform.position, Quaternion.identity);
        currentCropPrefab.position = new Vector3(currentCropPrefab.position.x, 0.1f, currentCropPrefab.position.z);
        currentCropPrefab.SetParent(transform);
    }

    // Vector3 worldPosition is useless in this case
    private void GrowthUp(Vector3 worldPosition) {
        if(crop != null && currentPhase < crop.phasePrefabs.Count - 1) {
            currentPhase++;
            SpawnCrop();
        }
    }

    public void HarvestCrop() {
        if(transform.childCount != 0) {
            Destroy(transform.GetChild(0).gameObject);
            canSpawnCrop = false;
        }
    }

    public void SetCrop(CropSO crop, int currentPhase) {
        this.crop = crop;
        this.currentPhase = currentPhase;
        canSpawnCrop = true;
    }

    public CropSO GetCrop() {
        return crop;
    }

    public int GetCurrentPhase() {
        return currentPhase;
    }
}
