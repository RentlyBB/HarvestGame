using UnityEngine;

public class Harvester : MonoBehaviour {

    [Header("Events Listeners")]
    [SerializeField] private GridObjectEventChannelSO OnHarvestEvent;

    private void OnEnable() {
        OnHarvestEvent.OnEventRaised += HarvestFarmland;
    }

    private void OnDisable() {
        OnHarvestEvent.OnEventRaised -= HarvestFarmland;
    }

    public void HarvestFarmland(GridObject gridObject) {
        var farmland = gridObject.GetLand().GetComponent<Farmland>();

        if(farmland != null) {
            farmland.HarvestCrop();
        } 
    }
}
