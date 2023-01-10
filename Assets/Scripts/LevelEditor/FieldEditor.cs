using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FieldEditor : MonoBehaviour {
    [Header("Grid Properties")]
    [SerializeField] private float cellSize;

    [Header("Edit Mode")]
    [SerializeField] private CropSO cropToSet;

    private GridXZ<GridObject> grid;
    private int width;
    private int height;


    private void OnEnable() {
        EditorManager.onCreateNewLevel += InitGrid;   
    }

    private void OnDisable() {
        EditorManager.onCreateNewLevel -= InitGrid;
    }

    private void Start() {
        InitGrid();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 position = GetMousePosition3D();
            var clickedGridObject = grid.GetGridObject(position);

            if(clickedGridObject != null) {
                EditorManager.Instance.Place(clickedGridObject);
            } 
        }

        if(Input.GetMouseButtonDown(1)) {
            Vector3 position = GetMousePosition3D();
            var clickedGridObject = grid.GetGridObject(position);

            if(clickedGridObject != null) {
                EditorManager.Instance.RemoveTileFromGrid(clickedGridObject);
            }
        }
    }

    private void InitGrid() {
        width = EditorManager.Instance.editingLevel.width;
        height = EditorManager.Instance.editingLevel.height;

        grid = new GridXZ<GridObject>(width, height, cellSize, transform.position, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));

        LoadLevel();

        EditorManager.Instance.grid = grid;
    }

    public void LoadLevel() {

        for(int x = 0; x < width; x++) {
            for(int z = 0; z < height; z++) {
                var gridObject = grid.GetGridObject(x, z);
                
                if(gridObject.CanCreateLand()) {
                    var tileData = EditorManager.Instance.editingLevel.GetTileDataAt(x, z);

                    if(EditorManager.Instance.editingLevel == null) Debug.Log("aha");

                    if(tileData.GetLand() != null) {
                        var land = Instantiate(tileData.GetLand().landPrefab, grid.GetWorldPositionCellCenter(x, z), Quaternion.identity);
                        if(tileData.GetCrop() != null) {
                            var plantable = land.GetComponent<Farmland>();
                            plantable.SetCrop(tileData.GetCrop(), 0);
                            plantable.SpawnCrop();
                        }
                        gridObject.ClearLand();
                        gridObject.SetLand(land);
                    }
                }
            }
        }
    }

    private Vector3 GetMousePosition3D() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue)) {
            return raycastHit.point;
        } else {
            return new Vector3(1000, 1000, 1000);
        }
    }
}
