using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : VoidEventListener {


    public void HarvestFarmland() {
        var gridObject = GameManager.Instance.grid.GetGridObject(transform.position);
        var farmland = gridObject.GetLand().GetComponent<Farmland>();
        farmland.HarvestCrop();
    }


}
