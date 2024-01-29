using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using HarvestCode.Utilities;
using HarvestCode.Tiles;

namespace HarvestCode.Player {
    public class GridMovement : MonoBehaviour {

        [SerializeField] private float movementSpeed;
        [SerializeField] private bool allowDiagonalMovement = true;

        [Header("Broadcasting Events")]
        [SerializeField] private GameEvent OnMovementEndEvent = default(GameEvent);

        // List of GridObjects which hold position where player wants to go
        private List<GridObject> nextMoves = new List<GridObject>();

        // Last grid object where player was on
        private GridObject lastPosition = new GridObject();

        private PlayerBehaviour interactor;

        private void Awake() {
            interactor = GetComponent<PlayerBehaviour>();
        }

        private void OnEnable() {
            interactor._inputReader.GameResetEvent += ResetMovement;
        }

        private void OnDisable() {
            interactor._inputReader.GameResetEvent -= ResetMovement;
        }
     
        private void Update() {
            MovePlayer();
        }

        public void CalculateNextMove(GridObject gridObject) {

            if(gridObject == null) return;

            //Check if the land is walkable
            if(!CheckIfWalkable(gridObject)) {
                return;
            }

            if(lastPosition.GetGrid() == null) {
                nextMoves.Add(gridObject);
                return;
            }

            int lastX, lastZ;
            if(nextMoves.Count != 0) {
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
                
                interactor.InteractWithTile(lastPosition);

                OnMovementEndEvent.Raise();

                nextMoves.RemoveAt(0);
            }
        }

        private bool CheckIfWalkable(GridObject gridObject) {

            if(gridObject.GetLand() == null) return false;

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
            if(nextX > lastX + 1 || nextX < lastX - 1) return false;
            if(nextZ > lastZ + 1 || nextZ < lastZ - 1) return false;

            // Check diagonals
            if(!allowDiagonalMovement) {
                if(nextX > lastX && nextZ > lastZ) return false;
                if(nextX < lastX && nextZ < lastZ) return false;
                if(nextX > lastX && nextZ < lastZ) return false;
                if(nextX < lastX && nextZ > lastZ) return false;
            }

            return true;
        }

        private void ResetMovement() {
            lastPosition = new GridObject();
            nextMoves = new List<GridObject>();
        }
    }
}