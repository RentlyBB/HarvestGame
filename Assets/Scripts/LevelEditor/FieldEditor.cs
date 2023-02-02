using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RnT.Utilities;
using HarvestCode.Core;
using ScriptableObjectArchitecture;


namespace HarvestCode.LevelEditor {
    public class FieldEditor : MonoBehaviour {

        [SerializeField] private InputReaderSO _inputReader;

        [Header("Broadcasting Events")]
        [SerializeField] private GameEvent OnGridInit = default(GameEvent);

        private float cellSize = 1;
        public GridXZ<GridObject> grid;
        private int width;
        private int height;

        private EditorManager editorManager;

        private void OnEnable() {
            EditorManager.onCreateNewLevel += InitGrid;
            _inputReader.PlaceOnGrid += PlaceOnGrid;
            _inputReader.RemoveFromGrid += RemoveFromGrid;
        }

        private void OnDisable() {
            EditorManager.onCreateNewLevel -= InitGrid;
            _inputReader.PlaceOnGrid -= PlaceOnGrid;
            _inputReader.RemoveFromGrid -= RemoveFromGrid;
        }

        private void Start() {

            _inputReader.EnableLevelEditorInput();

            editorManager = EditorManager.Instance;

            InitGrid();
        }

        private void PlaceOnGrid() {
            Vector3 position = Utils.GetMousePosition3D();

            var gridObject = grid.GetGridObject(position);

            if(gridObject == null) return;

            editorManager.Place(gridObject);
        }

        private void RemoveFromGrid() {
            Vector3 position = Utils.GetMousePosition3D();

            var gridObject = grid.GetGridObject(position);

            if(gridObject == null) return;

            editorManager.RemoveTileFromGrid(gridObject);
        }


        private void InitGrid() {
            width = editorManager.editingLevel.width;
            height = editorManager.editingLevel.height;

            grid = new GridXZ<GridObject>(width, height, cellSize, transform.position, (g, x, z) => new GridObject(g, x, z));

            LoadLevel();

            editorManager.grid = grid;

            OnGridInit.Raise();
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
    }
}