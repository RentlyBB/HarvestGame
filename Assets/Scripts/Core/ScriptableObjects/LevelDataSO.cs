using System.Collections.Generic;
using UnityEngine;
using HarvestCode.Core;

[System.Serializable]
[CreateAssetMenu(menuName = "Gameplay/LevelDataSO")]
public class LevelDataSO : ScriptableObject {

    public int width = 0;
    public int height = 0;

    [SerializeField] public Vector3 playerStartPoint;

    [SerializeField] public List<TileData> tileData_list = new List<TileData>();
    
    //Persistant data of crop seeds 
    [Header("Persistant data - this should not be change in gameplay")]
    [SerializeField] public List<Transform> seeds_list = new List<Transform>();

    //Data of crop seeds used in game 
    // public List<Transform> cropSeeds_list = new List<Transform>();

    public void InitEmptyLevel() {

        tileData_list = new List<TileData>();

        for(int x = 0; x < width; x++) {
            for(int z = 0; z < height; z++) {
                tileData_list.Add(new TileData(x, z));
            }
        }
    }

    public void SetLandToLevel(int x, int z, LandDataSO land) {
        if(tileData_list.Count == 0) {
            InitEmptyLevel();
        } 

        var tileData = tileData_list.Find(t => t.GetX() == x && t.GetZ() == z);

        tileData.SetLand(land);
    }

    public void RemoveLand(int x, int z) {
        var tileData = tileData_list.Find(t => t.GetX() == x && t.GetZ() == z);

        if(tileData != null) {
            tileData.SetCrop(null);
            tileData.SetLand(null);
        }
    }

    public void SetCropOnLand(int x, int z, Transform crop) {
        if(tileData_list.Count == 0) {
            InitEmptyLevel();
        }

        var tileData = tileData_list.Find(t => t.GetX() == x && t.GetZ() == z);
        tileData.SetCrop(crop);
        tileData.SetStartPhase(0);
    }

    public TileData GetTileDataAt(int x, int z) {
        var tileData = tileData_list.Find(t => t.GetX() == x && t.GetZ() == z);

        if(tileData == null) Debug.Log("tile data is null");

        return tileData;
    }

    public void ClearLevel() {
        tileData_list.Clear();
        InitEmptyLevel();
    }

    public void SetCropStartPhase(int x, int z, int phase) {
        var tileData = tileData_list.Find(t => t.GetX() == x && t.GetZ() == z);

        tileData.SetStartPhase(phase);
    }

    public void AddCropSeedToList(Transform crop) {
        seeds_list.Add(crop);
    }

    public void RemoveCropSeedFromList() {
        seeds_list.Remove(seeds_list[^1]);
    }

    public void Reset() {
        //cropSeeds_list = new List<Transform>(seeds_list);
    }
}

