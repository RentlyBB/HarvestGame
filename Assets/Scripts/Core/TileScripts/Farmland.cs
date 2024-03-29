﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarvestCode.Utilities;
using HarvestCode.DataClasses;
using HarvestCode.Crops;

namespace HarvestCode.Tiles {
    public class Farmland : MonoBehaviour, IInteractableTile {

        [SerializeField] private PlayerDataSO playerData;

        [SerializeField] private GameObject dryFarmland;
        [SerializeField] private GameObject wateredFarmland;

        [SerializeField] public FarmlandState currentState = FarmlandState.Dry;

        private Plantable plantedCrop;

        private bool canGrowthUp = true;

        // this bool delaying plant
        private bool canPlant = true;

        private void Start() {
            SetFarmlandState(currentState);
        }

        public void Interact() {
            if(canPlant) {
                PlantCrop();
            } else {
                canPlant = true;
            }
        }

        public bool WaterFarmland() {
            // Is already watered
            if(currentState == FarmlandState.Watered) return false;

            SetFarmlandState(FarmlandState.Watered);
            return true;
        }

        // Update visual of the farmland.
        public void SetFarmlandState(FarmlandState state) {
            currentState = state;

            switch(currentState) {
                case FarmlandState.Watered:
                    dryFarmland.SetActive(false);
                    wateredFarmland.SetActive(true);
                    break;
                case FarmlandState.Dry:
                    dryFarmland.SetActive(true);
                    wateredFarmland.SetActive(false);
                    break;
                default:
                    Debug.LogWarning("Unknown FarmlandState.");
                    break;
            }
        }

        public bool PlantCrop() {

            //Crop is already planted on this farmland
            if(!CanPlantCrop()) {
                // Process bad harvest
                HarvestCrop();

                return false;
            } 

           
            var cropToPlant = playerData.GetLastCropSeed();
            // Player has no seeds, cancel it
            if(cropToPlant == null) return false;

            var tempCropGameObject = Instantiate(cropToPlant.GetPrefab().transform, transform.position, Quaternion.identity);
            tempCropGameObject.position = new Vector3(tempCropGameObject.position.x, 0.05f, tempCropGameObject.position.z);
            tempCropGameObject.SetParent(transform);

            plantedCrop = tempCropGameObject.GetComponent<Plantable>();

            plantedCrop.SetCurrentPhase(0);
            plantedCrop.UpdateCropPhase();

            canGrowthUp = false;
            return true;
        }

        public void GrowthUpCrop() {
            if(plantedCrop == null) return;

            if(!canGrowthUp) {
                canGrowthUp = true;
                return;
            }
            
            plantedCrop.GrowthUp(currentState);
        }

        public bool HarvestCrop() {
            //If there is no crop, cancel it
            if(CanPlantCrop()) return false;

            //FIXME: can be Not-Harvestable
            plantedCrop.CanBeHarvest();

            SetFarmlandState(FarmlandState.Dry);
            DestroyCrop();
            canPlant = false;
            return true;
        }

        public void DestroyCrop() {
            Destroy(plantedCrop.gameObject);
            plantedCrop = null;
        }

        public Plantable GetCrop() {
            return plantedCrop;
        }

        public bool CanPlantCrop() {
            return plantedCrop == null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crop">Crop to be created.</param>
        /// <param name="cropStartPhaseID">Crops start growth phase.</param>
        /// <returns></returns>
        public bool PlantCrop(Transform crop, int cropStartPhaseID = 0) {
            if(!CanPlantCrop()) {
                return false;
            }

            var tempCropGameObject = Instantiate(crop, transform.position, Quaternion.identity);
            tempCropGameObject.position = new Vector3(tempCropGameObject.position.x, 0.05f, tempCropGameObject.position.z);
            tempCropGameObject.SetParent(transform);

            plantedCrop = tempCropGameObject.GetComponent<Plantable>();

            plantedCrop.SetCurrentPhase(cropStartPhaseID);
            plantedCrop.UpdateCropPhase();

            return true;
        }




    }
}


