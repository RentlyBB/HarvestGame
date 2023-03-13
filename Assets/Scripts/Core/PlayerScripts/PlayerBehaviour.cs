using HarvestCode.Utilities;
using UnityEngine;
using HarvestCode.Core;
using HarvestCode.Tiles;
using HarvestCode.DataClasses;

namespace HarvestCode.Player {
    [RequireComponent(typeof(GridMovement))]
    public class PlayerBehaviour : MonoBehaviour {

        [SerializeField] public InputReaderSO _inputReader;
        [SerializeField] PlayerDataSO playerData;

        private Vector3 startingPositon = Vector3.zero;

        private void OnEnable() {
            _inputReader.GameResetEvent += ResetPlayer;
        }

        private void OnDisable() {
            _inputReader.GameResetEvent -= ResetPlayer;
        }

        public void InteractWithTile(GridObject gridObject) {

            if(gridObject.GetGrid() == null) return;

            var tool = playerData.GetActiveTool();
            if(tool != null) {
                tool.UseTool(gridObject);
            }

            var interactable = gridObject.GetLand().GetComponent<IInteractableTile>();
            
            if(interactable != null) { 
                interactable.Interact();
            }
        }

        public void InitPlayerData(LevelDataSO levevData) {
            startingPositon = levevData.playerStartPoint;
            ResetPlayer();
        }

        public void ResetPlayer() {
            transform.position = startingPositon;
            playerData.Reset();
        }
    }
}