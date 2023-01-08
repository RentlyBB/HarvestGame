using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

    public delegate void OnMovementEnds(Vector3 worldPosition = default(Vector3));
    public static event OnMovementEnds onMovementEnds;

    [SerializeField] private List<Vector3> nextMoves;
    [SerializeField] private float speed;

    private bool canMove = false;
    private Vector3 lastPosition;

    private void Start() {
        lastPosition = transform.position;
    }

    private void OnEnable() {
        Field.clickedOnTile += calculateNextMove;
    }

    private void OnDisable() {
        Field.clickedOnTile -= calculateNextMove;
    }

    private void Update() {
        if(nextMoves.Count == 0) canMove = false;
        processMovement();
    }

    private void calculateNextMove(GridXZ<GridObject> grid, Vector3 mouseWorldPositon) {
       
        // Calcuate world position of the cell by mouse position
        Vector3 worldPosition = grid.GetWorldPositionCellCenter(mouseWorldPositon);

        // Get XZ coords of the next cell
        grid.GetXZ(mouseWorldPositon, out int nextX, out int nextZ);

        int lastX, lastZ;

        if(nextMoves.Count != 0) {
            grid.GetXZ(nextMoves[^1], out lastX, out lastZ);
        } else {
            grid.GetXZ(lastPosition, out lastX, out lastZ);
        }

        //check if next movement is valid
        if(validateNextMove(lastX, lastZ, nextX, nextZ)) {
            nextMoves.Add(worldPosition);
            canMove = true;
        }
    }
    
    private void processMovement() {

        if(canMove) {
            var step = speed * Time.deltaTime;
            var target = new Vector3(nextMoves[0].x, transform.position.y, nextMoves[0].z);
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if(Vector3.Distance(transform.position, target) < 0.001f) {
                lastPosition = nextMoves[0];
                onMovementEnds?.Invoke(lastPosition);
                nextMoves.RemoveAt(0);
            }
        } 
    }

    private bool validateNextMove(int lastX, int lastZ, int nextX, int nextZ) {

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
