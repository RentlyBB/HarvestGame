using System.Collections;
using UnityEngine;


public interface ITileHandler  {

    public void SpawnCrop();
    public void GrowthUp();
    public void SetCrop(CropSO crop, int currentPhase);
    public CropSO GetCrop();
 

}
