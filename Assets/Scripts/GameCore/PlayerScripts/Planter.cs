﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour {

    [Header("Broadcasting Events")]
    [SerializeField] private VoidEventChannelSO OnPlantEvent;

    [Header("Listen to")]
    [SerializeField] private LevelDataEventChannelSO OnLevelLoadEvent;

    private List<Transform> seeds_list;

    private void OnEnable() {
        OnLevelLoadEvent.OnEventRaised += InitCropSeeds;
    }

    private void OnDisable() {
        OnLevelLoadEvent.OnEventRaised -= InitCropSeeds;
    }

    public void PlantCropOnFarmland(Farmland farmland) {
        if(seeds_list.Count > 0) {
            if(farmland.PlantCrop(seeds_list[0])) {
                seeds_list.RemoveAt(0);
                OnPlantEvent.RaiseEvent();
            }
        }
    }

    public void InitCropSeeds(LevelDataSO levelData) {
        seeds_list = levelData.cropSeeds_list;
    }
}