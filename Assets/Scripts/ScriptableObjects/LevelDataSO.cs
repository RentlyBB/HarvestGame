using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataSO : ScriptableObject {

    public int width;
    public int height;

    [SerializeField] public List<TileData> tileData_list;

    public void SetData() {
        for(int x = 0; x < width; x++) {
            for(int z = 0; z < height; z++) {
                tileData_list.Add(new TileData(x, z));
            }
        }
    }
}