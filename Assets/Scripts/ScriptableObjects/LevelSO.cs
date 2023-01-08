using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelSO : ScriptableObject {

    [SerializeField] private int startSeeds;

    [SerializeField] public int seeds;

    [SerializeField] public List<TileData> tileData_list;


    private void OnEnable() {
        seeds = startSeeds;
    }

    private void OnDisable() {
        seeds = startSeeds;
    }


    public void SetTileData(int x, int z, CropSO crop) {
        var tile = new TileData(x, z);
        tile.SetCrop(crop);

        if(ValidateData(tile)) {
            tileData_list.Add(tile);
        }
    }

    public TileData GetTileDataAt(int x, int z) {

        foreach(TileData td in tileData_list) {
            if(td.GetX() == x && td.GetZ() == z) {
                return td;
            }
        }

        return default(TileData);
    }

    private bool ValidateData(TileData tile) {

        foreach(TileData td in tileData_list) {
            if(td.GetX() == tile.GetX() && td.GetZ() == tile.GetZ()) {
                tileData_list.Remove(td);
                return false;
            } 
        }

        return true;
    }
}
