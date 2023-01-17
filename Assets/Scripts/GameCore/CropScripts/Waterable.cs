using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterable : Plantable {

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO WaterIsNeedEvent;

    [Header("Waterable values")]
    [SerializeField] private bool isWatered = false;

    private Plantable plantable = default;

    public override bool CanGrowthUp() {
        if(currentPhase > (cropData.GetPhaseCount() - 1)) {
            return false;
        }

        if(currentGrowthTick < tickToGrowth) {
            currentGrowthTick++;
            return false;
        }

        if(!isWatered) {
            return false;
        }

        return true;
    }

    private void Awake() {
        plantable = GetComponent<Plantable>();
    }


    public void GetWatered() {
        isWatered = true;
    }

}
