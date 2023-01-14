using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    [SerializeField] private LevelDataSO levelToLoad;

    private int width;
    private int height;
    private float cellSize = 1f;
   
    private GridXZ<GridObject> grid;

    //Movement use this
    public delegate void ClickedOnTile(GridXZ<GridObject> grid, Vector3 position);
    public static event ClickedOnTile clickedOnTile;

    private void OnEnable() {

    }

    private void OnDisable() {

    }

    private void Start() {
        InitGrid();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 position = GetMousePosition3D();

            clickedOnTile?.Invoke(grid, position);
        }

        if(Input.GetMouseButtonDown(1)) {
            GridObject curGO = grid.GetGridObject(GetMousePosition3D());
        }
    }

    private void InitGrid() {

        width = levelToLoad.width;
        height = levelToLoad.height;

        grid = new GridXZ<GridObject>(width, height, cellSize, transform.position, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
        GameManager.Instance.grid = grid;

        LoadLevel();
    }

    public void LoadLevel() {
        if(levelToLoad == null) {
            Debug.LogError("There is no level to load!");
            return;
        }

        for(int x = 0; x < width; x++) {
            for(int z = 0; z < height; z++) {
                var gridObject = grid.GetGridObject(x, z);

                if(gridObject.CanCreateLand()) {

                    var tileData = levelToLoad.GetTileDataAt(x, z);

                    if(tileData.GetLand() != null) {
                        var land = Instantiate(tileData.GetLand().landPrefab, grid.GetWorldPositionCellCenter(x, z), Quaternion.identity);
                        if(tileData.GetCrop() != null) {
                            var farmland = land.GetComponent<Farmland>();
                            farmland.PlantCrop(tileData.GetCrop(), tileData.GetStartPhase());
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
            return new Vector3(1000,1000,1000);
        }
    }
}
