using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RnT.Utilities;
using HarvestCode.Core;


namespace HarvestCode.LevelEditor {
    public class FieldEditor : MonoBehaviour {

        [Header("Broadcasting Events")]
        [SerializeField] private VoidEventChannelSO OnGridInit = default;

        private float cellSize = 1;
        public GridXZ<GridObject> grid;
        private int width;
        private int height;

        private EditorManager editorManager;

        private void OnEnable() {
            EditorManager.onCreateNewLevel += InitGrid;
        }

        private void OnDisable() {
            EditorManager.onCreateNewLevel -= InitGrid;
        }

        private void Start() {
            editorManager = EditorManager.Instance;
            InitGrid();
        }

        private void Update() {
            if(Input.GetMouseButtonDown(0)) {
                Vector3 position = GetMousePosition3D();
                var clickedGridObject = grid.GetGridObject(position);

                if(clickedGridObject != null) {
                    editorManager.Place(clickedGridObject);
                }
            }

            if(Input.GetMouseButtonDown(1)) {
                Vector3 position = GetMousePosition3D();
                var clickedGridObject = grid.GetGridObject(position);

                if(clickedGridObject != null) {
                    editorManager.RemoveTileFromGrid(clickedGridObject);
                }
            }
        }

        private void InitGrid() {
            width = editorManager.editingLevel.width;
            height = editorManager.editingLevel.height;

            grid = new GridXZ<GridObject>(width, height, cellSize, transform.position, (g, x, z) => new GridObject(g, x, z));

            LoadLevel();

            editorManager.grid = grid;

            OnGridInit.RaiseEvent();
        }

        public void LoadLevel() {
            if(editorManager.editingLevel == null) {
                Debug.LogError("There is no level to load in EditorManager.editingLevel!");
                return;
            }

            for(int x = 0; x < width; x++) {
                for(int z = 0; z < height; z++) {
                    var gridObject = grid.GetGridObject(x, z);

                    if(gridObject.CanCreateLand()) {

                        var tileData = editorManager.editingLevel.GetTileDataAt(x, z);

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
                return new Vector3(1000, 1000, 1000);
            }
        }
    }
}