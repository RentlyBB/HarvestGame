using System.Collections;
using UnityEngine;


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
