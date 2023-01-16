using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum PlaceModes { 
    LandMode,
    CropMode
}

public class EditorManager : Singleton<EditorManager>{

    public delegate void OnCreateNewLevel();
    public static event OnCreateNewLevel onCreateNewLevel;

    [SerializeField] public LevelDataSO editingLevel;

    public LandSO selectedLand;
    public Transform selectedCrop;

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

                //Visualization
                obj = Instantiate(selectedLand.landPrefab.gameObject, grid.GetWorldPositionCellCenter(gridObject.GetX(), gridObject.GetZ()), Quaternion.identity);
                gridObject.ClearLand();
                gridObject.SetLand(obj.transform);

                //Update level data file
                editingLevel.SetLandToLevel(gridObject.GetX(), gridObject.GetZ(), selectedLand);
            }
        }
    }

    private void PlaceCropOnLand(GridObject gridObject) {
        if(selectedCrop != null) {
            //Check if on this tile is any land
            if(!gridObject.CanCreateLand()) { 
                var land = gridObject.GetLand();

                var farmland = land.GetComponent<Farmland>();

                if(farmland == null) return;

                if(farmland.GetCrop() == null) {
                    //Visualization
                    farmland.PlantCrop(selectedCrop);
                    
                    //Update level data file
                    editingLevel.SetCropOnLand(gridObject.GetX(), gridObject.GetZ(), selectedCrop);
                } else {
                    //Visualization
                    var plantable = farmland.GetCrop().GetComponent<Plantable>();
                    plantable.GrowthUp();

                    //Update level data file
                    editingLevel.SetCropStartPhase(gridObject.GetX(), gridObject.GetZ(), plantable.GetCurrentPhase());
                }
            }
        }
    }

    public void RemoveTileFromGrid(GridObject gridObject) {
        if(!gridObject.CanCreateLand()) {
            //Visualization
            Destroy(gridObject.GetLand().gameObject);
            gridObject.ClearLand();

            //Update level data file
            editingLevel.RemoveLand(gridObject.GetX(), gridObject.GetZ());
            editingLevel.SetCropStartPhase(gridObject.GetX(), gridObject.GetZ(), 0);
        }
    }

    public void AddSeed() {
        if(selectedCrop == null) return;
        editingLevel.seeds_list.Add(selectedCrop);
        
    }

    public void RemoveSeed() {
        if(editingLevel.seeds_list.Count > 0) {
            editingLevel.seeds_list.Remove(editingLevel.seeds_list[^1]);
        }
    }

    public void CreateNewLevel() {
        editingLevel = ScriptableObject.CreateInstance<LevelDataSO>();
        string path = "Assets/Resources/Levels/" + CreateFileName() + ".asset";
        #if UNITY_EDITOR
            AssetDatabase.CreateAsset(editingLevel, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(editingLevel);
            EditorUtility.FocusProjectWindow();
        #endif
        editingLevel.width = 3;
        editingLevel.height = 3;
        editingLevel.InitEmptyLevel();
        ClearLevel();

        onCreateNewLevel?.Invoke();

    }

    public void SaveLevel() {
        #if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(editingLevel);
            EditorUtility.FocusProjectWindow();
        #endif
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


    public void LoadLevel() { 
        
    }
}
