using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour {

    [Header("Events Listeners")]
    [SerializeField] private GridObjectEventChannelSO OnPlantEvent;
    [SerializeField] private LevelDataEventChannelSO OnLevelLoadEvent;

    private List<Transform> seeds_list;

    private void OnEnable() {
        OnPlantEvent.OnEventRaised += PlantCropOnFarmland;
        OnLevelLoadEvent.OnEventRaised += InitCropSeeds;
    }

    private void OnDisable() {
        OnPlantEvent.OnEventRaised -= PlantCropOnFarmland;
        OnLevelLoadEvent.OnEventRaised -= InitCropSeeds;
    }

    public void PlantCropOnFarmland(GridObject gridObject) {

        if(seeds_list.Count > 0) {
            var farmland = gridObject.GetLand().GetComponent<Farmland>();

            if(farmland == null) return;

            if(farmland.PlantCrop(seeds_list[0])) {
                seeds_list.RemoveAt(0);
            }
        }
    }

    public void InitCropSeeds(LevelDataSO levelData) {
        seeds_list = new List<Transform>(levelData.seeds_list);
    }
}
