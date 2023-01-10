using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public enum PlaceModes { 
    LandMode,
    CropMode
}

public class EditorManager : Singleton<EditorManager>{

    public delegate void OnCreateNewLevel();
    public static event OnCreateNewLevel onCreateNewLevel;

    [SerializeField] public LevelDataSO editingLevel;

    public LandSO selectedLand;
    public CropSO selectedCrop;

    public GridXZ<GridObject> grid;

    [Header("0 - Place Tile | 1 - Place Crop")]
    public PlaceModes placeMode;

    public void Place(GridObject gridObject) {

        switch(placeMode) {
            case PlaceModes.LandMode:
                PlaceLandOnGrid(gridObject);
                break;
            case PlaceModes.CropMode:
                PlaceCropOnLand(gridObject);
                break;
        }
    }
    
    private void PlaceLandOnGrid(GridObject gridObject) {
        if(selectedLand != null) {
            GameObject obj;
            if(gridObject.CanCreateLand()) {
                obj = Instantiate(selectedLand.landPrefab.gameObject, grid.GetWorldPositionCellCenter(gridObject.GetX(), gridObject.GetZ()), Quaternion.identity);
                gridObject.ClearLand();
                gridObject.SetLand(obj.transform);
                editingLevel.SetLandToLevel(gridObject.GetX(), gridObject.GetZ(), selectedLand);
            
            }
        }
    }

    private void PlaceCropOnLand(GridObject gridObject) {
        if(selectedCrop != null) {
            if(!gridObject.CanCreateLand()) { 
                var land = gridObject.GetLand();

                var plantable = land.GetComponent<Farmland>();

                if(plantable == null) return;
                if(plantable.GetCrop() != null) return;

                plantable.SetCrop(selectedCrop, 0);
                plantable.SpawnCrop();
                editingLevel.SetCropOnLand(gridObject.GetX(), gridObject.GetZ(), selectedCrop);

            }
        }
    }

    public void RemoveTileFromGrid(GridObject gridObject) {
        if(!gridObject.CanCreateLand()) {
            Destroy(gridObject.GetLand().gameObject);
            gridObject.ClearLand();
            editingLevel.RemoveLand(gridObject.GetX(), gridObject.GetZ());

        }
    }

    public void CreateNewLevel() {
        editingLevel = ScriptableObject.CreateInstance<LevelDataSO>();
        string path = "Assets/Resources/Levels/" + CreateFileName() + ".asset";
        AssetDatabase.CreateAsset(editingLevel, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(editingLevel);
        EditorUtility.FocusProjectWindow();

        editingLevel.width = 3;
        editingLevel.height = 3;
        editingLevel.InitEmptyLevel();
        ClearLevel();

        onCreateNewLevel?.Invoke();

    }

    public void SaveLevel() {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(editingLevel);
        EditorUtility.FocusProjectWindow();
        Debug.Log("Level Saved!");
    }

    public void ClearLevel() {
        var _x = grid.GetWidth();
        var _z = grid.GetHeight();

        for(int x = 0; x < _x; x++) {
            for(int z = 0; z < _z; z++) {
                RemoveTileFromGrid(grid.GetGridObject(x, z));
            }
        }
    }

    private string CreateFileName() {
        var myUniqueFileName = $@"{DateTime.Now.Ticks}_level.txt";
        return myUniqueFileName;
    }

}
