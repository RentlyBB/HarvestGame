using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FarmlandState { 
    Watered,
    Dry
}

namespace HarvestCode.Core {
    public class Farmland : MonoBehaviour {

        private Plantable plantedCrop;

        [SerializeField] private GameObject dryFarmland;
        [SerializeField] private GameObject wateredFarmland;

        [SerializeField] private FarmlandState currentState = FarmlandState.Dry;

        private void Start() {
            SetFarmlandState(currentState);
        }

        public bool PlantCrop(Transform crop, int cropStartPhaseID = 0) {
            if(!CanPlantCrop()) {
                return false;
            }

            var tempCropGameObject = Instantiate(crop, transform.position, Quaternion.identity);
            tempCropGameObject.position = new Vector3(tempCropGameObject.position.x, 0.05f, tempCropGameObject.position.z);
            tempCropGameObject.SetParent(transform);

            plantedCrop = tempCropGameObject.GetComponent<Plantable>();

            plantedCrop.SetCurrentPhase(cropStartPhaseID);
            plantedCrop.UpdateCrop();

            return true;
        }

        public void HarvestCrop() {
            //If there is no crop, cancel it
            if(CanPlantCrop()) return;

            //FIXME: can be Not-Harvestable
            if(plantedCrop.CanBeHarvest()) {
                //Good harvest
            } else {
                //Bad Harvest
            }

            SetFarmlandState(FarmlandState.Dry);
            DestroyCrop();
        }

        public bool WaterFarmland() {
            if(currentState == FarmlandState.Watered) return false;

            SetFarmlandState(FarmlandState.Watered);
            return true;
        }

        private void SetFarmlandState(FarmlandState state) {
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

        public void DestroyCrop() {
            Destroy(plantedCrop.gameObject);
            plantedCrop = null;
        }

        public virtual Plantable GetCrop() {
            return plantedCrop;
        }

        public virtual bool CanPlantCrop() {
            return plantedCrop == null;
        }
    }
}


