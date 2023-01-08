using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public enum PlaceModes { 
    TileMode,
    CropMode
}

public class EditorManager : Singleton<EditorManager>{

    public LevelSO editingLevel;

    public TileSO selectedTile;
    public CropSO selectedCrop;

    [Header("0 - Place Tile | 1 - Place Crop")]
    public PlaceModes placeMode;

    public void Place(GridObject gridObject, GridXZ<GridObject> grid) {

        switch(placeMode) {
            case PlaceModes.TileMode:
                PlaceTileOnGrid(gridObject, grid);
                break;
            case PlaceModes.CropMode:
                PlaceCropOnTile(gridObject, grid);
                break;
        }
    }
    
    private void PlaceTileOnGrid(GridObject gridObject, GridXZ<GridObject> grid) {
        if(selectedTile != null) {
            GameObject obj;
            if(gridObject.CanCreateTile()) {
                obj = Instantiate(selectedTile.tilePrefab.gameObject, grid.GetWorldPositionCellCenter(gridObject.GetX(), gridObject.GetZ()), Quaternion.identity);
                gridObject.ClearTile();
                gridObject.SetTile(obj.transform);
            }
        }
    }

    private void PlaceCropOnTile(GridObject gridObject, GridXZ<GridObject> grid) {
        if(selectedCrop != null) {
            if(!gridObject.CanCreateTile()) { 
                var tile = gridObject.GetTile();

                var tileHandler = tile.GetComponent<ITileHandler>();

                tileHandler.SetCrop(selectedCrop, 0);
                tileHandler.SpawnCrop();

            }
            //var tileScript = gridObject.GetTile().GetComponent<ITileHandler>();
        }
    }

    public void CreateNewLevel() {
        editingLevel = ScriptableObject.CreateInstance<LevelSO>();
        string path = "Assets/Levels/" + CreateFileName() + ".asset";
        AssetDatabase.CreateAsset(editingLevel, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();

    }

    public void SaveLevel() { 
        // Save level
    }

    private string CreateFileName() {
        var myUniqueFileName = $@"{DateTime.Now.Ticks}_level.txt";
        return myUniqueFileName;
    }

}
