﻿using System.Collections;
using UnityEngine;


public abstract class Farmland : MonoBehaviour {

    protected Transform plantedCrop;

    [SerializeField] private TransformEventChannelSO OnBadHarvest;

    private void Start() {
        OnStart();
    }

    protected virtual void OnStart() {

    }

    public virtual bool PlantCrop(Transform crop, int cropStartPhaseID = 0) {
        if(!CanPlantCrop()) {
            return false;
        }

        plantedCrop = Instantiate(crop, transform.position, Quaternion.identity);
        plantedCrop.position = new Vector3(plantedCrop.position.x, 0.05f, plantedCrop.position.z);
        plantedCrop.SetParent(transform);

        var plantable = this.plantedCrop.GetComponent<Plantable>();
        plantable.SetCurrentPhase(cropStartPhaseID);
        plantable.CreateCrop();


        return true;
    }

    public virtual void HarvestCrop() {
        //If there is no crop, cancel it
        if(CanPlantCrop()) return;

        if(plantedCrop.GetComponent<Plantable>().CanBeHarvest()) {
           
        } else {
            OnBadHarvest.RaiseEvent(transform);
        }

        Destroy(plantedCrop.gameObject);
        plantedCrop = null;

    }

    public virtual void DestroyCrop() {
        Destroy(plantedCrop.gameObject);
        plantedCrop = null;
    }

    public virtual Transform GetCrop() {
        return this.plantedCrop;
    }

    public virtual bool CanPlantCrop() {
        return plantedCrop == null;
    }
}

