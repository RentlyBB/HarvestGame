﻿using System.Collections;
using UnityEngine;


public abstract class Farmland : MonoBehaviour {

    [Header("Crop planted on the land. Can be null.")]
    [SerializeField] protected Transform crop;

    public Transform plantCropTest;

    public delegate void OnHarvest();
    public static event OnHarvest onHarvest;

    private void Start() {
        OnStart();
    }

    protected virtual void OnStart() {

    }

    public virtual bool PlantCrop(Transform crop, int cropStartPhaseID = 0) {
        if(!CanPlantCrop()) return false;

        this.crop = Instantiate(crop, transform.position, Quaternion.identity);
        this.crop.position = new Vector3(this.crop.position.x, 0.05f, this.crop.position.z);
        this.crop.SetParent(transform);

        var plantable = this.crop.GetComponent<Plantable>();
        plantable.SetCurrentPhase(cropStartPhaseID);
        plantable.CreateCrop();

        return true;
    }

    public virtual void HarvestCrop() {
        //If there is no crop, cancle it
        if(CanPlantCrop()) return;

        //If crop is not harvestable yet, cancle it
        if(!crop.GetComponent<Plantable>().CanBeHarvest()) return;

        Destroy(crop.gameObject);
        crop = null;

        onHarvest?.Invoke();
    }

    public virtual Transform GetCrop() {
        return this.crop;
    }

    public bool CanPlantCrop() {
        return crop == null;
    }
}

