using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterable : Plantable {

    [Header("Broadcasting on")]
    //UI
    [SerializeField] private VoidEventChannelSO WaterIsNeedEvent;

    [Header("Waterable values")]
    [SerializeField] private bool isWatered = false;

   

    public override bool CanGrowthUp() {
        if(currentPhase > (cropData.GetPhaseCount() - 1)) {
            return false;
        }

        if(!isWatered) {
            return false;
        }

        if(currentGrowthTick < tickToGrowth) {
            currentGrowthTick++;
            return false;
        }

        return true;
    }


    private bool IsWaterNeeded() {
        return !isWatered;
    }

    public bool GetWatered() {

        // Water is not needed
        if(!IsWaterNeeded()) return false;
        
        isWatered = true;

        return true;
    }

}
