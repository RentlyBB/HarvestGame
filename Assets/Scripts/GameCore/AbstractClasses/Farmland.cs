using System.Collections;
using UnityEngine;

public abstract class Farmland : MonoBehaviour {

    //Temporary
    [SerializeField] protected CropSO crop;

    //TODO: Rename it to crop after refactor
    [SerializeField] protected Transform cropObject;


    //Temporary
    public bool canSpawnCrop = false;
    public bool canPlantCrop = false;

    //Temporary
    protected int currentPhase;

    private void Start() {
        OnStart();
    }

    protected virtual void OnStart() {
       
    }


    public virtual void PlantCrop() {
        
    }

    public virtual void SetCrop(CropSO crop) {
        this.crop = crop;
        canSpawnCrop = true;
    }

    public virtual CropSO GetCrop() {
        return this.crop;
    }

    //Temporary
    public virtual void SetCrop(CropSO crop, int currentPhase) {
        this.crop = crop;
        this.currentPhase = currentPhase;
        canSpawnCrop = true;
    }

    //Temporary
    public virtual void SpawnCrop() {
        if(!canSpawnCrop) return;

        if(transform.childCount != 0) {
            Destroy(transform.GetChild(0).gameObject);
        }

        var currentCropPrefab = Instantiate(crop.phasePrefabs[currentPhase], transform.position, Quaternion.identity);
        currentCropPrefab.position = new Vector3(currentCropPrefab.position.x, 0.1f, currentCropPrefab.position.z);
        currentCropPrefab.SetParent(transform);
    }

}
