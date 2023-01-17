using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Harvester), typeof(Planter))]
public class PlayerBehaviour : MonoBehaviour {

    [SerializeField] private Planter planter = default;
    [SerializeField] private Harvester harvester = default;

    void Awake() {
        planter = GetComponent<Planter>();
        harvester = GetComponent<Harvester>();
    }


    public void InteractWithTile(GridObject gridObject) {

        //Try plant or harvest
        PlantOrHarvest(gridObject);
       

    }


    private void PlantOrHarvest(GridObject gridObject) {

        var farmland = gridObject.GetLand().GetComponent<Farmland>();
        if(farmland == null) return;
        
        if(farmland.CanPlantCrop()) {
            planter.PlantCropOnFarmland(farmland);
        } else {
            harvester.HarvestFarmland(farmland);
        }
    }
}
