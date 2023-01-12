using System.Collections;
using UnityEngine;

public class Planter : MonoBehaviour {

    [SerializeField] private int seeds = 2;

    private void OnEnable() {
        GridMovement.plantEvent += PlantCropOnFarmland;
    }

    private void OnDisable() {
        GridMovement.plantEvent -= PlantCropOnFarmland;
    }

    private void PlantCropOnFarmland() {

        if(seeds > 0) {
            var gridObject = GameManager.Instance.grid.GetGridObject(transform.position);
            var farmland = gridObject.GetLand().GetComponent<Farmland>();
            if(farmland.PlantCrop(farmland.plantCropTest)) { 
                seeds--;
            }
        }
    }
}
