using System.Collections;
using UnityEngine;

public class Planter : VoidEventListener {

    [Header("Number of seed to be plant in level")]
    [SerializeField] private int seeds = 2;


    public void PlantCropOnFarmland() {

        if(seeds > 0) {
            var gridObject = GameManager.Instance.grid.GetGridObject(transform.position);
            var farmland = gridObject.GetLand().GetComponent<Farmland>();
            if(farmland.PlantCrop(farmland.plantCropTest)) { 
                seeds--;
            }
        }
    }
}
