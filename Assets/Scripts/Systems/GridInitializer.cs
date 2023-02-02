using ScriptableObjectArchitecture;
using System.Collections;
using HarvestCode.Core;
using RnT.Utilities;
using UnityEngine;

namespace HarvestCode.Systems {
    public class GridInitializer : MonoBehaviour {

#if UNITY_EDITOR
        [TextArea] public string description;
#endif

        [Space, SerializeField] private InputReaderSO _inputReader = default;

        [Header("Broadcasting Events")]
        [SerializeField] private GameEvent GridInitEvent = default(GameEvent);
        [SerializeField] private GridObjectGameEvent AddNextMoveEvent = default;

        private LevelDataSO levelData = default;

        private float cellSize = 1f;

        public GridXZ<GridObject> grid = default;


        private void OnEnable() {
            _inputReader.GameClickButton += MouseClickOnGrid;
            _inputReader.GameResetEvent += ResetLevel;
        }

        private void OnDisable() {
            _inputReader.GameClickButton -= MouseClickOnGrid;
            _inputReader.GameResetEvent -= ResetLevel;
        }

        public void LoadLevelData(LevelDataSO levelData) {
            this.levelData = levelData;
            InitGrid();
        }

        private void InitGrid() {

            grid = new GridXZ<GridObject>(levelData.width, levelData.height, cellSize, transform.position, (g, x, z) => new GridObject(g, x, z));

            StartCoroutine(SpawningGridObjects());

            GridInitEvent.Raise();
        }

        private IEnumerator SpawningGridObjects() {

            // Create grid with object from levelData
            for(int x = 0; x < levelData.width; x++) {
                for(int z = 0; z < levelData.height; z++) {
                    var gridObject = grid.GetGridObject(x, z);

                    if(!gridObject.CanCreateLand()) {
                        Destroy(gridObject.GetLand().gameObject);
                    }

                    var tileData = levelData.GetTileDataAt(x, z);

                    if(tileData.GetLand() != null) {
                        var land = Instantiate(tileData.GetLand().landPrefab, grid.GetWorldPositionCellCenter(x, z), Quaternion.identity);
                        if(tileData.GetCrop() != null) {
                            var farmland = land.GetComponent<Farmland>();
                            farmland.PlantCrop(tileData.GetCrop(), tileData.GetStartPhase());
                        }
                        gridObject.ClearLand();
                        gridObject.SetLand(land);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }
        }

        private void ResetLevel() {

            for(int x = 0; x < levelData.width; x++) {
                for(int z = 0; z < levelData.height; z++) {
                    var gridObject = grid.GetGridObject(x, z);
                    if(!gridObject.CanCreateLand()) {
                        Destroy(gridObject.GetLand().gameObject);
                    }
                }
            }

            InitGrid();
        }

        private void MouseClickOnGrid() {
            Vector3 position = Utils.GetMousePosition3D();

            var gridObject = grid.GetGridObject(position);

            Debug.Log("Ahoj" + gridObject.GetX() + ", " + gridObject.GetZ());
            

            Debug.Log("Ahoj after");

            AddNextMoveEvent.Raise(gridObject);
        }
    }
}