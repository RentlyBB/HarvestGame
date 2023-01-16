using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmlandGrid : MonoBehaviour {

    [Header("Grid Movement Properties")]
    [SerializeField] private Transform player = default;
    [SerializeField] private float movementSpeed = default;

    private Vector3 lastPosition = default;
    [SerializeField] private List<Vector3> nextMoves = new List<Vector3>();

    [Header("Broadcasting Events")]
    [SerializeField] private GridObjectEventChannelSO OnMovementEndEvent = default;

    [Header("Event Listeners")]
    [SerializeField] private LevelDataEventChannelSO LoadLevelEvent = default;

    private int width = default;
    private int height = default;

    private float cellSize = 1f;
   
    private GridXZ<GridObject> grid = default;

    private void OnEnable() {
        LoadLevelEvent.OnEventRaised += InitGrid;    
    }
    private void OnDisable() {
        LoadLevelEvent.OnEventRaised -= InitGrid;
    }

    private void Start() {
        lastPosition = player.position;
    }

    private void Update() {

        // Check if player should move
        if(nextMoves.Count > 0) { 
            MovePlayer();
        }

        if(Input.GetMouseButtonDown(0)) {
            Vector3 position = GetMousePosition3D();
            CalculateNextMove(position);
        }
    }

    private void InitGrid(LevelDataSO levelData) {

        width = levelData.width;
        height = levelData.height;

        grid = new GridXZ<GridObject>(width, height, cellSize, transform.position, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
        
        // Create grid with object from levelData
        for(int x = 0; x < width; x++) {
            for(int z = 0; z < height; z++) {
                var gridObject = grid.GetGridObject(x, z);

                if(gridObject.CanCreateLand()) {

                    var tileData = levelData.GetTileDataAt(x, z);

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

    public void CalculateNextMove(Vector3 mouseWorldPositon) {

        // Calcuate world position of the cell by mouse position
        Vector3 worldPosition = grid.GetWorldPositionCellCenter(mouseWorldPositon);

        var gridObject = grid.GetGridObject(worldPosition);

        //Check if the land is walkable
        if(!CheckIfWalkable(gridObject)) {
            return;
        }


        int lastX, lastZ;

        if(nextMoves.Count != 0) {
            grid.GetXZ(nextMoves[^1], out lastX, out lastZ);
        } else {
            grid.GetXZ(lastPosition, out lastX, out lastZ);
        }

        //check if next movement is valid
        if(ValidateNextMove(lastX, lastZ, gridObject.GetX(), gridObject.GetZ())) {
            nextMoves.Add(worldPosition);
        }
    }

    //Should be this in script for player??
    private void MovePlayer() {
        var step = movementSpeed * Time.deltaTime;
        var targetPosition = new Vector3(nextMoves[0].x, player.position.y, nextMoves[0].z);
        player.position = Vector3.MoveTowards(player.position, targetPosition, step);

        if(Vector3.Distance(player.position, targetPosition) < 0.001f) {
            lastPosition = nextMoves[0];

            OnMovementEndEvent.RaiseEvent(grid.GetGridObject(lastPosition));
            
            nextMoves.RemoveAt(0);
        }
    }

    private bool CheckIfWalkable(GridObject gridObject) {

        if(gridObject == null) return false;

        var walkable = gridObject.GetLand().GetComponent<Walkable>();

        if(walkable != null && walkable.IsWalkable()) {
            return true;
        }

        return false;
    }

    private bool ValidateNextMove(int lastX, int lastZ, int nextX, int nextZ) {

        //Check if we are already on tile
        if(lastX == nextX && lastZ == nextZ) return false;

        // Check horizontals and verticals
        if(nextX > (lastX + 1) || nextX < (lastX - 1)) return false;
        if(nextZ > (lastZ + 1) || nextZ < (lastZ - 1)) return false;

        // Check diagonals
        //if(nextX > lastX && nextZ > lastZ) return false;
        //if(nextX < lastX && nextZ < lastZ) return false;
        //if(nextX > lastX && nextZ < lastZ) return false;
        //if(nextX < lastX && nextZ > lastZ) return false;

        return true;
    }

    //Input Manager?? SO??
    private Vector3 GetMousePosition3D() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue)) {
            return raycastHit.point;
        } else {
            return new Vector3(1000,1000,1000);
        }
    }
}
