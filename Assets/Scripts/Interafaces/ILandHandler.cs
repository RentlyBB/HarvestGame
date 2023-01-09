using UnityEngine;

public interface ILandHandler  {

    public void SpawnCrop();
    public void SetCrop(CropSO crop, int currentPhase);
    public CropSO GetCrop();
 
}
