using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

    [Header("Broadcasting Events")]
    [SerializeField] private VoidEventChannelSO OnMovementEndEvent;


    [SerializeField] private List<Vector3> nextMoves;
    [SerializeField] private float speed;

    private bool canMove = false;
    private Vector3 lastPosition;

    private void Start() {
        lastPosition = transform.position;
    }

    private void OnEnable() {
    }

    private void OnDisable() {
    }

    private void Update() {
        if(nextMoves.Count == 0) canMove = false;
        ProcessMovement();
    }

    public void CalculateNextMove(GridXZ<GridObject> grid, Vector3 mouseWorldPositon) {

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
            canMove = true;
        }
    }
    
    private void ProcessMovement() {

        if(canMove) {
            var step = speed * Time.deltaTime;
            var target = new Vector3(nextMoves[0].x, transform.position.y, nextMoves[0].z);
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if(Vector3.Distance(transform.position, target) < 0.001f) {
                lastPosition = nextMoves[0];
                OnMovementEndEvent.RaiseEvent();
                nextMoves.RemoveAt(0);
            }
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
}
