using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using RnT.Utilities;

namespace HarvestCode.Core {
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]

    public class InputReaderSO : DescriptionBaseSO, GameInput.IGameplayActions, GameInput.ILevelEditorActions {

        // Gameplay
        public event UnityAction MoveOnGrid = delegate { };
        public event UnityAction GameResetEvent = delegate { };

        //Level Editor
        public event UnityAction PlaceOnGrid = delegate { };
        public event UnityAction RemoveFromGrid = delegate { };

        public GameInput _gameInput;

        private void OnEnable() {

            if(_gameInput == null) {
                _gameInput = new GameInput();
                _gameInput.Gameplay.SetCallbacks(this);
                _gameInput.LevelEditor.SetCallbacks(this);
            }
            _gameInput.Gameplay.Enable();
            _gameInput.LevelEditor.Enable();
        }

        private void OnDisable() {
            _gameInput.Gameplay.Disable();
            _gameInput.LevelEditor.Disable();
        }

        #region Gameplay
        public void OnMoveOnGrid(InputAction.CallbackContext context) {
            if(context.phase == InputActionPhase.Performed) {
                MoveOnGrid?.Invoke();
            }
        }

        public void OnGameReset(InputAction.CallbackContext context) {
            if(context.phase == InputActionPhase.Performed) {
                GameResetEvent?.Invoke();
            }
        }
        #endregion


        #region Level Editor
        public void OnPlaceOnGrid(InputAction.CallbackContext context) {
            if(context.phase == InputActionPhase.Performed) {
                PlaceOnGrid?.Invoke();
            }
        }


        public void OnRemoveFromGrid(InputAction.CallbackContext context) {
            if(context.phase == InputActionPhase.Performed) {
                RemoveFromGrid?.Invoke();
            }
        }
        #endregion

        public void EnableLevelEditorInput() {
            _gameInput.Gameplay.Disable();

            _gameInput.LevelEditor.Enable();

        }

        public void EnableGameplayInput() {
            _gameInput.LevelEditor.Disable();

            _gameInput.Gameplay.Enable();

        }
    }
}