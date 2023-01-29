using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed;
    [SerializeField] private bool allowDiagonalMovement = true;

    [Header("Broadcasting Events")]
    [SerializeField] private VoidEventChannelSO OnMovementEndEvent = default;

    [Header("Listen to")]
    [SerializeField] private GridObjectEventChannelSO AddNextMoveEvent = default;

    // List of GridObjects which hold position where player wants to go
    private List<GridObject> nextMoves = new List<GridObject>();

    // Last grid object where player was on
    private GridObject lastPosition;

    private PlayerBehaviour interator;


    private void OnEnable() {
        AddNextMoveEvent.OnEventRaised += CalculateNextMove;
    }

    private void OnDisable() {
        AddNextMoveEvent.OnEventRaised -= CalculateNextMove;
    }

    private void Start() {
        interator = GetComponent<PlayerBehaviour>();
    }

    private void Update() {
        MovePlayer();
    }

    public void CalculateNextMove(GridObject gridObject) {

        //Check if the land is walkable
        if(!CheckIfWalkable(gridObject)) {
            return;
        }

        if(lastPosition == null) {
            nextMoves.Add(gridObject);
            return;
        }

        int lastX, lastZ;
        if(nextMoves.Count != 0 ) {
            gridObject.GetGrid().GetXZ(nextMoves[^1].GetWorldPositionCellCenter(), out lastX, out lastZ);
        } else {
            gridObject.GetGrid().GetXZ(lastPosition.GetWorldPositionCellCenter(), out lastX, out lastZ);
        }

        //check if next movement is valid
        if(ValidateNextMove(lastX, lastZ, gridObject.GetX(), gridObject.GetZ())) {
            nextMoves.Add(gridObject);
        }
    }

    private void MovePlayer() {

        if(nextMoves.Count == 0) return;

        var step = movementSpeed * Time.deltaTime;
        
        var targetPosition = nextMoves[0].GetWorldPositionCellCenter();
        targetPosition.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if(Vector3.Distance(transform.position, targetPosition) < 0.001f) {
            lastPosition = nextMoves[0];

            interator.InteractWithTile(lastPosition);

            OnMovementEndEvent.RaiseEvent();

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
        if(!allowDiagonalMovement) {
            if(nextX > lastX && nextZ > lastZ) return false;
            if(nextX < lastX && nextZ < lastZ) return false;
            if(nextX > lastX && nextZ < lastZ) return false;
            if(nextX < lastX && nextZ > lastZ) return false;
        }

        return true;
    }

}
