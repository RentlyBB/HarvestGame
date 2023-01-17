using UnityEngine;

public class Harvester : MonoBehaviour {


    public void HarvestFarmland(Farmland farmland) {
        farmland.HarvestCrop();
    }
}
