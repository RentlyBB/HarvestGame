﻿using System.Collections.Generic;
using UnityEngine;
using HarvestCode.Utilities;

namespace HarvestCode.Crops {
    public class Plantable : MonoBehaviour {

        [Header("Crop Setting")]
        [SerializeField] protected CropState cropState;

        [Tooltip("A number of moves the player has to do to grow crop up by one phase.")]
        [SerializeField] protected int tickToGrowth = 1;

        [SerializeField] private int harvestablePhase;

        [SerializeField] private int rottenPhase;

        [SerializeField] protected int currentPhase = 0;

        [SerializeField] private List<GameObject> cropPhases_list = new List<GameObject>();

        protected Transform currentCropPrefab;
        protected int currentGrowthTick = 0;

        private void Start() {
            UpdateCropState();
        }

        public void UpdateCropPhase() {

            if(currentPhase > 0) { 
                cropPhases_list[currentPhase - 1].SetActive(false);
            }
            cropPhases_list[currentPhase].SetActive(true);
        }

        public void UpdateCropPhase(int phase) {

            if(currentPhase > 0) {
                cropPhases_list[currentPhase - 1].SetActive(false);
            }

            cropPhases_list[phase].SetActive(true);
        }

        /// <summary>
        /// GrowthUp method which consider farmland state.
        /// </summary>
        /// <param name="farmlandState"> State of farmland </param>
        public void GrowthUp(FarmlandState farmlandState) {

            if(!CanGrowthUp()) return;

            if(farmlandState == FarmlandState.Watered) {
                currentPhase++;
                UpdateCropPhase();
                UpdateCropState();
            } else if (farmlandState == FarmlandState.Dry) {
                UpdateCropPhase(rottenPhase);
                UpdateCropState(CropState.Rotten);
            }

            // Reset tick timer
            currentGrowthTick = 0;
        }

        /// <summary>
        /// GrowthUp method which not consider any state of farmland, just growup
        /// </summary>
        public void GrowthUp() {
            if(!CanGrowthUp()) return;
            
            currentPhase++;
            UpdateCropPhase();
            UpdateCropState();

            // Reset tick timer
            currentGrowthTick = 0;
        }

        public bool CanGrowthUp() {

            if(currentPhase > (cropPhases_list.Count - 1)) {
                return false;
            }

            if(currentPhase == rottenPhase) return false;

            if(currentGrowthTick < tickToGrowth) {
                currentGrowthTick++;
                return false;
            }

            return true;
        }

        public void SetCurrentPhase(int phaseID) {
            currentPhase = phaseID;
        }

        public int GetCurrentPhase() {
            return currentPhase;
        }

        private void UpdateCropState() {
            if(currentPhase >= rottenPhase) {
                cropState = CropState.Rotten;
            } else if(currentPhase == harvestablePhase) {
                cropState = CropState.Harvestable;
            } else {
                cropState = CropState.Not_Harvestable;
            }
        }

        private void UpdateCropState(CropState state) {
            cropState = state;
        }

        public void CanBeHarvest() {

            switch(cropState) {
                case CropState.Harvestable:
                    Debug.Log("Good Harvest");
                    break;
                case CropState.Rotten:
                    Debug.Log("Rotten Harvest");
                    break;
                default:
                    Debug.Log("Bad Harvest");
                    break;
            }
        }
    }
}
