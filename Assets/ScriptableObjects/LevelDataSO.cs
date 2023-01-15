using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelDataSO : ScriptableObject {

    public int width;
    public int height;

    [SerializeField] public List<Transform> cropToPlant_list;
    [SerializeField] public List<TileData> tileData_list;

    public void InitEmptyLevel() {

        tileData_list = new List<TileData>();

        for(int x = 0; x < width; x++) {
            for(int z = 0; z < height; z++) {
                tileData_list.Add(new TileData(x, z));
            }
        }
    }

    public void SetLandToLevel(int x, int z, LandSO land) {
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
}