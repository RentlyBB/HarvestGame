using System.Collections;
using UnityEngine;

public class Planter : MonoBehaviour {

    [Header("Number of seed to be plant in level")]
    [SerializeField] private int seeds = 2;

    [Header("Events Listeners")]
    [SerializeField] private VoidEventChannelSO OnPlantEvent;

    private void OnEnable() {
        OnPlantEvent.OnEventRaised += PlantCropOnFarmland;
    }

    private void OnDisable() {
        OnPlantEvent.OnEventRaised -= PlantCropOnFarmland;
    }

    public void PlantCropOnFarmland() {

        if(seeds > 0) {
            var gridObject = GameManager.Instance.grid.GetGridObject(transform.position);
            var farmland = gridObject.GetLand().GetComponent<Farmland>();

            if(farmland == null) return;

            if(GameManager.Instance.levelData.cropToPlant_list.Count != 0 && farmland.PlantCrop(GameManager.Instance.levelData.cropToPlant_list[0])) {
                seeds--;
            }
        }
    }
}
