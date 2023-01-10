using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetalGrowth : Farmland {


    private void OnEnable() {
        Field.onGridInit += SpawnCrop;
        GridMovement.onMovementEnds += GrowthUp;
    }

    private void OnDisable() {
        Field.onGridInit -= SpawnCrop;
        GridMovement.onMovementEnds -= GrowthUp;
    }


    protected override void OnStart() {
        base.OnStart();


    }

    // Vector3 worldPosition is useless in this case
    // GrowthUp method will be in CropBehaviour
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

    public int GetCurrentPhase() {
        return currentPhase;
    }
}
