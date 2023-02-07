using UnityEngine;

namespace HarvestCode.Core {
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

            playerData.GetActiveTool().UseTool(gridObject);

            var interactable = gridObject.GetLand().GetComponent<IInteractableTile>();
            
            if(interactable != null) { 
                interactable.Interact();
            }
        }

        public void InitPlayerData(LevelDataSO levevData) {
            startingPositon = levevData.playerStartPoint;
        }

        public void ResetPlayer() {
            transform.position = startingPositon;

           // FIXME: Reset seedQueue 
           // planter.ResetPlanter();
        }
    }
}