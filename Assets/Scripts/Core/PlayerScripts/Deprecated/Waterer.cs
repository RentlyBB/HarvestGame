using System.Collections;
using UnityEngine;

namespace HarvestCode.Core {
    public class Waterer : MonoBehaviour {

        [SerializeField] private bool canWater = false;

        public bool WaterFarmland(Farmland farmland) {
            if(!canWater) return false;

            //if(farmland.WaterFarmland()) {
            //    canWater = false;
            //    return true;
            //}

            return false;
        }

        public void GetWater() {
            canWater = true;
        }

    }
}