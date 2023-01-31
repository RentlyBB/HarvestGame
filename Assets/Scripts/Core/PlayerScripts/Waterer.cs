using System.Collections;
using UnityEngine;

namespace HarvestCode.Core {
    public class Waterer : MonoBehaviour {

        [SerializeField] private bool canWater = false;

        public bool WaterCropOnFarmland(Farmland farmland) {
            if(!canWater) return false;

            if(farmland.WaterCrop()) {
                canWater = false;
                return true;
            }

            return false;
        }

        public void GetWater() {
            canWater = true;
        }

    }
}