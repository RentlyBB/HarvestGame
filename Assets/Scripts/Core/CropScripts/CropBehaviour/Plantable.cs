using System.Collections.Generic;
using UnityEngine;

namespace HarvestCode.Core {
    public class Plantable : MonoBehaviour {
        public enum CropState { Not_Harvestable, Harvestable, Rotten };

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
            UpdateState();
        }

        public void UpdateCrop() {

            if(currentPhase > 0) { 
                cropPhases_list[currentPhase - 1].SetActive(false);
            }
            cropPhases_list[currentPhase].SetActive(true);
        }

        public void GrowthUp() {

            if(!CanGrowthUp()) return;

            currentPhase++;

            UpdateCrop();
            UpdateState();


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

        private void UpdateState() {
            if(currentPhase >= rottenPhase) {
                cropState = CropState.Rotten;
            } else if(currentPhase == harvestablePhase) {
                cropState = CropState.Harvestable;
            } else {
                cropState = CropState.Not_Harvestable;
            }
        }

        public bool CanBeHarvest() {

            switch(cropState) {
                case CropState.Harvestable:
                    return true;
                case CropState.Rotten:
                    return true;
                default:
                    return false;
            }
        }
    }
}
