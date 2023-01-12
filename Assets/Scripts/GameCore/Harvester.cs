using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour {

    private void OnEnable() {
        GridMovement.harvestEvent += HarvestFarmland;
    }

    private void OnDisable() {
        GridMovement.harvestEvent -= HarvestFarmland;
    }

    private void HarvestFarmland() {
        var gridObject = GameManager.Instance.grid.GetGridObject(transform.position);
        var farmland = gridObject.GetLand().GetComponent<Farmland>();
        farmland.HarvestCrop();
    }


}
