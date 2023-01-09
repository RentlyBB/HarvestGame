using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    [Header("Grid Properties")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    [Header("Other")]
    [SerializeField] private Transform tile_prefab;

    [Header("Edit Mode")]
    [SerializeField] private CropSO cropToSet;
   
    private GridXZ<GridObject> grid;

    public delegate void ClickedOnTile(GridXZ<GridObject> grid, Vector3 position);
    public static event ClickedOnTile clickedOnTile;

    public delegate void OnGridInit();
    public static event OnGridInit onGridInit;

    private void OnEnable() {
        GridMovement.onMovementEnds += HarverstTile;
    }

    private void OnDisable() {
        GridMovement.onMovementEnds -= HarverstTile;
    }

    private void Start() {
        InitGrid();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 position = GetMousePosition3D();

            GridObject curGO = grid.GetGridObject(position);

            if(curGO != null) { 
                clickedOnTile?.Invoke(grid, position);
            }
        }

        if(Input.GetMouseButtonDown(1)) {
            GridObject curGO = grid.GetGridObject(GetMousePosition3D());
            GameManager.Instance.level.SetTileData(curGO.GetX(), curGO.GetZ(), cropToSet);
        }
    }

    private void InitGrid() {
        grid = new GridXZ<GridObject>(width, height, cellSize, transform.position, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));

        for(int x = 0; x < width; x++) {
            for(int z = 0; z < height; z++) {
                var tile = Instantiate(tile_prefab, grid.GetWorldPositionCellCenter(x, z), Quaternion.identity, transform);
                var fetalGrowthScipt = tile.GetComponent<FetalGrowth>();

                var tileData = GameManager.Instance.level.GetTileDataAt(x, z);

                if(tileData != null) fetalGrowthScipt.SetCrop(tileData.GetCrop(), tileData.GetCurrentPhase());

                grid.GetGridObject(x, z).SetLand(tile);

            }
        }

        onGridInit?.Invoke();
    }

    private Vector3 GetMousePosition3D() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue)) {
            return raycastHit.point;
        } else {
            return new Vector3(1000,1000,1000);
        }
    }

    private void HarverstTile(Vector3 worldPosition) {
        var gridObject = grid.GetGridObject(worldPosition);
        var tile = gridObject.GetLand();
        var fetalGrowth = tile.GetComponent<FetalGrowth>();

        if(fetalGrowth.canSpawnCrop == false && GameManager.Instance.level.seeds > 0) {
            fetalGrowth.SetCrop(cropToSet, 0);
            fetalGrowth.SpawnCrop();
            GameManager.Instance.level.seeds--;
            return;
        }

        if(fetalGrowth.GetCurrentPhase() == 3) {
            fetalGrowth.HarvestCrop();
            return;
        }

    }
}
