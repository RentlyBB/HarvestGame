using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FieldEditor : MonoBehaviour {
    [Header("Grid Properties")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    [Header("Edit Mode")]
    [SerializeField] private CropSO cropToSet;

    private GridXZ<GridObject> grid;

    private void Start() {
        InitGrid();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 position = GetMousePosition3D();

            var clickedGridObject = grid.GetGridObject(position);

            if(clickedGridObject != null) {
                EditorManager.Instance.Place(clickedGridObject, grid);
            } 
        }

        if(Input.GetMouseButtonDown(1)) {
            Vector3 position = GetMousePosition3D();

            var clickedGridObject = grid.GetGridObject(position);
            if(!clickedGridObject.CanCreateTile()) {
                Destroy(clickedGridObject.GetTile().gameObject);
                clickedGridObject.ClearTile();
            }
        }
    }


    private void InitGrid() {
        grid = new GridXZ<GridObject>(width, height, cellSize, transform.position, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
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
