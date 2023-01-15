using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour {

    [Header("Events Listeners")]
    [SerializeField] private VoidEventChannelSO OnHarvestEvent;


    private void OnEnable() {
        OnHarvestEvent.OnEventRaised += HarvestFarmland;
    }

    private void OnDisable() {
        OnHarvestEvent.OnEventRaised -= HarvestFarmland;
    }

    public void HarvestFarmland() {
        var gridObject = GameManager.Instance.grid.GetGridObject(transform.position);
        var farmland = gridObject.GetLand().GetComponent<Farmland>();

        if(farmland != null) farmland.HarvestCrop();
    }


}
