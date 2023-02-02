using HarvestCode.Core;
using System.Collections;
using UnityEngine;

namespace HarvestCode.Core {
    public class Plantable : MonoBehaviour {
        public enum CropState { Not_Harvestable, Harvestable, Rotten };

        [SerializeField] public CropSO cropData;

        [SerializeField] protected int currentPhase = 0;

        [SerializeField] protected CropState state;

        [SerializeField] protected int tickToGrowth = 1;

        protected Transform currentCropPrefab;

        protected int currentGrowthTick = 1;

        private void OnEnable() {
            BaseOnEnable();
        }

        protected virtual void BaseOnEnable() {
        }

        private void OnDisable() {
            BaseOnDisable();
        }


        protected virtual void BaseOnDisable() {
        }


        private void Start() {
            OnStart();
        }

        protected virtual void OnStart() {

            UpdateState();
        }

        public virtual void CreateCrop() {
            currentCropPrefab = Instantiate(cropData.GetPrefab(currentPhase), transform.position, Quaternion.identity);
            currentCropPrefab.SetParent(transform);
        }

        public virtual void GrowthUp() {

            if(CanGrowthUp()) {
                currentPhase++;
                Destroy(currentCropPrefab.gameObject);
                CreateCrop();
                UpdateState();
                currentGrowthTick = 1;
            }
        }

        public virtual bool CanGrowthUp() {

            if(currentPhase > (cropData.GetPhaseCount() - 1)) {
                return false;
            }

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
            if(currentPhase >= cropData.GetOvergrownPrefabID()) {
                state = CropState.Rotten;
                var walkable = GetComponentInParent<Walkable>();

                if(walkable != null) walkable.SetWalkable(true);

            } else if(currentPhase == cropData.GetHarvestablePrefabID()) {
                state = CropState.Harvestable;
                var walkable = GetComponentInParent<Walkable>();

                if(walkable != null) walkable.SetWalkable(true);

            } else {
                state = CropState.Not_Harvestable;
            }
        }

        public bool CanBeHarvest() {

            switch(state) {
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
